using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RenderImage : MonoBehaviour {
	public Text txtCameraStatus;

	public void LoadImage() {
		gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load("image", typeof(Sprite)) as Sprite;
		txtCameraStatus.text = "Image loaded!";
	}
}
