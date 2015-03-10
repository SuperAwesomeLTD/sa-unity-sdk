/*
 *  Copyright (c) 2011 The ORMMA.org project authors. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license
 *  that can be found in the LICENSE file in the root of the source
 *  tree. All contributing project authors may
 *  be found in the AUTHORS file in the root of the source tree.
 */
(function () {
    var ormma = window.ormma = {};
    var mraid = window.mraid = {};

    // CONSTANTS ///////////////////////////////////////////////////////////////

    var STATES = ormma.STATES = {
        UNKNOWN:'unknown',
        LOADING:'loading',
        DEFAULT:'default',
        RESIZED:'resized',
        EXPANDED:'expanded',
        HIDDEN:'hidden'
    };
    mraid.STATES = {
        LOADING:'loading',
        DEFAULT:'default',
        EXPANDED:'expanded',
        HIDDEN:'hidden'
    };

    var EVENTS = ormma.EVENTS = {
        READY:'ready',
        ERROR:'error',
        INFO:'info',
		RESPONSE:'response',
		SHAKE:'shake',
 
		HEADINGCHANGE:'heading',
        HEADINGCHANGE_WITH_SUFFIX:'headingChange',
 
        KEYBOARDCHANGE:'keyboard',
		KEYBOARDCHANGE_WITH_SUFFIX:'keyboardChange',
 
		LOCATIONCHANGE:'location',
        LOCATIONCHANGE_WITH_SUFFIX:'locationChange',
	
		NETWORKCHANGE:'network',
        NETWORKCHANGE_WITH_SUFFIX:'networkChange',
		
        ORIENTATIONCHANGE:'orientation',
		ORIENTATIONCHANGE_WITH_SUFFIX:'orientationChange',
 
		SCREENCHANGE:'screen',
		SCREENCHANGE_WITH_SUFFIX:'screenChange',
	
        SIZECHANGE:'size',
		SIZECHANGE_WITH_SUFFIX:'sizeChange',
 
		TILTCHANGE:'tilt',
		TILTCHANGE_WITH_SUFFIX:'tiltChange',
 
		STATECHANGE_WITH_SUFFIX:'stateChange',
		VIEWABLECHANGE_WITH_SUFFIX:'viewableChange'
    };

    mraid.EVENTS = {
        READY:'ready',
        ERROR:'error',
        INFO:'info',
        STATECHANGE_WITH_SUFFIX:'stateChange',
        VIEWABLECHANGE_WITH_SUFFIX:'viewableChange'
    };

    var FEATURES = ormma.FEATURES = {
        LEVEL1:'level-1',
        LEVEL2:'level-2',
        SCREEN:'screen',
        ORIENTATION:'orientation',
        HEADING:'heading',
        LOCATION:'location',
        SHAKE:'shake',
        TILT:'tilt',
        NETWORK:'network',
        KEYBOARD:'keyboard',
        SMS:'sms',
        PHONE:'phone',
        EMAIL:'email',
        CALENDAR:'calendar',
        CAMERA:'camera',
        MAP:'map',
        AUDIO:'audio',
        VIDEO:'video'
    };

    mraid.FEATURES = {
        SMS:'sms',
        TEL:'tel',
        CALENDAR:'calendar',
        STOREPICTURE:'storePicture',
        INLINEVIDEO: 'inlineVideo'
    };

    var NETWORK = ormma.NETWORK = {
        OFFLINE:'offline',
        WIFI:'wifi',
        CELL:'cell',
        UNKNOWN:'unknown'
    };

    // PRIVATE PROPERTIES (sdk controlled)
	// //////////////////////////////////////////////////////

    var state = STATES.UNKNOWN;

    var size = {
        width:0,
        height:0
    };

    var defaultPosition = {
        x:0,
        y:0,
        width:0,
        height:0
    };

    var currentPosition = {
        x:0,
        y:0,
        width:0,
        height:0
    };

    var maxSize = {
        width:0,
        height:0
    };


    var supports = {
        <!-- ORMMA -->
        'level-1':false,
        'level-2':false,
        'screen':false,
        'orientation':false,
        'keyboard':false,
        'network':false,
        'heading':false,
        'location':false,
        'shake':false,
        'tilt':false,
        'phone':false,
        'email':false,
        'camera':false,
        'map':false,
        'audio':false,
        'video':false,

        <!-- MRAID 2.0 -->
        'tel':false,
        'storePicture':false,
        'inlineVideo':false,

        <!-- COMMON -->
        'sms':false,
        'calendar':false
    };

    var heading = -1;

    var keyboardState = false;

    var location = null;

    var network = NETWORK.UNKNOWN;

    var orientation = -1;

    var screenSize = null;

    var shakeProperties = null;

    var tilt = null;

    var assets = {};

    var cacheRemaining = -1;

    // PRIVATE PROPERTIES (internal)
	// //////////////////////////////////////////////////////

    var intervalID = null;
    var timeoutID = null;
    var readyTimeout = 10000;
    var readyInterval = 750;

    // @TODO: ok to allow ads that are larger than maxSize
    var sizeValidators = {
        width:function (value) {
            return !isNaN(value) && value >= 0 && value <= maxSize.width;
        },
        height:function (value) {
            return !isNaN(value) && value >= 0 && value <= maxSize.height;
        }
    };

    // @TODO: there are more expand properties
    var expandPropertyValidators = {
        useBackground:function (value) {
            return (value === true || value === false);
        },
        backgroundColor:function (value) {
            return true;
        }, // return (typeof value == 'string' && value.substr(0,1) == '#' &&
			// !isNaN(parseInt(value.substr(1), 16))); },
        backgroundOpacity:function (value) {
            return !isNaN(value) && value >= 0 && value <= 1;
        },
        isModal:function (value) {
            return (value === true || value === false);
        },
        lockOrientation:function (value) {
            return (value === true || value === false);
        },
        useCustomClose:function (value) {
            return (value === true || value === false);
        },
        width:function (value) {
            return !isNaN(value) && value >= 0;
        },
        height:function (value) {
            return !isNaN(value) && value >= 0;
        }
    };

    var shakePropertyValidators = {
        intensity:function (value) {
            return !isNaN(value);
        },
        interval:function (value) {
            return !isNaN(value);
        }
    };

    var changeHandlers = {
		ready:function (val) {
			intervalID = window.setInterval(ormma.signalReady, 0);
		},
        state:function (val) {
            if (state == STATES.UNKNOWN && val != STATES.UNKNOWN) {
                broadcastEvent(EVENTS.INFO, 'controller initialized');
            }
 
			broadcastEvent(EVENTS.INFO, 'setting state to ' + stringify(val));
			state = val;
			broadcastEvent(EVENTS.STATECHANGE_WITH_SUFFIX, state);
        },
        size:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting size to ' + stringify(val));
            size = val;
            broadcastEvent(EVENTS.SIZECHANGE, size.width, size.height);
			broadcastEvent(EVENTS.SIZECHANGE_WITH_SUFFIX, size.width, size.height);
        },
        defaultPosition:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting default position to ' + stringify(val));
            defaultPosition = val;
        },
        currentPosition:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting current position to ' + stringify(val));
            currentPosition = val;
        },
        maxSize:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting maxSize to ' + stringify(val));
            maxSize = val;
        },
        expandProperties:function (val) {
            broadcastEvent(EVENTS.INFO, 'merging expandProperties with ' + stringify(val));
            ormmaview.setExpandProperties(val);
        },
        supports:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting supports to ' + stringify(val));
            supports = {};
            for (var key in ormma.FEATURES) {
                supports[ormma.FEATURES[key]] = contains(ormma.FEATURES[key], val);
            }
            for (var key in mraid.FEATURES) {
                supports[mraid.FEATURES[key]] = contains(mraid.FEATURES[key], val);
            }
        },
        heading:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting heading to ' + stringify(val));
            heading = val;
            broadcastEvent(EVENTS.HEADINGCHANGE, heading);
			broadcastEvent(EVENTS.HEADINGCHANGE_WITH_SUFFIX, heading);
        },
        keyboardState:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting keyboardState to ' + stringify(val));
            keyboardState = val;
            broadcastEvent(EVENTS.KEYBOARDCHANGE, keyboardState);
			broadcastEvent(EVENTS.KEYBOARDCHANGE_WITH_SUFFIX, keyboardState);
        },
        location:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting location to ' + stringify(val));
            location = val;
            broadcastEvent(EVENTS.LOCATIONCHANGE, location.lat, location.lon, location.acc);
			broadcastEvent(EVENTS.LOCATIONCHANGE_WITH_SUFFIX, location.lat, location.lon, location.acc);
        },
        network:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting network to ' + stringify(val));
            network = val;
            broadcastEvent(EVENTS.NETWORKCHANGE, (network != NETWORK.OFFLINE && network != NETWORK.UNKNOWN), network);
			broadcastEvent(EVENTS.NETWORKCHANGE_WITH_SUFFIX, (network != NETWORK.OFFLINE && network != NETWORK.UNKNOWN), network);
        },
        orientation:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting orientation to ' + stringify(val));
            orientation = val;
            broadcastEvent(EVENTS.ORIENTATIONCHANGE, orientation);
			broadcastEvent(EVENTS.ORIENTATIONCHANGE_WITH_SUFFIX, orientation);
        },
        screenSize:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting screenSize to ' + stringify(val));
            screenSize = val;
            broadcastEvent(EVENTS.SCREENCHANGE, screenSize.width, screenSize.height);
			broadcastEvent(EVENTS.SCREENCHANGE_WITH_SUFFIX, screenSize.width, screenSize.height);
        },
        shakeProperties:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting shakeProperties to ' + stringify(val));
            shakeProperties = val;
        },
        tilt:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting tilt to ' + stringify(val));
            tilt = val;
            broadcastEvent(EVENTS.TILTCHANGE, tilt.x, tilt.y, tilt.z);
			broadcastEvent(EVENTS.TILTCHANGE_WITH_SUFFIX, tilt.x, tilt.y, tilt.z);
        },

        // added by adtech
        viewable:function (val) {
            broadcastEvent(EVENTS.INFO, 'setting viewable to ' + stringify(val));
            mraid.viewable = val;
            broadcastEvent(EVENTS.VIEWABLECHANGE_WITH_SUFFIX, val);
        }
    };

    var listeners = {};

    var EventListeners = function (event) {
        this.event = event;
        this.count = 0;
        var listeners = {};

        this.add = function (func) {
            var id = String(func);
            if (!listeners[id]) {
                listeners[id] = func;
                this.count++;
                if (this.count == 1) {
                    if (event != EVENTS.INFO && event != EVENTS.ERROR && event != EVENTS.READY && event != EVENTS.VIEWABLECHANGE_WITH_SUFFIX) {
                        broadcastEvent(EVENTS.INFO, 'activating ' + event);
                        ormmaview.activate(event);
                    }
                }
            }
        };
        this.remove = function (func) {
            var id = String(func);
            if (listeners[id]) {
                listeners[id] = null;
                delete listeners[id];
                this.count--;
                if (this.count == 0) {
                    broadcastEvent(EVENTS.INFO, 'deactivating ' + event);
                    ormmaview.deactivate(event);
                }
                return true;
            } else {
                return false;
            }
        };
        this.removeAll = function () {
            for (var id in listeners) this.remove(listeners[id]);
        };
        this.broadcast = function (args) {
            for (var id in listeners) listeners[id].apply({}, args);
        };
        this.toString = function () {
            var out = [event, ':'];
            for (var id in listeners) out.push('|', id, '|');
            return out.join('');
        };
    };

    // PRIVATE METHODS
	// ////////////////////////////////////////////////////////////

    ormmaview.addEventListener('change', function (properties) {
        for (var property in properties) {
            var handler = changeHandlers[property];
            if (handler === undefined) {
                // ignnore the handler
            } else {
                handler(properties[property]);
            }
        }
    });

    ormmaview.addEventListener('shake', function () {
        broadcastEvent(EVENTS.SHAKE);
    });

    ormmaview.addEventListener('error', function (message, action) {
        broadcastEvent(EVENTS.ERROR, message, action);
    });

    ormmaview.addEventListener('response', function (uri, response) {
        broadcastEvent(EVENTS.RESPONSE, uri, response);
    });

    var clone = function (obj) {
        var f = function () {
        };
        f.prototype = obj;
        return new f();
    };

    var stringify = function (obj) {
        if (typeof obj == 'object') {
            if (obj.push) {
                var out = [];
                for (var p = 0; p < obj.length; p++) {
                    out.push(obj[p]);
                }
                return '[' + out.join(',') + ']';
            } else {
                var out = [];
                for (var p in obj) {
                    out.push('\'' + p + '\':' + obj[p]);
                }
                return '{' + out.join(',') + '}';
            }
        } else {
            return String(obj);
        }
    };

    var valid = function (obj, validators, action, full) {
        if (full) {
            if (obj === undefined) {
                broadcastEvent(EVENTS.ERROR, 'Required object missing.', action);
                return false;
            } else {
                for (var i in validators) {
                    if (obj[i] === undefined) {
                        broadcastEvent(EVENTS.ERROR, 'Object missing required property ' + i, action);
                        return false;
                    }
                }
            }
        }
        for (var i in obj) {
            if (!validators[i]) {
                broadcastEvent(EVENTS.ERROR, 'Invalid property specified - ' + i + '.', action);
                return false;
            } else if (!validators[i](obj[i])) {
                broadcastEvent(EVENTS.ERROR, 'Value of property ' + i + ' is not valid type.', action);
                return false;
            }
        }
        return true;
    };

    var contains = function (value, array) {
        for (var i in array) if (array[i] == value) return true;
        return false;
    };

    var broadcastEvent = function () {
        var args = new Array(arguments.length);
        for (var i = 0; i < arguments.length; i++) args[i] = arguments[i];
        var event = args.shift();
        if (listeners[event]) listeners[event].broadcast(args);
    }

    // LEVEL 1
	// ////////////////////////////////////////////////////////////////////

    ormma.readyTimeout = function () {
        window.clearInterval(intervalID);
        window.clearTimeout(timeoutID);
        if (!ormmaview.scriptFound) {
            broadcastEvent(EVENTS.INFO, 'No ORMMAReady callback found (timeout of ' + readyTimeout + 'ms occurred), assume use of ready eventListener.');
        }
    };

    ormma.signalReady = function () {
		window.clearInterval(intervalID);
        broadcastEvent(EVENTS.INFO, 'ready eventListener triggered');
        broadcastEvent(mraid.EVENTS.READY, 'mraid ready event triggered');
        try {
            ORMMAReady();
            broadcastEvent(EVENTS.INFO, 'ORMMA callback invoked');
        } catch (e) {
            // ignore errors, will try again soon and then timeout
        }
    };

    ormma.info = function (message) {
        broadcastEvent(EVENTS.INFO, message);
    };

    ormma.error = function (message) {
        broadcastEvent(EVENTS.ERROR, message);
    };

    ormma.addEventListener = function (event, listener) {
        if (!event || !listener) {
            broadcastEvent(EVENTS.ERROR, 'Both event and listener are required.', 'addEventListener');
        } else if (!contains(event, EVENTS)) {
            broadcastEvent(EVENTS.ERROR, 'Unknown event: ' + event, 'addEventListener');
        } else {
            if (!listeners[event]) listeners[event] = new EventListeners(event);
            listeners[event].add(listener);
        }
    };

    ormma.close = function () {
        ormmaview.close();
    };

    ormma.expand = function (URL) {
        var props = ormma.getExpandProperties();
        if (( typeof props.height == "undefined" ) || ( props.height == null ) || ( isNaN(props.height) ) || ( props.height < 0 )) {
            props.height = ormma.getScreenSize().height;
        }
        if (( typeof props.width == "undefined" ) || ( props.width == null ) || ( isNaN(props.width) ) || ( props.width < 0 )) {
            props.width = ormma.getScreenSize().width;
        }
        var size = {width:props.width, height:props.height};
        broadcastEvent(EVENTS.INFO, 'expanding to ' + stringify(size));
        ormmaview.ormmaExpand(URL);
    };

    ormma.getDefaultPosition = function () {
        return clone(defaultPosition);
    };

    ormma.getExpandProperties = function () {
		// Provide by default the screenSize if the width or height is not set
        var properties = clone(ormmaview.getExpandProperties());
        if (isNaN(properties.width)) {
            properties.width = screenSize.width;
        }
        if (isNaN(properties.height)) {
            properties.height = screenSize.height;
        }
        return properties;
    };

    ormma.getMaxSize = function () {
        return clone(maxSize);
    };

    ormma.getSize = function () {
        return clone(size);
    };

    ormma.getState = function () {
        return state;
    };

    ormma.getVersion = function () {
        return ("1.1.0");
    };

    ormma.hide = function () {
        if (state == STATES.HIDDEN) {
            broadcastEvent(EVENTS.ERROR, 'Ad is currently hidden.', 'hide');
        } else {
            ormmaview.hide();
        }
    };

    ormma.open = function (URL, controls) {
        if (!URL) {
            broadcastEvent(EVENTS.ERROR, 'URL is required.', 'open');
        } else {
            ormmaview.open(URL, controls);
        }
    };
	
	ormma.vibrate = function (repeat) {
		ormmaview.vibrate(repeat);
	};


    ormma.openMap = function (POI, fullscreen) {
        if (!POI) {
            broadcastEvent(EVENTS.ERROR, 'POI is required.', 'openMap');
        } else {
            ormmaview.openMap(POI, fullscreen);
        }
    };

    ormma.removeEventListener = function (event, listener) {
        if (!event) {
            broadcastEvent(EVENTS.ERROR, 'Must specify an event.', 'removeEventListener');
        } else {
            if (listener && (!listeners[event] || !listeners[event].remove(listener))) {
                broadcastEvent(EVENTS.ERROR, 'Listener not currently registered for event', 'removeEventListener');
                return;
            } else {
                listeners[event].removeAll();
            }
            if (listeners[event].count == 0) {
                listeners[event] = null;
                delete listeners[event];
            }
        }
    };

    ormma.resize = function (width, height) {
        if (width == null || height == null || isNaN(width) || isNaN(height) || width < 0 || height < 0) {
            broadcastEvent(EVENTS.ERROR, 'Requested size must be numeric values between 0 and maxSize.', 'resize');
        } else if (width > maxSize.width || height > maxSize.height) {
            broadcastEvent(EVENTS.ERROR, 'Request (' + width + ' x ' + height + ') exceeds maximum allowable size of (' + maxSize.width + ' x ' + maxSize.height + ')', 'resize');
        } else {
            ormmaview.resize(width, height);
        }
    };

    ormma.setExpandProperties = function (properties) {
        if (valid(properties, expandPropertyValidators, 'setExpandProperties')) {
            var origProperties = ormma.getExpandProperties();
            for (var prop in properties) {
                origProperties[prop] = properties[prop];
            }
            ormmaview.setExpandProperties(origProperties);
        }
    };

    ormma.getOrientationProperties = function() {
        return ormmaview.getOrientationProperties();
    };

    ormma.setOrientationProperties = function(properties) {
        ormmaview.setOrientationProperties(properties);
    };

    // ormma.setResizeProperties = function(properties) {};

    ormma.show = function () {
        if (state != STATES.HIDDEN) {
            broadcastEvent(EVENTS.ERROR, 'Ad is currently visible.', 'show');
        } else {
            ormmaview.show();
        }
    };

    ormma.useCustomClose = function (val) {
        var props = ormma.getExpandProperties();
        props.useCustomClose = val;
        ormma.setExpandProperties(props);
    }

    ormma.playAudio = function (URL, properties) {
        if (!supports[FEATURES.AUDIO]) {
            broadcastEvent(EVENTS.ERROR, 'Method playAudio not supported by this client.', 'playAudio');
        } else if (!URL || typeof URL != 'string') {
            broadcastEvent(EVENTS.ERROR, 'Request must specify a URL', 'playAudio');
        } else {
            ormmaview.playAudio(URL, properties);
        }
    };


    ormma.playVideo = function (URL, properties) {
        if (!supports[FEATURES.VIDEO]) {
            broadcastEvent(EVENTS.ERROR, 'Method playVideo not supported by this client.', 'playVideo');
        } else if (!URL || typeof URL != 'string') {
            broadcastEvent(EVENTS.ERROR, 'Request must specify a URL', 'playVideo');
        } else {
            ormmaview.playVideo(URL, properties);
        }
    };


    // LEVEL 2
	// ////////////////////////////////////////////////////////////////////
    ormma.createEvent = function (date, title, body) {
        if (!supports[FEATURES.CALENDAR]) {
            broadcastEvent(EVENTS.ERROR, 'Method createEvent not supported by this client.', 'createEvent');
        } else if (!date || typeof date != 'object' || !date.getDate) {
            broadcastEvent(EVENTS.ERROR, 'Valid date required.', 'createEvent');
        } else if (!title || typeof title != 'string') {
            broadcastEvent(EVENTS.ERROR, 'Valid title required.', 'createEvent');
        } else {
            ormmaview.createEvent(date, title, body);
        }
    };

    ormma.getHeading = function () {
        if (!supports[FEATURES.HEADING]) {
            broadcastEvent(EVENTS.ERROR, 'Method getHeading not supported by this client.', 'getHeading');
        }
        return heading;
    };

    ormma.getKeyboard = function () {
        if (!supports[FEATURES.KEYBOARD]) {
            broadcastEvent(EVENTS.ERROR, 'Method getKeyboard not supported by this client.', 'getKeyboard');
        }
        return keyboardState;
    }

    ormma.getLocation = function () {
        if (!supports[FEATURES.LOCATION]) {
            broadcastEvent(EVENTS.ERROR, 'Method getLocation not supported by this client.', 'getLocation');
        }
        return (null == location) ? null : clone(location);
    };

    ormma.getNetwork = function () {
        if (!supports[FEATURES.NETWORK]) {
            broadcastEvent(EVENTS.ERROR, 'Method getNetwork not supported by this client.', 'getNetwork');
        }
        return network;
    };

    ormma.getOrientation = function () {
        if (!supports[FEATURES.ORIENTATION]) {
            broadcastEvent(EVENTS.ERROR, 'Method getOrientation not supported by this client.', 'getOrientation');
        }
        return orientation;
    };

    ormma.getScreenSize = function () {
        if (!supports[FEATURES.SCREEN]) {
            broadcastEvent(EVENTS.ERROR, 'Method getScreenSize not supported by this client.', 'getScreenSize');
        } else {
            return (null == screenSize) ? null : clone(screenSize);
        }
    };

    ormma.getShakeProperties = function () {
        if (!supports[FEATURES.SHAKE]) {
            broadcastEvent(EVENTS.ERROR, 'Method getShakeProperties not supported by this client.', 'getShakeProperties');
        } else {
            return (null == ormmaview.getShakeProperties()) ? null : clone(ormmaview.getShakeProperties());
        }
    };

    ormma.getTilt = function () {
        if (!supports[FEATURES.TILT]) {
            broadcastEvent(EVENTS.ERROR, 'Method getTilt not supported by this client.', 'getTilt');
        } else {
            return (null == tilt) ? null : clone(tilt);
        }
    };

    ormma.makeCall = function (number) {
        if (!supports[FEATURES.PHONE]) {
            broadcastEvent(EVENTS.ERROR, 'Method makeCall not supported by this client.', 'makeCall');
        } else if (!number || typeof number != 'string') {
            broadcastEvent(EVENTS.ERROR, 'Request must provide a number to call.', 'makeCall');
        } else {
            ormmaview.makeCall(number);
        }
    };

    ormma.sendMail = function (recipient, subject, body) {
        if (!supports[FEATURES.EMAIL]) {
            broadcastEvent(EVENTS.ERROR, 'Method sendMail not supported by this client.', 'sendMail');
        } else if (!recipient || typeof recipient != 'string') {
            broadcastEvent(EVENTS.ERROR, 'Request must specify a recipient.', 'sendMail');
        } else {
            ormmaview.sendMail(recipient, subject, body);
        }
    };

    ormma.sendSMS = function (recipient, body) {
        if (!supports[FEATURES.SMS]) {
            broadcastEvent(EVENTS.ERROR, 'Method sendSMS not supported by this client.', 'sendSMS');
        } else if (!recipient || typeof recipient != 'string') {
            broadcastEvent(EVENTS.ERROR, 'Request must specify a recipient.', 'sendSMS');
        } else {
            ormmaview.sendSMS(recipient, body);
        }
    };

    ormma.setShakeProperties = function (properties) {
        if (!supports[FEATURES.SHAKE]) {
            broadcastEvent(EVENTS.ERROR, 'Method setShakeProperties not supported by this client.', 'setShakeProperties');
        } else if (valid(properties, shakePropertyValidators, 'setShakeProperties')) {
            ormmaview.setShakeProperties(properties);
        } else {
            broadcastEvent(EVENTS.ERROR, 'Invalid attributes.', 'setShakeProperties');
        }
    };


    ormma.supports = function (feature) {
        if (supports[feature]) {
            return true;
        } else {
            return false;
        }
    };

    ormma.request = function (uri, display) {
        if (!supports[FEATURES.LEVEL3]) {
            broadcastEvent(EVENTS.ERROR, 'Method request not supported by this client.', 'request');
        } else if (!uri || typeof uri != 'string') {
            broadcastEvent(EVENTS.ERROR, 'URI is required.', 'request');
        } else {
            ormmaview.request(uri, display);
        }
    };

    // MRAID ////////////////////
    mraid.readyTimeout = function () {
        window.clearInterval(intervalID);
        broadcastEvent(EVENTS.ERROR, 'No MRAID ready listener found (timeout of ' + readyTimeout + 'ms occurred)');
    };

    mraid.getVersion = function () {
        return ('2.0');
    };


    mraid.close = ormma.close;
    mraid.getState = ormma.getState;
    mraid.open = ormma.open;
    mraid.addEventListener = ormma.addEventListener;
    mraid.removeEventListener = ormma.removeEventListener;
    mraid.useCustomClose = ormma.useCustomClose;
    mraid.error = ormma.error;
    mraid.supports = ormma.supports;
    mraid.makeCall = ormma.makeCall;
    mraid.sendMail = ormma.sendMail;
    mraid.sendSMS = ormma.sendSMS;
    mraid.createEvent = ormma.createEvent;
    mraid.playVideo = ormma.playVideo;
    mraid.openMap = ormma.openMap;
    mraid.playAudio = ormma.playAudio;
    mraid.getNetwork = ormma.getNetwork;
    mraid.getHeading = ormma.getHeading;
    mraid.getTilt = ormma.getTilt;
    mraid.getOrientation = ormma.getOrientation;
    mraid.getLocation = ormma.getLocation;
    mraid.getKeyboard = ormma.getKeyboard;
    mraid.getScreenSize = ormma.getScreenSize;
    mraid.getShakeProperties = ormma.getShakeProperties;
    mraid.setShakeProperties = ormma.setShakeProperties;

    mraid.getSize = ormma.getSize;
    mraid.getMaxSize = ormma.getMaxSize;
    mraid.getScreenSize = ormma.getScreenSize;
    mraid.getDefaultPosition = ormma.getDefaultPosition;
    mraid.show = ormma.show;
    mraid.hide = ormma.hide;

    mraid.storePicture = function (url) {
        ormmaview.storePicture(url);
    }

    mraid.createCalendarEvent = function (w3c_calendar_params) {
        if (!supports[FEATURES.CALENDAR]) {
            broadcastEvent(EVENTS.ERROR, 'Method createCalendarEvent not supported by this client.', 'createCalendarEvent');
        } else {
            ormmaview.createCalendarEvent(w3c_calendar_params);
        }
    }

    ormma.storePicture = mraid.storePicture;

    // MRAID 2.0 additions

    ormma.getCurrentPosition = function () {
        return clone(currentPosition);
    }

    mraid.getCurrentPosition = ormma.getCurrentPosition;

    // extended by adtech ///////////////

    // placement type handling
    var PLACEMENT_TYPES = mraid.PLACEMENT_TYPES = {
        INLINE:'inline',
        INTERSTITIAL:'interstitial'
    };


    // placementType add-ons //////
    mraid.placementType = null;

    mraid.getPlacementType = function () {
        return mraid.placementType;
    }

    ormma.getPlacementType = mraid.getPlacementType;

    mraid.setPlacementType = function (val) {
        if (val == PLACEMENT_TYPES.INLINE || val == PLACEMENT_TYPES.INTERSTITIAL) {
            mraid.placementType = val;
        } else {
            broadcastEvent(EVENTS.ERROR, 'Invalid placementType:' + val + '. Allowed values: ' + PLACEMENT_TYPES.INLINE + ', ' + PLACEMENT_TYPES.INTERSTITIAL);
        }
    }

    ormma.setPlacementType = mraid.setPlacementType;

    // / viewable add-on ///
    mraid.viewable = false;

    mraid.isViewable = function () {
        return mraid.viewable;
    }

    ormma.isViewable = mraid.isViewable;


    ormma.addEventListener('error', function (val) {
        console.log("ERROR: " + val);
    });

    ormma.addEventListener('info', function (val) {
        console.log("INFO: " + val);
    });

})();