using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class BannerView : MonoBehaviour {

		public String placementID = "Your Placement ID";

		// Use this for initialization
		void Start () {
			StartCoroutine(loadAd());
		}

		// Update is called once per frame
		void Update () {
//			if (GUIUtility.hotControl==0) { //dont click through GUI
//				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
//					checkTouch(Input.GetTouch(0).position);
//				} else if (Input.GetMouseButtonDown(0)) {
//					checkTouch(Input.mousePosition);
//				}
//			}
		}

		private IEnumerator loadAd(){
			Dictionary<string, object> ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = ad ["creative"] as Dictionary<string, object>;
			Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;
			string imgurl = (string) details["image"];
			Debug.Log (imgurl);
			
			WWW image = new WWW(imgurl);
			yield return image;
			
			Texture2D texture = image.texture;
			SpriteRenderer sr = this.GetComponent<SpriteRenderer>(); 
//			MaterialPropertyBlock block = new MaterialPropertyBlock();
//			block.AddTexture("_MainTex",texture);
//			sr.SetPropertyBlock(block);

			Sprite sprite = new Sprite ();
		}

//		private void checkTouch(Vector2 pos) {
//			Debug.Log ("check touch");
//			if (!gameObject.GetComponent<GUITexture>()) {
//				Ray ray = Camera.main.ScreenPointToRay (pos);
//				RaycastHit hit;
//				
//				if (Physics.Raycast(ray, out hit)) {
//					if (GetComponent<Renderer>().enabled && hit.transform.gameObject == gameObject) {
//						gameObject.SendMessage("adClicked", null, SendMessageOptions.DontRequireReceiver);
//					}
//				}
//			} else {
//				if (GetComponent<GUITexture>().HitTest(pos)) {
//					if (GetComponent<GUITexture>().enabled) {
//						GetComponent<GUITexture>().transform.SendMessage("adClicked", null, SendMessageOptions.DontRequireReceiver);
//					}
//				}
//			}
//		}
//
//		void adClicked() {
//			Debug.Log ("AD CLICKED");
//		}
		

	}
}