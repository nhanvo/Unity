using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using gcs.FishColoring.ConstantVariable;

public class WelcomeScene : MonoBehaviour {
	public Button fishButton;
	public Button crabButton;
	public Button hippocampusButton;
	public Image imgFishColoring;
	public GameObject btnGroup;
	public GameObject imgFishColoringTarget;

	public GameObject fishOriginal;
	public GameObject crabOriginal;
	public GameObject hippocampusOriginal;

	Vector2 floatY;

	float fishOriginalY;
	float crabOriginalY;
	float hippocampusOriginalY;

	float fishStrength;
	float crabStrength;
	float hippocampusStrength;
	bool isTranslate;
	float speed;

	public float floatStrength;

	// Use this for initialization
	void Start () {
		fishOriginalY = fishOriginal.transform.position.y;
		crabOriginalY = crabOriginal.transform.position.y;
		hippocampusOriginalY = hippocampusOriginal.transform.position.y;

		fishStrength = Random.Range (floatStrength / 2, floatStrength);
		crabStrength = Random.Range (floatStrength / 2, floatStrength);
		hippocampusStrength = Random.Range (floatStrength / 2, floatStrength);

		Debug.Log (fishStrength);
		Debug.Log (crabStrength);
		Debug.Log (hippocampusStrength);
		isTranslate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTranslate) {
			if (imgFishColoring.transform.position.y >= imgFishColoringTarget.transform.position.y) {
				Vector3 posImg = imgFishColoring.transform.position;
				Vector3 posBtn = btnGroup.transform.position;
				speed += Time.deltaTime * 10.0f;
				posImg.y -= speed;
				posBtn.y += speed;
				imgFishColoring.transform.position = posImg;
				btnGroup.transform.position = posBtn;
			} else {
				isTranslate = true;
			}
		} else {
			Move (fishButton, fishOriginalY, fishStrength);
			Move (crabButton, crabOriginalY, -crabStrength);
			Move (hippocampusButton, hippocampusOriginalY, hippocampusStrength);
		}
	}

	private void Move( Button btn, float originalY, float strength) {
		btn.transform.position = new Vector2 (btn.transform.position.x, originalY + Mathf.Sin (Time.time) * strength);
	}

	public void OnPress(string name) {
		if (name.Equals ("fish_button")) {
			Application.LoadLevel (FishConstance.CAMERA_SCENE);
		} else if (name.Equals("crab_button")) {
			Debug.Log(name);
		} else if (name.Equals("hippocampus_button")) {
			Debug.Log(name);
			}
		}
}
