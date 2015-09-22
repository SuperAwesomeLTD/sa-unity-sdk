using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome
{
	// event names
	public enum SAEventType {
		AdFetched = 0,
		AdLoaded,
		AdReady,
		AdFailed,
		AdStart,
		AdStop,
		AdResume,
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

		// private constructor
		protected EventManager() {
			this.request = new EventRequest ();
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
			evdict.Add("type", req.type.ToString());
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
		public void LogAdFetched(Ad ad) {
			this.request.type = SAEventType.AdFetched;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogAdLoaded(Ad ad) {
			this.request.type = SAEventType.AdLoaded;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogAdReady(Ad ad) {
			this.request.type = SAEventType.AdReady;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogAdFailed(Ad ad) {
			this.request.type = SAEventType.AdFailed;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogAdStart(Ad ad) {
			this.request.type = SAEventType.AdStart;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogAdStop(Ad ad) {
			this.request.type = SAEventType.AdStop;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogAdResume(Ad ad) {
			this.request.type = SAEventType.AdResume;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogUserCanceledParentalGate(Ad ad) {
			this.request.type = SAEventType.UserCanceledParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogUserSuccessWithParentalGate(Ad ad) {
			this.request.type = SAEventType.UserSuccessWithParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
		
		public void LogUserErrorWithParentalGate(Ad ad) {
			this.request.type = SAEventType.UserErrorWithParentalGate;
			assignRequestFromResponse(ad);
			sendRequestWithEvent(this.request);
		}
	}
}