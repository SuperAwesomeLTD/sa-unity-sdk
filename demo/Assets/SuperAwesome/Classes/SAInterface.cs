using UnityEngine;
using System.Collections;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	public interface SAInterface {
		void SAAdLoaded (int placementId);
		void SAAdFailedToLoad (int placementId);
		void SAAdShown ();
		void SAAdFailedToShow ();
		void SAAdClosed ();
		void SAAdClicked ();
	}

}

