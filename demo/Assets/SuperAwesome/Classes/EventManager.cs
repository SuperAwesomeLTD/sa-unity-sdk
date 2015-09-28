using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using MiniJSON;
using System.Net;
using System.IO;

namespace SuperAwesome
{
	// event names
	public enum SAEventType {
		NoAd = -1,
		AdFetched = 0,
		AdLoaded,
		viewable_impression,
		AdFailed,
		AdStart,
		AdStop,
		AdResume,
		AdRate,
		UserCanceledParentalGate,
		UserSuccessWithParentalGate,
		UserErrorWithParentalGate
	};

	// do nothing for now
	public class EventManager {
		// singleton specific variables
		private static EventManager instance = null;
		private static readonly object padlock = new object();

		// other variables
		public EventRequest request; // request
		private NetManager nmanager;

		// private constructor
		protected EventManager() {
			this.request = new EventRequest ();
			nmanager = new NetManager ();
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
			this.request.detailValue = -1;
		}

		private Dictionary<string, object> transfromSAEventRequestToDictionary(EventRequest req) {
			Dictionary<string, object> dict = new Dictionary<string, object>();
			if (req != null) {
				dict.Add ("line_item", req.lineItemId);
				dict.Add ("creative", req.creativeId);
				dict.Add ("placement", Int64.Parse (req.placementId));
				if (req.type != SAEventType.NoAd) {
					if (req.type == SAEventType.viewable_impression) {
						dict.Add("type", req.type.ToString());
					}
					else {
						dict.Add ("type", "custom."+req.type.ToString ());
					}
				}
				if (req.detailValue > 0) {
					Dictionary<string, object> mdict = new Dictionary<string, object> ();
					mdict.Add ("value", req.detailValue);
					dict.Add ("details", mdict);
				}
			}
			return dict;
		}

		private void sendRequestWithEvent(EventRequest request) {
			Dictionary<string, object> requestDict = this.transfromSAEventRequestToDictionary (request);
			nmanager.sendPOSTRequest ("/event", requestDict);
		}

		private void sendClickWithEvent(EventRequest request){
			Dictionary<string, object> requestDict = this.transfromSAEventRequestToDictionary (request);
			nmanager.sendPOSTRequest ("/click", requestDict);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		// The main functions that actually send the event
		////////////////////////////////////////////////////////////////////////////////////////////////
		public IEnumerator LogAdFetched(Ad ad) {
			this.request.type = SAEventType.AdFetched;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogAdLoaded(Ad ad) {
			this.request.type = SAEventType.AdLoaded;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogViewableImpression(Ad ad) {
			this.request.type = SAEventType.viewable_impression;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogAdFailed(Ad ad) {
			this.request.type = SAEventType.AdFailed;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogAdStart(Ad ad) {
			this.request.type = SAEventType.AdStart;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogAdStop(Ad ad) {
			this.request.type = SAEventType.AdStop;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogAdResume(Ad ad) {
			this.request.type = SAEventType.AdResume;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogUserCanceledParentalGate(Ad ad) {
			this.request.type = SAEventType.UserCanceledParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogUserSuccessWithParentalGate(Ad ad) {
			this.request.type = SAEventType.UserSuccessWithParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}
		
		public IEnumerator LogUserErrorWithParentalGate(Ad ad) {
			this.request.type = SAEventType.UserErrorWithParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
			yield return null;
		}

		public IEnumerator LogClick(Ad ad) {
			this.request.type = SAEventType.NoAd;
			assignRequestFromResponse (ad);
			sendClickWithEvent (this.request);
			yield return null;
		}

		public IEnumerator LogRating(Ad ad, Int64 value) {
			assignRequestFromResponse (ad);
			this.request.type = SAEventType.AdRate;
			this.request.detailValue = value;
			sendClickWithEvent (this.request);
			yield return null;
		}
	}
}