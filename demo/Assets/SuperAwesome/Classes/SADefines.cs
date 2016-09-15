using UnityEngine;
using System.Collections;

namespace SuperAwesome {

	public enum SAOrientation {
		ANY = 0,
		PORTRAIT = 1,
		LANDSCAPE = 2
	}

	public enum SAEvent {
		adLoaded = 0,
		adFailedToLoad = 1,
		adShown = 2,
		adFailedToShow = 3,
		adClicked = 4,
		adClosed = 5
	}

	public enum SAConfiguration {
		PRODUCTION = 0,
		STAGING = 1
	}
}

