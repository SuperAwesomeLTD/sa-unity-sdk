// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using MiniJSON;
using System.Threading;

namespace SuperAwesome {

//	class YeldableObject {

	
//	}

	public class NetManager : MonoBehaviour {

		public NetManager() {
			// constructor
		}

		public void sendPOSTRequest(string endpoint, Dictionary<string, object> POSTdata) {
			// form final URL
			var finalURL = SuperAwesome.instance.getBaseURL () + endpoint;
			
			// form data
			var postData = Json.Serialize (POSTdata);
			var data = System.Text.Encoding.ASCII.GetBytes(postData);
			
			// form post request
			var postRequest = (HttpWebRequest)WebRequest.Create(finalURL);
			postRequest.Method = "POST";
			postRequest.ContentType = "application/json";
			postRequest.ContentLength = data.Length;
			
			// write data
			using (var stream = postRequest.GetRequestStream()) {
				stream.Write(data, 0, data.Length);
			}
			
			// get response
			var response = (HttpWebResponse)postRequest.GetResponse();
			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			
			// log status
			Debug.Log(string.Format("POST to {0} with payload {1} returned {2}", finalURL, postData, responseString));
			
//			yield return null; 
		}

//		public void sendPOSTRequest(string endpoint, Dictionary<string, object> POSTdata) {
//			StartCoroutine (sendAsyncRequest(endpoint, POSTdata));
//		}

	}
}
