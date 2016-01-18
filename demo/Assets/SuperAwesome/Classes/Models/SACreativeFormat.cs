using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome{ 

	/**
 	 * An enum that defines the number of formats an ad can be in
	 *  - invalid: defined by the SDK in case of some error
 	 *  - image: the creative is a simple banner image
 	 *  - video: the creative is a video that must be streamed
 	 *  - rich: a mini-HTML page with user interaction
 	 *  - tag: a rich-media (usually) served as a JS file via a 3rd party service
  	 */
	public enum SACreativeFormat {
		invalid = 0,
		image = 1,
		video = 2,
		rich = 3,
		tag = 4
	}
}