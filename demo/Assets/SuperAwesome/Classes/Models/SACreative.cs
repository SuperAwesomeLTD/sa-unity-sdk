using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome{ 

	/**
	 * The creative contains essential ad information like format, click url
	 * and such
	 */
	public class SACreative {

		/** the creative ID is a unique ID associated by the server with this Ad */
		public int creativeId;
		
		/** name of the creative - set by the user in the dashboard */
		public string name;
		
		/** agreed upon CPM; not really a useful field */
		public int cpm;
		
		/**
	     * the creative format defines the type of ad (image, video, rich media, tag, etc)
	     * and is an enum defined in SACreativeFormat.h
	     */
		public string baseFormat;
		public SACreativeFormat format;
		
		/** the impression URL; not really useful */
		public string impressionURL;
		
		/** the viewable impression URL - used to send impression data to the Ad server */
		public string viewableImpressionURL;
		
		/**
	     * the click URL taken from the Ad server - usually it's used in conjunction with the
	     * tracking URL to form the complete URL
	     */
		public string clickURL;
		
		/** the tracking URL is used to send clicks to the Ad server */
		public string trackingURL;
		
		/**
	     * the fullclick URL is usually trackingURL + clickURL
	     * unless we're dealing with rich-media or 3rd party tags - which the ad server does not always
	     * provide the correct clickURL and we have to obtain it at runtime from the WebView
	     */
		public string fullClickURL;
		
		/**
	     * says if the click URL is valid or not
	     */
		public bool isFullClickURLReliable;
		
		/**
	     * here for completnes' sake - is only used by the AIR SDK (for now)
	     */
		public string videoCompleteURL;
		
		/** must be always true for real ads */
		public bool approved;
		
		/** pointer to a SADetails object containing even more creative information */
		public SADetails details;
		
		public SACreative() {
			/** do nothing */
		}

		/** aux print function */
		public void print() {
			string printout = " \nCREATIVE:\n";
			printout += "\t creativeId: " + creativeId + "\n";
			printout += "\t name: " + name + "\n";
			printout += "\t cpm: " + cpm + "\n";
			printout += "\t baseFormat: " + baseFormat + "\n";
			printout += "\t format: " + format.ToString() + "\n";
			printout += "\t impressionURL: " + impressionURL + "\n";
			printout += "\t viewableImpressionURL: " + viewableImpressionURL + "\n";
			printout += "\t clickURL: " + clickURL + "\n";
			printout += "\t trackingURL: " + trackingURL + "\n";
			printout += "\t fullClickURL: " + fullClickURL + "\n";
			printout += "\t isFullClickURLReliable: " + isFullClickURLReliable + "\n";
			printout += "\t approved: " + approved + "\n";
			Debug.Log(printout);
			details.print();
		}
	}
}