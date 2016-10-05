using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace SuperAwesome {
	public class SAGameWall : MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAGameWallCreate ();
		
		
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAGameWallLoad(int placementId,
		                                                           int configuration, 
		                                                           bool test);
		
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAGameWallPlay(int placementId, 
		                                                           bool isParentalGateEnabled);
		
		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySAGameWallHasAdAvailable(int placementId);
#endif

		// private state vars
		private static SAGameWall		 			staticInstance = null;
		private static bool 						isParentalGateEnabled = true;
		private static bool 						isTestingEnabled = false;
		private static bool							isBackButtonEnabled = false;
		private static SAConfiguration 				configuration = SAConfiguration.PRODUCTION;
		private static Action<int, SAEvent>			callback = (p, e) => {};

		// instance constructor
		private static void tryAndCreateOnce () {

			// create just one static instance for ever!
			if (staticInstance == null) {
				GameObject obj = new GameObject ();
				staticInstance = obj.AddComponent<SAGameWall> ();
				staticInstance.name = "SAGameWall";
				DontDestroyOnLoad (staticInstance);
				
#if (UNITY_IPHONE && !UNITY_EDITOR) 
				SAGameWall.SuperAwesomeUnitySAGameWallCreate ();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
				
				var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				
				context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
					var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnity");
					saplugin.CallStatic("SuperAwesomeUnitySAGameWallCreate", context);
				}));
				
#else 
				Debug.Log("SAGameWall Create");
#endif
				
			}
		}

		// Use this for initialization
		void Start () {
			if (this.GetComponent<Image> () != null) {
				Color current = this.GetComponent<Image>().color;
				current.a = 0;
				this.GetComponent<Image>().color = current;
			}
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		////////////////////////////////////////////////////////////////////
		// GameWall specific methods
		////////////////////////////////////////////////////////////////////
		
		public static void load (int placementId) {
			
			// create an instrance of an SAGameWall (for callbacks)
			tryAndCreateOnce ();
			
#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAGameWall.SuperAwesomeUnitySAGameWallLoad(placementId, 
			                                           (int)configuration,
			                                           isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			
			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnity");
				saplugin.CallStatic("SuperAwesomeUnitySAGameWallLoad", 
				                    context, 
				                    placementId, 
				                    (int)configuration, 
				                    isTestingEnabled);
			}));
			
#else
			Debug.Log ("SAGameWall Load");
#endif
			
		}
		
		public static void play (int placementId) {
			
			// create an instrance of an SAGameWall (for callbacks)
			tryAndCreateOnce ();
			
#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAGameWall.SuperAwesomeUnitySAGameWallPlay(placementId,
			                                                 isParentalGateEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			
			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnity");
				saplugin.CallStatic("SuperAwesomeUnitySAGameWallPlay", 
				                    context, 
				                    placementId, 
				                    isParentalGateEnabled,
				                    isBackButtonEnabled);
			}));

#else
			Debug.Log ("SAGameWall Play");
#endif
			
		}
		
		public static bool hasAdAvailable (int placementId) {
			
			// create an instrance of an SAGameWall (for callbacks)
			tryAndCreateOnce ();
			
#if (UNITY_IPHONE && !UNITY_EDITOR) 
			return SAGameWall.SuperAwesomeUnitySAGameWallHasAdAvailable(placementId);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			
			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnity");
			
			return saplugin.CallStatic<bool>("SuperAwesomeUnitySAGameWallHasAdAvailable", 
			                                 context, 
			                                 placementId);
			
#else 
			Debug.Log ("SAGameWall HasAdAvailable");
			return false;
#endif
			return false;
		}

		////////////////////////////////////////////////////////////////////
		// Setters & getters
		////////////////////////////////////////////////////////////////////
		
		public static void setCallback (Action<int, SAEvent> value) {
			callback = value != null ? value : callback;
		}
		
		public static void enableParentalGate () {
			isParentalGateEnabled = true;
		}
		
		public static void disableParentalGate () {
			isParentalGateEnabled = false;
		}
		
		public static void enableTestMode () {
			isTestingEnabled = true;
		}
		
		public static void disableTestMode () {
			isTestingEnabled = false;
		}
		
		public static void setConfigurationProduction () {
			configuration = SAConfiguration.PRODUCTION;
		}
		
		public static void setConfigurationStaging () {
			configuration = SAConfiguration.STAGING;
		}

		public static void enableBackButton () {
			isBackButtonEnabled = true;
		}
		
		public static void disableBackButton () {
			isBackButtonEnabled = false;
		}
		
		////////////////////////////////////////////////////////////////////
		// Native callbacks
		////////////////////////////////////////////////////////////////////
		
		public void nativeCallback(string payload) {
			Dictionary<string, object> payloadDict;
			string type = "";
			int placementId = 0;
			
			// try to get payload and type data
			try {
				payloadDict = Json.Deserialize (payload) as Dictionary<string, object>;
				type = (string) payloadDict["type"];
				string plac = (string) payloadDict["placementId"];
				int.TryParse(plac, out placementId);
			} catch {
				Debug.Log ("Error w/ callback!");
				return;
			}
			
			switch (type) {
			case "sacallback_adLoaded":  callback(placementId, SAEvent.adLoaded); break;
			case "sacallback_adFailedToLoad": callback(placementId, SAEvent.adFailedToLoad); break;
			case "sacallback_adShown": callback(placementId, SAEvent.adShown); break;
			case "sacallback_adFailedToShow": callback (placementId, SAEvent.adFailedToShow); break;
			case "sacallback_adClicked": callback (placementId, SAEvent.adClicked); break;
			case "sacallback_adClosed": callback (placementId, SAEvent.adClosed); break;
			}
		}
	}
}

