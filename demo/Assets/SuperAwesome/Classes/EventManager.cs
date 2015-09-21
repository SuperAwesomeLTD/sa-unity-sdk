using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome
{
	// event names
	public enum SAAdEventName {
		SAEventAdFetched = 0,
		SAEventAdLoaded,
		SAEventAdReady,
		SAEventAdFailed,
		SAEventAdStart,
		SAEventAdStop,
		SAEventAdResume,
		SAEventUserCanceledParentalGate,
		SAEventUserSuccessWithParentalGate,
		SAEventUserErrorWithParentalGate,
		SAEventUserClickedOnAd
	};
	
	// Event types
	public enum SAEventType {
		SAEventTypeImpression = 0,
		SAEventTypeEvent
	};
	
	// Ad Type
	public enum SAAdType {
		SAAdTypeBanner = 0,
		SAAdTypeInterstitial,
		SAAdTypeVideo
	};

	// do nothing for now
	public class EventManager {
		// singleton specific variables
		private static EventManager instance = null;
		private static readonly object padlock = new object();

		// other variables
		public EventRequest request; // request

		// private constructor
		protected EventManager() {
			this.request = new EventRequest ();
			this.request.evttype = SAEventType.SAEventTypeImpression;
		}

		// instance bs
		public static EventManager Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new EventManager();
					}
					return instance;
				}
			}
		}

		// other private functions
		private void assignRequestFromResponse(Ad ad) {
			this.request.creativeId = ad.creativeId;
			this.request.lineItemId = ad.lineItemId;
			this.request.placementId = ad.placementId;
		}

		// transform to dict
		private Dictionary<string, object> transformSAEventRequestToDictionary(EventRequest req) {
			Dictionary<string, object> evdict = new Dictionary<string, object>();
			evdict.Add("line_item", req.lineItemId);
			evdict.Add("creative", req.creativeId);
			evdict.Add("placement", req.placementId);
			evdict.Add("eventname", req.evtname.ToString());
			evdict.Add("evtype", req.evttype.ToString());
			evdict.Add("adtype", req.adtype.ToString());
			return evdict;
		}

		private void sendRequestWithEvent(EventRequest request) {
			Dictionary<string, object> evdict = transformSAEventRequestToDictionary (request);

			foreach (KeyValuePair<string, object> kvp in evdict)
			{
				//textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
				Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		// The main functions that actually send the event
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void LogSAEventAdFetched(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdFetched;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventAdLoaded(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdLoaded;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventAdReady(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdReady;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventAdFailed(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdFailed;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventAdStart(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdStart;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventAdStop(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdStop;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventAdResume(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventAdResume;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventUserCanceledParentalGate(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventUserCanceledParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventUserSuccessWithParentalGate(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventUserSuccessWithParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventUserErrorWithParentalGate(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventUserErrorWithParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogSAEventUserClickedOnAd(Ad ad) {
			this.request.evtname = SAAdEventName.SAEventUserClickedOnAd;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
	}

	public class SABannerEventManager : EventManager {
		// singleton specific variables
		private static SABannerEventManager instance = null;
		private static readonly object padlock = new object();
		
		// private constructor
		SABannerEventManager() {
			this.request.adtype = SAAdType.SAAdTypeBanner;
		}
		
		// instance bs
		public static SABannerEventManager Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new SABannerEventManager();
					}
					return instance;
				}
			}
		}
	}

	public class SAInterstitialEventManager : EventManager {
		// singleton specific variables
		private static SAInterstitialEventManager instance = null;
		private static readonly object padlock = new object();
		
		// private constructor
		SAInterstitialEventManager() {
			this.request.adtype = SAAdType.SAAdTypeInterstitial;
		}
		
		// instance bs
		public static SAInterstitialEventManager Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new SAInterstitialEventManager();
					}
					return instance;
				}
			}
		}
	}

	public class SAVideoEventManager : EventManager {
		// singleton specific variables
		private static SAVideoEventManager instance = null;
		private static readonly object padlock = new object();
		
		// private constructor
		SAVideoEventManager() {
			this.request.adtype = SAAdType.SAAdTypeVideo;
		}
		
		// instance bs
		public static SAVideoEventManager Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new SAVideoEventManager();
					}
					return instance;
				}
			}
		}
	}
}