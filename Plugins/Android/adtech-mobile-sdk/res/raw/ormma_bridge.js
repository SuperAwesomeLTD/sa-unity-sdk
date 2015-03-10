/*  Copyright (c) 2011 The ORMMA.org project authors. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license
 *  that can be found in the LICENSE file in the root of the source
 *  tree. All contributing project authors may
 *  be found in the AUTHORS file in the root of the source tree.
 */

(function() {

	var ormmaview = window.ormmaview = {};
	var readyFired = false;

	/** ************************************************* */
	/** ******** PROPERTIES OF THE ORMMA BRIDGE ********* */
	/** ************************************************* */

	/** Expand Properties */
	var expandProperties = {
		width : Number.NaN,
		height : Number.NaN,
		useCustomClose : false,
		isModal : true
	};

	/**
	 * Resize Properties
	 */
	var resizeProperties = null;/*
								 * { width : Number.NaN, height : Number.NaN,
								 * customClosePosition : null, offsetX :
								 * Number.Nan, offsetY : Number.Nan,
								 * allowOffscreen : false };
								 */

	var orientationProperties = null;

	var shakeProperties = {
		'interval' : 0,
		'intensity' : 0
	};

	/** The set of listeners for ORMMA Native Bridge Events */
	var listeners = {};

	/** Holds the current dimension values */
	dimensions: {
	}
	;

	/** A Queue of Calls to the Native SDK that still need execution */
	var nativeCallQueue = [];

	/** Identifies if a native call is currently in progress */
	var nativeCallInFlight = false;

	/** timer for identifying iframes */
	var timer;
	var totalTime;

	/** ******************************************* */
	/** *********** JAVA ENTRY POINTS ************* */
	/** ******************************************* */

	ormmaview.fireUseCustomClose = function() {
		AdtechBridge.useCustomClose(ormmaview.getExpandProperties().useCustomClose);
	};

	/**
	 * Called by the JAVA SDK when an asset has been fully cached.
	 * 
	 * @returns string, "OK"
	 */
	ormmaview.fireAssetReadyEvent = function(alias, URL) {
		var handlers = listeners["assetReady"];
		if (handlers != null) {
			for ( var i = 0; i < handlers.length; i++) {
				handlers[i](alias, URL);
			}
		}

		return "OK";
	};

	/**
	 * Called by the JAVA SDK when an asset has been removed from the cache at
	 * the request of the creative.
	 * 
	 * @returns string, "OK"
	 */
	ormmaview.fireAssetRemovedEvent = function(alias) {
		var handlers = listeners["assetRemoved"];
		if (handlers != null) {
			for ( var i = 0; i < handlers.length; i++) {
				handlers[i](alias);
			}
		}

		return "OK";
	};

	/**
	 * Called by the JAVA SDK when an asset has been automatically removed from
	 * the cache for reasons outside the control of the creative.
	 * 
	 * @returns string, "OK"
	 */
	ormmaview.fireAssetRetiredEvent = function(alias) {
		var handlers = listeners["assetRetired"];
		if (handlers != null) {
			for ( var i = 0; i < handlers.length; i++) {
				handlers[i](alias);
			}
		}

		return "OK";
	};

	/**
	 * Called by the JAVA SDK when various state properties have changed.
	 * 
	 * @returns string, "OK"
	 */
	ormmaview.fireChangeEvent = function(properties) {

		if (!this.readyFired) {
			for (var property in properties) {
				if (property == "ready") {
					this.readyFired = true;
					break;
				}
			}
		}
	
		var handlers = listeners["change"];
		if (handlers != null) {
			for ( var i = 0; i < handlers.length; i++) {
				handlers[i](properties);
			}
		}

		return "OK";
	};

	/**
	 * Called by the JAVA SDK when an error has occured.
	 * 
	 * @returns string, "OK"
	 */
	ormmaview.fireErrorEvent = function(message, action) {
		var handlers = listeners["error"];
		if (handlers != null) {
			for ( var i = 0; i < handlers.length; i++) {
				handlers[i](message, action);
			}
		}

		return "OK";
	};

	/**
	 * Called by the JAVA SDK when the user shakes the device.
	 * 
	 * @returns string, "OK"
	 */
	ormmaview.fireShakeEvent = function() {
		var handlers = listeners["shake"];
		if (handlers != null) {
			for ( var i = 0; i < handlers.length; i++) {
				handlers[i]();
			}
		}

		return "OK";
	};

	/**
	 * 
	 */
	ormmaview.showAlert = function(message) {
		AdtechBridge.showAlert(message);
	};

	/** ****************************************** */
	/** ******** INTERNALLY USED METHODS ********* */
	/** ****************************************** */

	/**
	 * 
	 */
	ormmaview.zeroPad = function(number) {
		var text = "";
		if (number < 10) {
			text += "0";
		}
		text += number;
		return text;
	}

	/** ************************************************************************ */
	/** ******** LEVEL 0 (not part of spec, but required by public API ********* */
	/** ************************************************************************ */

	/**
	 * 
	 */
	ormmaview.activate = function(event) {
		AdtechBridge.activate(event);
	};

	/**
	 * 
	 */
	ormmaview.addEventListener = function(event, listener) {
		var handlers = listeners[event];
		if (handlers == null) {
			// no handlers defined yet, set it up
			listeners[event] = [];
			handlers = listeners[event];
		}

		// see if the listener is already present
		for ( var handler in handlers) {
			if (listener == handler) {
				// listener already present, nothing to do
				return;
			}
		}

		// not present yet, go ahead and add it
		handlers.push(listener);
	};

	/**
	 * 
	 */
	ormmaview.deactivate = function(event) {
		AdtechBridge.deactivate(event);
	};

	/**
	 * 
	 */
	ormmaview.removeEventListener = function(event, listener) {
		var handlers = listeners[event];
		if (handlers != null) {
			handlers.remove(listener);
		}
	};

	/** ************************** */
	/** ******** LEVEL 1 ********* */
	/** ************************** */

	/**
	 * 
	 */
	ormmaview.close = function() {
		if (!this.readyFired) {
			console.warn("WARNING: Ad closes before ready event is fired");
		}
		try {
			AdtechBridge.close();
		} catch (e) {
			ormmaview.showAlert("close: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.ormmaExpand = function(URL) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad expands before ready event is fired");
		}
		var localExpandProperties = stringify(ormmaview.getExpandProperties());
		try {
			AdtechBridge.expandORMMA(URL, localExpandProperties);
		} catch (e) {
			ormmaview.showAlert("executeNativeExpand: " + e + ", URL = " + URL
					+ ", expandProperties = " + localExpandProperties);
		}
	};

	ormmaview.mraidExpand = function(URL) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad expands before ready event is fired");
		}
		var localExpandProperties = stringify(ormmaview.getExpandProperties());
		try {
			AdtechBridge.expandMRAID2(URL, localExpandProperties);
		} catch (e) {
			ormmaview.showAlert("executeNativeExpand: " + e + ", URL = " + URL
					+ ", expandProperties = " + localExpandProperties);
		}
	}

	/**
	 * 
	 */
	ormmaview.hide = function() {
		try {
			AdtechBridge.hide();
		} catch (e) {
			ormmaview.showAlert("hide: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.open = function(URL, controls) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad opens URL before ready event is fired");
		}
		// the navigation parameter is an array, break it into its parts
		var back = false;
		var forward = false;
		var refresh = false;
		if (controls == null) {
			back = true;
			forward = true;
			refresh = true;
		} else {
			for ( var i = 0; i < controls.length; i++) {
				if ((controls[i] == "none") && (i > 0)) {
					// error
					self
							.fireErrorEvent(
									"none must be the only navigation element present.",
									"open");
					return;
				} else if (controls[i] == "all") {
					if (i > 0) {
						// error
						self
								.fireErrorEvent(
										"none must be the only navigation element present.",
										"open");
						return;
					}

					// ok
					back = true;
					forward = true;
					refresh = true;
				} else if (controls[i] == "back") {
					back = true;
				} else if (controls[i] == "forward") {
					forward = true;
				} else if (controls[i] == "refresh") {
					refresh = true;
				}
			}
		}

		try {
			AdtechBridge.open(URL, back, forward, refresh);
		} catch (e) {
			ormmaview.showAlert("open: " + e);
		}

	};

	/**
	 * 
	 */
	ormmaview.openMap = function(POI, fullscreen) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad opens map before ready event is fired");
		}
		try {
			AdtechBridge.openMap(POI, fullscreen);
		} catch (e) {
			ormmaview.showAlert("openMap: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.playAudio = function(URL, properties) {

		if (!this.readyFired) {
			console.warn("WARNING: Ad plays audio before ready event is fired");
		}
		var autoPlay = false, controls = false, loop = false, inline = false, startStyle = 'normal', stopStyle = 'normal';

		if (properties != null) {

			if ((typeof properties.autoplay != "undefined")
					&& (properties.autoplay != null)) {
				autoPlay = true;
			}

			if ((typeof properties.controls != "undefined")
					&& (properties.controls != null)) {
				controls = true;
			}

			if ((typeof properties.loop != "undefined")
					&& (properties.loop != null)) {
				loop = true;
			}

			if ((typeof properties.inline != "undefined")
					&& (properties.inline != null)) {
				inline = true;
			}

			// TODO check valid values...

			if ((typeof properties.startStyle != "undefined")
					&& (properties.startStyle != null)) {
				startStyle = properties.startStyle;
			}

			if ((typeof properties.stopStyle != "undefined")
					&& (properties.stopStyle != null)) {
				stopStyle = properties.stopStyle;
			}

			if (startStyle == 'normal') {
				inline = true;
			}

			if (inline) {
				autoPlay = true;
				controls = false;
				loop = false;
				stopStyle = 'exit';
			}

			if (loop) {
				stopStyle = 'normal';
				controls = true;
			}

			if (!autoPlay) {
				controls = true;
			}

			if (!controls) {
				stopStyle = 'exit';
			}
		}

		try {
			AdtechBridge.playAudio(URL, autoPlay, controls, loop, inline,
					startStyle, stopStyle);
		} catch (e) {
			ormmaview.showAlert("playAudio: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.playVideo = function(URL, properties) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad plays video before ready event is fired");
		}
		var audioMuted = false, autoPlay = false, controls = false, loop = false, position = [
				-1, -1, -1, -1 ], startStyle = 'normal', stopStyle = 'normal';
		if (properties != null) {

			if ((typeof properties.audio != "undefined")
					&& (properties.audio != null)) {
				audioMuted = true;
			}

			if ((typeof properties.autoplay != "undefined")
					&& (properties.autoplay != null)) {
				autoPlay = true;
			}

			if ((typeof properties.controls != "undefined")
					&& (properties.controls != null)) {
				controls = true;
			}

			if ((typeof properties.loop != "undefined")
					&& (properties.loop != null)) {
				loop = true;
			}

			if ((typeof properties.position != "undefined")
					&& (properties.position != null)) {
				inline = new Array(4);

				inline[0] = properties.position.top;
				inline[1] = properties.position.left;

				if ((typeof properties.width != "undefined")
						&& (properties.width != null)) {
					inline[2] = properties.width;
				} else {
					// TODO ERROR
				}

				if ((typeof properties.height != "undefined")
						&& (properties.height != null)) {
					inline[3] = properties.height;
				} else {
					// TODO ERROR
				}
			}

			if ((typeof properties.startStyle != "undefined")
					&& (properties.startStyle != null)) {
				startStyle = properties.startStyle;
			}

			if ((typeof properties.stopStyle != "undefined")
					&& (properties.stopStyle != null)) {
				stopStyle = properties.stopStyle;
			}

			if (loop) {
				stopStyle = 'normal';
				controls = true;
			}

			if (!autoPlay)
				controls = true;

			if (!controls) {
				stopStyle = 'exit';
			}

			if (position[0] == -1 || position[1] == -1) {
				startStyle = "fullscreen";
			}
		}

		try {
			AdtechBridge.playVideo(URL, audioMuted, autoPlay, controls, loop,
					position, startStyle, stopStyle);
		} catch (e) {
			ormmaview.showAlert("playVideo: " + e);
		}

	};

	/**
	 * 
	 */
	ormmaview.resize = function(width, height) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad calls resize before ready event is fired");
		}
		try {
			AdtechBridge.resize(width, height);
		} catch (e) {
			ormmaview.showAlert("resize: " + e);
		}
	};

	ormmaview.mraidResize = function() {
		if (!this.readyFired) {
			console.warn("WARNING: Ad calls resize before ready event is fired");
		}
		try {
			if (resizeProperties == null && resizeProperties.width != null
					&& resizeProperties.height != null) {
				// fire errorStateChage
				ormmaview.fireErrorEvent(
						"setResizeProperties needs to be called first.",
						"resize");
				return;
			}
			var localResizeProperties = stringify(ormmaview
					.getResizeProperties());
			try {
				AdtechBridge.resize(localResizeProperties);
			} catch (e) {
				ormmaview
						.showAlert("executeNativeExpand: " + e + ", URL = "
								+ URL + ", expandProperties = "
								+ localExpandProperties);
				ormmaview.fireErrorEvent("executeNativeResize: " + e
						+ ", resizeProperties = " + localResizeProperties,
						"resize");
			}
		} catch (e) {
			ormmaview.fireErrorEvent(e, "resize");
		}
	}

	ormmaview.getExpandProperties = function() {
		return expandProperties;
	}
	

	/**
	 * 
	 */
	ormmaview.setExpandProperties = function(properties) {
		expandProperties = properties;
	};

	ormmaview.setOrientationProperties = function(properties) {
		orientationProperties = properties;
	};

	ormmaview.getOrientationProperties = function() {
		return orientationProperties;
	};

	ormmaview.setResizeProperties = function(properties) {
		resizeProperties = properties;
	};

	ormmaview.getResizeProperties = function() {
		return resizeProperties;
	};

	/**
	 * 
	 */
	ormmaview.show = function() {
		if (!this.readyFired) {
			console.warn("WARNING: Ad calls show method before ready event is fired");
		}
		try {
			AdtechBridge.show();
		} catch (e) {
			ormmaview.showAlert("show: " + e);
		}
	};

	/** ************************** */
	/** ******** LEVEL 2 ********* */
	/** ************************** */

	/**
	 * 
	 */
	ormmaview.createEvent = function(date, title, body) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad creates event before ready event is fired");
		}
		var msecs = (date.getTime() - date.getMilliseconds());

		try {
			AdtechBridge.createEvent(msecs.toString(), title, body);
			// ormmaview.createCalendarEvent(body);
		} catch (e) {
			ormmaview.showAlert("createEvent: " + e);
		}

	};

	/**
	 * 
	 */
	ormmaview.makeCall = function(phoneNumber) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad makes a call before ready event is fired");
		}
		try {
			AdtechBridge.makeCall(phoneNumber);
		} catch (e) {
			ormmaview.showAlert("makeCall: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.sendMail = function(recipient, subject, body) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad sends mail before ready event is fired");
		}
		try {
			AdtechBridge.sendMail(recipient, subject, body);
		} catch (e) {
			ormmaview.showAlert("sendMail: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.sendSMS = function(recipient, body) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad sends SMS before ready event is fired");
		}
		try {
			AdtechBridge.sendSMS(recipient, body);
		} catch (e) {
			ormmaview.showAlert("sendSMS: " + e);
		}
	};

	/**
	 * 
	 */
	ormmaview.setShakeProperties = function(properties) {
		shakeProperties = properties;
	};

	ormmaview.getShakeProperties = function() {
		return shakeProperties;
	};

	ormmaview.storePicture = function(url) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad stores picture before ready event is fired");
		}
		
		try {
			AdtechBridge.storePicture(url);
		} catch (e) {
			ormmaview.showAlert("StorePicture error: " + e);
		}
	};

	/** ************************** */
	/** ******** LEVEL 3 ********* */
	/** ************************** */

	/**
	 * 
	 */
	ormmaview.addAsset = function(URL, alias) {

	};
	/**
	 * 
	 */
	ormmaview.request = function(URI, display) {

	};
	/**
	 * 
	 */
	ormmaview.removeAsset = function(alias) {
	};

	ormmaview.show = function() {
		if (!this.readyFired) {
			console.warn("WARNING: Ad calls show method before ready event is fired");
		}
	
		try {
			AdtechBridge.show();
		} catch (e) {
			ormmaview.showAlert("show: " + e);
		}
	};

	ormmaview.getImageSize = function() {
		if (!this.readyFired) {
			console.warn("WARNING: Ad calls getImageSize method before ready event is fired");
		}
		
		try {
			if (document.images.length > 0) {
				var firstImage = document.images[0];
				if (firstImage.width >= 3 && firstImage.height >= 3) {
					AdtechBridge.onImageSize(firstImage.width,
							firstImage.height);
				} else {
					AdtechBridge.onImageSize(0, 0);
				}
			} else {
				AdtechBridge.onImageSize(0, 0);
			}
		} catch (e) {
			ormmaview.showAlert("getImageSize: " + e);
			AdtechBridge.onImageSize(0, 0);
		}
	};

	/** ******************* */
	/** *** MRAID 2.0 **** */
	/** ****************** */
	ormmaview.createCalendarEvent = function(w3cCalendarParams) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad creates calendar event before ready event is fired");
		}
	
		try {
			var calendarRepeatRule = {
				frequency : "weekly",
				interval : 1,
				expires : "2015-11-24T12:00:00-08:00",
				exceptionDates : [ '2012-12-22', '2012-12-29' ],
				daysInWeek : [ 1, 6 ],
				daysInMonth : [ 4, -10 ],
				daysInYear : [ 262, -102 ],
				weeksInMonth : [ 1, -1 ],
				monthsInYear : [ 4, 10 ]
			};
			var params = {
				id : "1987",
				description : "Topic is cat food.",
				location : "Mom's basement, in the corner left to old washing machine.",
				summary : "Bring samples!!",
				start : "2012-10-24T09:00:00-08:00",
				end : "2012-10-24T12:00:00-08:00",
				status : "pending",
				transparency : "transparent",
				recurrence : calendarRepeatRule,
				reminder : "-3600000",
			};

			// w3cCalendarParams = params;
			// code above is for testing
			/*
			 * description, location and summary values can contain any type of
			 * given text. thus we have to escape it before the object is
			 * converted to JSON.
			 */
			w3cCalendarParams.description = escape(w3cCalendarParams.description);
			w3cCalendarParams.location = escape(w3cCalendarParams.location);
			w3cCalendarParams.summary = escape(w3cCalendarParams.summary);
			AdtechBridge.createCalendarEvent(stringify(w3cCalendarParams),
					stringify(w3cCalendarParams.recurrence));
		} catch (e) {
			ormmaview.showAlert("createCalendarEvent: " + e);
		}
	};

	var stringify = function(obj) {
		if (typeof obj == 'object') {
			if (obj.push) {
				var out = [];
				for ( var p = 0; p < obj.length; p++) {
					out.push(obj[p]);
				}
				return '[' + out.join(',') + ']';
			} else {
				var out = [];
				for ( var p in obj) {
					var value = obj[p];
					var nameType = typeof value;
					var s = '\'' + p + '\':';

					if (Object.prototype.toString.call(value) === '[object Array]') {
						// alert('Array: '+value);
						s = s + '[';
						var array = [];
						for ( var i in value) {
							var item = value[i];
							// alert(value+" : "+item);
							var t = typeof item;
							if ((t === "number") || (t === "boolean")) {
								// s = s + item;
								array.push(item);
							} else {
								// s = s + '\'' + item + '\'';
								array.push('\'' + item + '\'');
							}
						}
						s = s + array.join(',') + ']';
					} else if ((nameType === "number")
							|| (nameType === "boolean")) {
						s = s + value;
					} else {
						s = s + '\'' + value + '\'';
					}
					out.push(s);
				}
				return '{' + out.join(',') + '}';
			}
		} else {
			return String(obj);
		}
	};

	ormmaview.vibrate = function(repeat) {
		if (!this.readyFired) {
			console.warn("WARNING: Ad calls vibrate before ready event is fired");
		}
		try {
			AdtechBridge.vibrate(String(repeat));
		} catch (e) {
			ormmaview.showAlert("vibrate: " + e);
		}
	};
	
	/*
	 * var stringify = function(obj) { if (typeof obj == 'object') { if
	 * (obj.push) { var out = []; for (var p = 0; p < obj.length; p++) {
	 * out.push(obj[p]); } return '[' + out.join(',') + ']'; } else { var out =
	 * []; for (var p in obj) { var s = '\''+p+'\':'; var value = obj[p];
	 * nameType = typeof arg[value]; if ((nameType === "number") || (nameType
	 * === "boolean")) { s = s + value; } else { s = s + '\'' + value + '\''; }
	 * 
	 * out.push(s); } return '{' + out.join(',') + '}'; } } else { return
	 * String(obj); } };
	 */

})();
