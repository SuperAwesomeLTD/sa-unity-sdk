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
		public SAEventType type { get; set; }
		public Int64 detailValue { get; set; }
		
		public EventRequest() {
			// do nothing
			placementId = null;
			lineItemId = 0;
			creativeId = 0;
			type = SAEventType.NoAd;
			detailValue = -1;
		}
	}
}