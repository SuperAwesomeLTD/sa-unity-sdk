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
		adEmpty = 1,
		adFailedToLoad = 2,
		adAlreadyLoaded = 3,
		adShown = 4,
		adFailedToShow = 5,
		adClicked = 6,
		adEnded = 7,
		adClosed = 8
	}

	// enums for different banner specific properties
	public enum SABannerPosition {
		TOP = 0,
		BOTTOM = 1
	}
}

