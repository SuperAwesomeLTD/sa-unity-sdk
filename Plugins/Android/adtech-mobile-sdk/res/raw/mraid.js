if (ormma && ormma.info) {
	ormma.info('mraid.js identification script found');
}
ormmaview.scriptFound = true;

mraid.resize = function() {
    ormmaview.mraidResize();
};

mraid.getResizeProperties = function() {
    return ormmaview.getResizeProperties();
};

mraid.setResizeProperties = function (properties) {
    ormmaview.setResizeProperties(properties);
};

mraid.expand = function (URL) {
    ormmaview.mraidExpand(URL);
};

mraid.vibrate = function (repeat) {
	ormmaview.vibrate(repeat);
};

mraid.getExpandProperties = function () {
	var mraidExpandProperties = {
		width:Number.NaN,
		height:Number.NaN,
		useCustomClose:false,
		isModal:true
    };
	
	var allProps = ormma.getExpandProperties();
	mraidExpandProperties.width = allProps.width;
	mraidExpandProperties.height = allProps.height;
	mraidExpandProperties.useCustomClose = allProps.useCustomClose;
	
	return mraidExpandProperties;
};

mraid.setExpandProperties = ormma.setExpandProperties;

mraid.getOrientationProperties = ormma.getOrientationProperties;
mraid.setOrientationProperties = ormma.setOrientationProperties;
