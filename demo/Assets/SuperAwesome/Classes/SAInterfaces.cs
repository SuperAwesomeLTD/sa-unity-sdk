using UnityEngine;
using System.Collections;

/**
 * Definitions of different interfaces used by AwesomeAds
 */
namespace SuperAwesome {

	/**
	 * SALoader interface defines two main functions that a SDK user might
	 * implement if he wants to preload Ads
	 * This protocol is implemented by a SALoader class delegate
	 */
	public interface SALoaderInterface {
		/**
		 * function that gets called when an Ad is succesfully called
		 * Returns a valid SAAd object
		 */
		void didLoadAd(SAAd ad);

		/**
		 * Function that gets called when an Ad has failed to load
		 * It returns a placementId of the failing ad through callback
		 */
		void didFailToLoadAd(int placementId);
	}

	/**
	 * This interface informs the user about different events in the lifecylce
	 * of a normal Ad;
	 * It has to be implemented as a delegate object by any child of
	 * SAView, meaning any Ad type is valid
	 */
	public interface SAAdInterface {
		/** this function is called when the ad is shown on the screen */
		void adWasShown(int placementId);

		/** this function is called when the ad failed to show */
		void adFailedToShow(int placementId);

		/**
		 * this function is called when an ad is closed;
		 * only applies to fullscreen ads like interstitials and fullscreen videos
		 */
		void adWasClosed(int placementId);

		/** this function is called when an ad is clicked */
		void adWasClicked(int placementId);

		/**
		 * this is called in case of incorrect format - e.g. user calls a video
		 * placement for an interstitial, etc
		 */
		void adHasIncorrectPlacement(int placementId);
	}

	/**
	 * A custom protocol that defines functions that respond to parental gate
	 * actions
	 */
	public interface SAParentalGateInterface {
		/** this function is called when a parental gate pop-up "cancel" button is pressed */
		void parentalGateWasCanceled(int placementId);

		/**
		 * this function is called when a parental gate pop-up "continue" button is
		 * pressed and the parental gate failed (because the numbers weren't OK)
		 */
		void parentalGateWasFailed(int placementId);

		/** 
		 * this function is called when a parental gate pop-up "continue" button is
		 * pressed and the parental gate succedded
		 */
		void parentalGateWasSucceded(int placementId);
	}

	/**
	 * This is the SAVideoProtocol implementation, that defines a series of
	 * functions that might be of interest to SDK users who want to have specific
	 * actions in case of video events
	 */
	public interface SAVideoAdInterface {
		/** fired when an ad has started */
		void adStarted(int placementId);
		
		/** fired when a video ad has started */
		void videoStarted(int placementId);
		
		/** fired when a video ad has reached 1/4 of total duration */
		void videoReachedFirstQuartile(int placementId);
		
		/** fired when a video ad has reached 1/2 of total duration */
		void videoReachedMidpoint(int placementId);
		
		/** fired when a video ad has reached 3/4 of total duration */
		void videoReachedThirdQuartile(int placementId);
		
		/** fired when a video ad has ended */
		void videoEnded(int placementId);
		
		/** fired when an ad has ended */
		void adEnded(int placementId);
		
		/** fired when all ads have ended */
		void allAdsEnded(int placementId);
	}

}

