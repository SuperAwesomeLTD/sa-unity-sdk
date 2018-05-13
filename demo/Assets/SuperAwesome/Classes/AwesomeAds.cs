using UnityEngine;
using System.Collections;

namespace tv {
	namespace superawesome {
		namespace sdk {
			namespace publisher {

				public class AwesomeAds {

#if (UNITY_IPHONE && !UNITY_EDITOR)
					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnityAwesomeAdsInit (bool loggingEnabled);
#endif

					public static void init (bool loggingEnabled) {
#if (UNITY_IPHONE && !UNITY_EDITOR)
						var loggingEnabledL = loggingEnabled;
						AwesomeAds.SuperAwesomeUnityAwesomeAdsInit (loggingEnabledL);
#else
						Debug.Log ("Initialising SDK");
#endif
					}
				}
			}
		}
	}
}

