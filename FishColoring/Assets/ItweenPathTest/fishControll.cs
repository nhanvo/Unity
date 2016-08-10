using UnityEngine;
using System.Collections;

public class fishControll : MonoBehaviour {
	public float time = 1.0f;
	bool bCharFollowingPath;
	Vector3 	origPos;
	Vector3[] 	vecPath;
	int currentVecPath;
	// Use this for initialization
	void Start () {
		bCharFollowingPath = false;
		vecPath = iTweenPath.GetPath ("FishPath");
		currentVecPath = 0;
		origPos = vecPath [currentVecPath];
	}
	
	// Update is called once per frame
	void Update () {
		if (!bCharFollowingPath)
		{
			bCharFollowingPath = true;
			Hashtable ht = new Hashtable();
			ht.Add("path", iTweenPath.GetPath("FishPath"));
			ht.Add("time", 100);
			ht.Add("oncomplete", "Path_OnComplete");
			ht.Add("oncompletetarget", this.gameObject);
			iTween.MoveTo(gameObject, ht);    
		}

		if (Vector3.Distance (transform.position, origPos) < 0.5) {
			currentVecPath ++;
			origPos = vecPath[currentVecPath];
		}

		Vector3 moveDirection = gameObject.transform.position - origPos; 
		Quaternion oldRotation = transform.rotation;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (moveDirection), 4 * Time.deltaTime);
		Quaternion newRotation = new Quaternion (oldRotation.x, oldRotation.y, transform.rotation.z, oldRotation.w);
		transform.rotation = newRotation;
	}
	
	void Path_OnComplete()
	{
		Debug.Log("Done");
		
		bCharFollowingPath = false;
	}
}
