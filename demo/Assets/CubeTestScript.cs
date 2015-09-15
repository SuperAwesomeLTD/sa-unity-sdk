using UnityEngine;
using System.Collections;

public class CubeTestScript : MonoBehaviour {

	public int Id = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		// this object was clicked - do something
		Debug.Log (string.Format("Clicked on {0}", Id));
	}
}
