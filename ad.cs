using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace superawesome {
	public class ad : MonoBehaviour {
		public int app_id = 0;
		public int placement_id = 0;
		public enum fixPlacement { NotFixed, Top, Bottom, Center };
		public bool hide = false;
		public bool closable = false;
		public bool parentalGate = false;
		public fixPlacement FixPlacement;
		private bool closeButton = true;
		private string imgurl = "";
		private string clickurl = "";
		private bool adReady = false;
		private string cookie;
		
		IEnumerator Start() {
			
			// hide ad until it is loaded
			if (!gameObject.guiTexture) {
				renderer.enabled = false;
			} else {
				guiTexture.enabled = false;
			}
			
			string dashboard_url = "https://ads.superawesome.tv/v1/unity/ad?app_id="+app_id+"&placement_id="+placement_id;
			
			WWW dashboard_data = new WWW (dashboard_url);
			yield return dashboard_data;
			
			string data = dashboard_data.text;
			var dict = Json.Deserialize(data) as Dictionary<string, object>;

			if (!data.Contains("{")) {
				Debug.Log(data + ": app_id = " + app_id + " | placement_id = " + placement_id);
				yield break;
			}
			
			getUrlsForPlacement (dict);
			
			WWW ad = new WWW(imgurl);
			yield return ad;
			
			Texture2D texture = ad.texture;
			float maxResolutionX = texture.width;
			float maxResolutionY = texture.height;
			
			adReady = true;
			
			if (!gameObject.guiTexture) {
				if (texture.width >= texture.height) {
					transform.localScale = new Vector3 (maxResolutionX * (renderer.bounds.size.x / maxResolutionX),
					                                    maxResolutionY * (renderer.bounds.size.x / maxResolutionX),
					                                    renderer.bounds.size.z);
				} else {
					transform.localScale = new Vector3 (maxResolutionX * (renderer.bounds.size.y / maxResolutionY),
					                                    maxResolutionY * (renderer.bounds.size.y / maxResolutionY),
					                                    renderer.bounds.size.z);
				}
				
				renderer.material.mainTexture = texture;
				if (!hide) renderer.enabled = true;
			} else {
				guiTexture.texture = texture;
				transform.localScale = Vector3.zero;
				
				float x;
				float y;
				float mod;
				
				if (texture.width > texture.height) {
					mod = Screen.width / maxResolutionX;
				} else {
					mod = Screen.height / maxResolutionY;
				}
				
				if (mod > 2) mod = 2;
				
				maxResolutionX = maxResolutionX * mod;
				maxResolutionY = maxResolutionY * mod;
				
				if (FixPlacement == fixPlacement.Top) {
					transform.position = new Vector3(0,0,guiTexture.transform.position.z);
					x = (Screen.width / 2) - (maxResolutionX / 2);
					y = (Screen.height - maxResolutionY);
				} else if (FixPlacement == fixPlacement.Bottom) {
					transform.position = new Vector3(0,0,guiTexture.transform.position.z);
					x = (Screen.width / 2) - (maxResolutionX / 2);
					y = 0;
				} else if (FixPlacement == fixPlacement.Center) {
					transform.position = new Vector3(0,0,guiTexture.transform.position.z);
					x = (Screen.width / 2) - (maxResolutionX / 2);
					y = ((Screen.height / 2) - (maxResolutionY / 2));
				} else {
					x = (guiTexture.transform.position.x - (maxResolutionX / 2));
					y = (guiTexture.transform.position.y - (maxResolutionY / 2));
				}
				
				guiTexture.pixelInset = new Rect(x, y, maxResolutionX, maxResolutionY);
				if (!hide) guiTexture.enabled = true;
			}
		}

		void OnGUI() {
			if (!closable || !gameObject.guiTexture) return;

			Texture2D close = Resources.Load ("close") as Texture2D;

			if (adReady && closeButton) {
				if (GUI.Button (new Rect (guiTexture.pixelInset.x + guiTexture.pixelInset.width - 35, 
				                          Screen.height - guiTexture.pixelInset.y + 5 - guiTexture.pixelInset.height, 
				                          40, 40), close, GUIStyle.none)) {
					guiTexture.enabled = false;
					closeButton = false;
				}
			}
		}
		
		private void getUrlsForPlacement(Dictionary<string, object> dict) {

			imgurl = (string) dict["placement_img"];
			clickurl = WWW.EscapeURL((string) dict["placement_link"]);
			cookie = WWW.EscapeURL((string) dict["cookie"]);
		}
		
		void Update() {
			if (GUIUtility.hotControl==0) { //dont click through GUI
//				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
//					checkTouch(Input.GetTouch(0).position);
//				} else 
				if (Input.GetMouseButtonDown(0)) {
					checkTouch(Input.mousePosition);
				}
			}
		}
		
		private void checkTouch(Vector2 pos) {
			if (!gameObject.guiTexture) {
				Ray ray = Camera.main.ScreenPointToRay (pos);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit)) {
					if (renderer.enabled && hit.transform.gameObject == gameObject) {
						gameObject.SendMessage("adClicked", null, SendMessageOptions.DontRequireReceiver);
					}
				}
			} else {
				if (guiTexture.HitTest(pos)) {
					if (guiTexture.enabled) {
						guiTexture.transform.SendMessage("adClicked", null, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		
		public void toggleAd () {
			hide = !hide;
			
			if (!gameObject.guiTexture && adReady) {
				renderer.enabled = !renderer.enabled;
			} else {
				if (adReady) guiTexture.enabled = !guiTexture.enabled;
			}
		}
		
		public bool isAdReady () {
			return adReady;
		}
		
		void adClicked() {
			StartCoroutine(adRedirect());
		}
		
		IEnumerator adRedirect() {
			string url = "https://ads.superawesome.tv/v1/unity/link?placement_link="+clickurl+"&cookie="+cookie;
			WWW click = new WWW(url);
			yield return click;
			
			string clickThrough = click.text;
			var dict = Json.Deserialize(clickThrough) as Dictionary<string, object>;
			
			if (this.parentalGate && SuperAwesome.openParentalGate((string) dict["link"])) {
				//Parental gate will open link; do nothing.
			} else {
				Application.OpenURL((string) dict["link"]);
			}
		}
		
	}
}