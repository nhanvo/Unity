using UnityEngine;
using System.Collections;

public class BackgroundResolution : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.aspect = 1024.0f / 600.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
