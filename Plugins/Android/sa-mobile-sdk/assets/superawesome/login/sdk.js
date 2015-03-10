//app_name is your app's username
//user is the user object for the client if they are signed in, otherwise null
function SuperAwesome(app_name, user, custom_url) {
    var sa = this;

    sa.url = custom_url || "https://superawesome.club"
    sa.app_name = app_name;
    sa.user = user;

    sa.notification_callbacks = [];
    sa.notification_status_callbacks = [];

    //Used for jsonp
    sa.document_head = document.getElementsByTagName('head')[0];

    var css_code = ".sa_login_button { width: 313px; height: 46px; -webkit-appearance: none; cursor: pointer; border: none; outline: none; background-image: url(" + sa.url + "/images/sign_in_with_sa.png); }";

    var css = document.createElement("style");
    css.type = "text/css";
    if (css.styleSheet){
      css.styleSheet.cssText = css_code;
    } else {
      css.appendChild(document.createTextNode(css_code));
    }

    sa.document_head.appendChild(css);
}

SuperAwesome.prototype.get_current_url = function() {
    var sa = this;

    var address = window.location.protocol + "//" + window.location.hostname;
    if (window.location.port != "") {
        address += ":" + window.location.port;
    }
    address += window.location.pathname;
    if (window.location.search != null) {
        address += window.location.search;
    }

    return address;
}

SuperAwesome.prototype.get_sign_in_url = function(custom_return_url) {
    var sa = this;

    var return_url = custom_return_url || sa.get_current_url();

    return sa.url + "/auth?app=" + encodeURIComponent(encodeURIComponent(sa.app_name)) + "&url=" + encodeURIComponent(encodeURIComponent(return_url));
}

SuperAwesome.prototype.get_sign_up_url = function(custom_return_url) {
    var sa = this;

    var return_url = custom_return_url || sa.get_current_url();

    return sa.url + "/auth?signup=true&app=" + encodeURIComponent(encodeURIComponent(sa.app_name)) + "&url=" + encodeURIComponent(encodeURIComponent(return_url));
}

SuperAwesome.prototype.sign_in_button = function(custom_return_url) {
    var sa = this;

    var button = document.createElement("button");
    button.className = 'sa_login_button';

    //Provide parameter false to disable default click handler
    if(custom_return_url!==false){
        button.onclick = function() {
            window.location = sa.get_sign_in_url(custom_return_url);
        }
    }

    return button;
}

SuperAwesome.prototype.award_points = function(event_id, callback) {
    var sa = this;

}

