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
		AdReady,
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
			this.request.detailValue = -1;
		}

		private Dictionary<string, object> transfromSAEventRequestToDictionary(EventRequest req) {
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict.Add ("line_item", req.lineItemId);
			dict.Add ("creative", req.creativeId);
			dict.Add ("placement", Int64.Parse(req.placementId));
			if (req.type != SAEventType.NoAd) {
				dict.Add ("type", req.type.ToString ());
			}
			if (req.detailValue > 0) {
				Dictionary<string, object> mdict = new Dictionary<string, object>();
				mdict.Add("value", req.detailValue);
				dict.Add("details", mdict);
			}
			return dict;
		}

		private void sendRequestWithEvent(EventRequest request) {
			Dictionary<string, object> requestDict = this.transfromSAEventRequestToDictionary (request);
			NetManager.sendPOSTRequest ("/event", requestDict);
		}

		private void sendClickWithEvent(EventRequest request){
			Dictionary<string, object> requestDict = this.transfromSAEventRequestToDictionary (request);
			NetManager.sendPOSTRequest ("/click", requestDict);
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

		public void LogClick(Ad ad) {
			this.request.type = SAEventType.NoAd;
			assignRequestFromResponse (ad);
			sendClickWithEvent (this.request);
		}

		public void LogRating(Ad ad, Int64 value) {
			assignRequestFromResponse (ad);
			this.request.type = SAEventType.AdRate;
			this.request.detailValue = value;
			sendClickWithEvent (this.request);
		}
	}
}