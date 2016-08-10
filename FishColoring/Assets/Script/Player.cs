using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	enum DIRECTION {
		DIR_NONE = -1,
		DIR_LEFT = 0,
		DIR_RIGHT = 1
	}

	struct PlayerPath {
		Vector3 position;
		int direction;

		public PlayerPath(Vector3 pos, int dir) {
			this.position = pos;
			this.direction = dir;
		}

		public Vector3 getPosition() {
			return this.position;
		}

		public int getDirection() {
			return this.direction;
		}
	}

	private PlayerPath[] playerPath = new PlayerPath[] {
		new PlayerPath (new Vector3 (-900 ,-38, 0), (int)DIRECTION.DIR_LEFT),  //0
		new PlayerPath (new Vector3 (-471 ,-112, 0), (int)DIRECTION.DIR_LEFT),  //1
		new PlayerPath (new Vector3 (-40 ,-89, 0), (int)DIRECTION.DIR_LEFT),  //2
		new PlayerPath (new Vector3 (475 ,125, 0), (int)DIRECTION.DIR_LEFT),  //3
		new PlayerPath (new Vector3 (562 ,-125, 0), (int)DIRECTION.DIR_LEFT),  //4

		new PlayerPath (new Vector3 (318 ,-64, 0), (int)DIRECTION.DIR_RIGHT),  //5
		new PlayerPath (new Vector3 (153 ,133, 0), (int)DIRECTION.DIR_RIGHT),  //6
		new PlayerPath (new Vector3 (-117 ,266, 0), (int)DIRECTION.DIR_RIGHT),  //7
		new PlayerPath (new Vector3 (-345 ,241, 0), (int)DIRECTION.DIR_RIGHT),  //8
		new PlayerPath (new Vector3 (-580 ,-8, 0), (int)DIRECTION.DIR_RIGHT),  //9

		new PlayerPath (new Vector3 (-370 ,-233, 0), (int)DIRECTION.DIR_LEFT),  //10
		new PlayerPath (new Vector3 (-247 ,-32, 0), (int)DIRECTION.DIR_LEFT),  //11
		new PlayerPath (new Vector3 (-139 ,176, 0), (int)DIRECTION.DIR_LEFT),  //12
		new PlayerPath (new Vector3 (86 ,284, 0), (int)DIRECTION.DIR_LEFT),  //13
		new PlayerPath (new Vector3 (337 ,278, 0), (int)DIRECTION.DIR_LEFT),  //14
		new PlayerPath (new Vector3 (450 ,80, 0), (int)DIRECTION.DIR_LEFT),  //15

		new PlayerPath (new Vector3 (287 ,-197, 0), (int)DIRECTION.DIR_RIGHT),  //16
		new PlayerPath (new Vector3 (80 ,-262, 0), (int)DIRECTION.DIR_RIGHT),  //17
		new PlayerPath (new Vector3 (-147 ,-259, 0), (int)DIRECTION.DIR_RIGHT),  //18
		new PlayerPath (new Vector3 (-345 ,-100, 0), (int)DIRECTION.DIR_RIGHT),  //19
		new PlayerPath (new Vector3 (-570 ,61, 0), (int)DIRECTION.DIR_RIGHT),  //20

	};
	private Transform[] waypoints;
	private int[] direction;

	private Transform currentWaypoint;
	private int currentIndex;
	private int currentDirection;

	public float moveSpeed = 10.0f;
	public float minDistance = 2.0f;

	// Use this for initialization
	void Start () {
		waypoints = new Transform[playerPath.Length];
		direction = new int[playerPath.Length];
		for (int i = 0; i < playerPath.Length; i++) {
			waypoints[i] = new GameObject().transform;
			waypoints[i].position = playerPath[i].getPosition();
			direction[i] = playerPath[i].getDirection();
		}

		currentWaypoint = waypoints [0];
		currentIndex = 0;
		currentDirection = direction [0];

		Vector3 vPos = waypoints[0].position;
		vPos.x = Random.Range (waypoints [0].position.x - 300, waypoints [0].position.x);
		vPos.y = Random.Range (0, Screen.height);
		waypoints [0].position = vPos;
	}

	// Update is called once per frame
	void Update () {
		MoveTowardWaypoint ();
		
		if (Vector3.Distance (currentWaypoint.transform.position, transform.position) < minDistance) {
			currentIndex ++;
			if (currentIndex > waypoints.Length - 1) {
				do {
					currentIndex = Random.Range(1, waypoints.Length - 2);
				} while (playerPath[currentIndex].getDirection() == (int)DIRECTION.DIR_RIGHT);
				Debug.Log(currentIndex);
			}
			if (direction[currentIndex] != currentDirection) {
				transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1.0f, 1.0f, 1.0f));
			}
			currentWaypoint = waypoints[currentIndex];
			currentDirection = direction[currentIndex];
		} 
	}

	void MoveTowardWaypoint() {
		Vector3 direction = currentWaypoint.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		transform.position += moveVector;
		Quaternion oldRotation = transform.rotation;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime);
		Quaternion newRotation = new Quaternion (oldRotation.x, oldRotation.y, transform.rotation.z, oldRotation.w);
		transform.rotation = newRotation;
	}

}