SuperAwesome.prototype.get_url_params = function() {
    var sa = this;

    var path_name = window.location.pathname;
    if (window.location.search != null) {
        path_name += window.location.search;
    }

    var current_url = path_name;

    var params = {};
    var url_split = current_url.split("?");
    if (url_split.length > 1) {
        var query_string = url_split[1];

        var query_split = query_string.split('&');
        for (var i = 0; i < query_split.length; i++) {
            pair = query_split[i].split('=');
            params[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
    }

    return params;
}

SuperAwesome.prototype.login = function(callback){
    var sa = this;

    var window_top = window.screenTop != undefined ? window.screenTop : screen.top;
    var window_left = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var height = window.innerHeight;
    var width = window.innerWidth;

    var popup_height = 840;
    var popup_width = 670;
    var offset_top = window_top + ((height - popup_height) / 2);
    var offset_left = window_left + ((width - popup_width) / 2);

    var request_id = Math.round(Math.random()*1000000);

    var window_props = "location=yes,top="+offset_top+",left="+offset_left+",height="+popup_height+",width="+popup_width+",scrollbars=yes,status=yes";
    var URL = sa.url+"/auth?app="+encodeURIComponent(sa.app_name)+"&request_id="+request_id;

    var on_close = function(){
        var url = sa.url + sa.jsonp_route(false)+"/get_app_auth_token?request_id="+request_id;
        sa.jsonp(url, function(response){
            callback(response);
        });
    }

    var win = window.open(URL, "_blank", window_props, null, "Sign In", on_close);//on_close is used on mobile

    var interval = setInterval(function(){
        if(win.closed){
            clearInterval(interval);

            on_close();
        }
    },250);
}

SuperAwesome.prototype.jsonp = function(url, callback) {
    var sa = this;

    var jsonp_callback_index = "cb"+Math.round(Math.random() * 1000000);
    var jsonp_callback_name = "SuperAwesome.callbacks."+jsonp_callback_index;

    var script = document.createElement('script');
    if (url.indexOf("?") != -1) {
        script.src = url + "&callback=" + jsonp_callback_name;
    } else {
        script.src = url + "?callback=" + jsonp_callback_name;
    }

    SuperAwesome.callbacks[jsonp_callback_index] = function(data) {
        sa.document_head.removeChild(script);
        callback(data);
        delete SuperAwesome.callbacks[jsonp_callback_index];
    }
    sa.document_head.appendChild(script);
}

SuperAwesome.prototype.jsonp_route = function(include_token,token){
    var sa = this;
    if(include_token){
        if((typeof token) === 'undefined') token = sa.user.app_token;
        return "/jsonp/app/"+encodeURIComponent(sa.app_name)+"/"+encodeURIComponent(token);
    } else {
        return "/jsonp/app/"+encodeURIComponent(sa.app_name);
    }
}

SuperAwesome.prototype.award_points = function(event_id, event_token, callback) {
    var sa = this;

    var url = sa.url + sa.jsonp_route(true) + "/award_points/" + event_id + "?token=" + encodeURIComponent(event_token);
    sa.jsonp(url, callback);
}

/* Call to check with the platform if the user is signed in - this
 * shouldn't be used to automatically sign the user in, but can be
 * used to alert them that they can use SuperAwesome with just one click.
 */
SuperAwesome.prototype.check_sign_in = function(callback) {
    var sa = this;

    var url = sa.url + "/jsonp/is_signed_in";
    sa.jsonp(url, callback);
}


SuperAwesome.prototype.get_user = function(token, callback) {
    var sa = this;

    var url = sa.url + sa.jsonp_route(true,token) + "/token";
    sa.jsonp(url, callback);
}

SuperAwesome.callbacks = {};
SuperAwesome.prototype.init_notifications = function(){
	var sa = this;

	var iframe = $("<iframe />").attr("src", sa.url+"/notifications").css({
		"position": "fixed",
		"top":"0px",
		"right":"50%",
		"z-index": 100,
		"background-color":"white",
		"height": "300px"
	}).hide().appendTo("body");

	var holder = $("<div />").css({
		"position": "fixed",
		"top":"0px",
		"right":"0px",
		"z-index": 100
	}).appendTo("body");

	function listener(event){
		console.log(event.origin);
		if ( event.origin !== sa.url ){
		    return;
		}

		var msg = JSON.parse(event.data);

		var type = msg.type;

		console.log("SA.notifications", type, msg);

		if(type==='alert'){
			var notification = msg.notification;
			if(notification.description){
				var alert_box = $("<a />").attr({
					"href": notification.address
				}).ajax_url().append(
					$("<div />").css({
						"width": "200px",
						"height": "40px",
						"background-color": "rgba(0,0,0,0.5)",
						"color": "white",
						"margin": "5px",
						"padding": "5px"
					}).text(notification.description)
				).hide().slideDown(400)
				.prependTo(holder);

				setTimeout(function(){
					alert_box.slideUp(400, function(){
						alert_box.remove();
					})
				},5000);
			}
		} else if(type==='silent'){
			var notification = msg.notification;
			var should_suppress = false;
	        for(var i = 0; i < sa.notification_callbacks.length; i++){
	        	var notification_callback = sa.notification_callbacks[i];

	        	var response = notification_callback(notification);
	        	should_suppress = should_suppress || response;
	        }

	        iframe[0].contentWindow.postMessage(
	            JSON.stringify({
	            	"type": "suppression_response",
	            	"id": notification.id,
	            	"suppress": should_suppress
	            }),
	            sa.url
	        )
		} else if(type==='status'){
			for(var i = 0; i < sa.notification_status_callbacks.length; i++){
	        	var notification_status_callback = sa.notification_status_callbacks[i];

	        	notification_status_callback(msg);
	        }
		}
	}

	if (window.addEventListener){
		addEventListener("message", listener, false)
	} else {
		attachEvent("onmessage", listener)
	}

}

SuperAwesome.prototype.on_notification = function(callback){
	var sa = this;

	sa.notification_callbacks.push(callback);
}

SuperAwesome.prototype.on_notification_status = function(callback){
	var sa = this;

	sa.notification_status_callbacks.push(callback);
}