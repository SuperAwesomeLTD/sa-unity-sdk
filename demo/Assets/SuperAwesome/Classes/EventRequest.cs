using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome
{
	// the event request class
	public class EventRequest
	{
		public String placementId { get; set; }
		public Int64 lineItemId { get; set; }
		public Int64 creativeId { get; set; }

		public SAAdEventName evtname { get; set; }
		public SAEventType evttype { get; set; }
		public SAAdType adtype { get; set; } 
		
		public EventRequest() {
			// do nothing
		}
	}
}