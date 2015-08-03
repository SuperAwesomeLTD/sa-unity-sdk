using UnityEngine;
using System;

class ParentalGateViewCallback : AndroidJavaProxy {
	// TODO: replace with full Java name for LoginListener interface
	public ParentalGateViewCallback() : base("tv.superawesome.mobile.ParentalGate$ParentalGateViewCallback") { }

	private string clickurl = "";

	public void setUrl(string url) {
		clickurl = url;
	}

	public void onCorrectAnswer() {
		Application.OpenURL(clickurl);
	}
}