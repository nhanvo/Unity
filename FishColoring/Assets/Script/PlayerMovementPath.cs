using UnityEngine;
using System.Collections;

public class PlayerMovementPath : MonoBehaviour {
	public Transform[] waypoints;
	
	private Transform currentWaypoint;
	private int currentIndex;
	
	public float moveSpeed = 10.0f;
	public float minDistance = 2.0f;
	
	// Use this for initialization
	void Start () {
		currentWaypoint = waypoints [0];
		currentIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardWaypoint ();
		
		if (Vector3.Distance (currentWaypoint.transform.position, transform.position) < minDistance) {
			++ currentIndex;
			if (currentIndex > waypoints.Length - 1) {
				currentIndex = Random.Range(0, waypoints.Length - 1);
			}
			currentWaypoint = waypoints[currentIndex];
		} 
	}
	
	void MoveTowardWaypoint() {
		Vector3 direction = currentWaypoint.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		transform.position += moveVector;
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), 4 * Time.deltaTime);
	}
}

