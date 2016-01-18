using UnityEngine;
using System.Collections;

namespace SuperAwesome {

	public class onBtnClick : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			// do nothing
		}
		
		// Update is called once per frame
		void Update () {
			// do nothing
		}
		
		// button actions
		public void btn1Click () {
			Debug.Log ("load");
		}
		
		public void btn2Click () {
			Debug.Log ("play");
			Debug.Log (SuperAwesome.instance.getBaseURL ());
		}
	}

}

