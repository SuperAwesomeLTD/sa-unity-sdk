using UnityEngine;
using System.Collections;

namespace SuperAwesome {

	public enum SAOrientation {
		ANY = 0,
		PORTRAIT = 1,
		LANDSCAPE = 2
	}

	public enum SAConfiguration {
		PRODUCTION = 0,
		STAGING = 1
	}

	public enum SAEvent {
		adLoaded = 0,
		adFailedToLoad = 1,
		adAlreadyLoaded = 2,
		adShown = 3,
		adFailedToShow = 4,
		adClicked = 5,
		adEnded = 6,
		adClosed = 7
	}

	// enums for different banner specific properties
	public enum SABannerPosition {
		TOP = 0,
		BOTTOM = 1
	}
}

