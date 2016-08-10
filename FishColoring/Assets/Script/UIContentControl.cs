using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using gcs.FishColoring.ConstantVariable;
using System.IO;

public class UIContentControl : MonoBehaviour {
	private const string FISHSCR_FOLDER = "FishSourcePath";
	private const string FISH_SOURCE_IMG = "fish_source_image";
	private static string m_FileLocation;
	private static GameObject[] smallImage;

	void Start() {
		m_FileLocation = UnityCam.getFileLocation ();
		CreateScrollView ();
	}
	
	private void CreateScrollView() {
		Object prefab = Resources.Load ("Prefab/SmallImage", typeof(GameObject));
		smallImage = new GameObject[FishConstance.NUMBER_FISH];
		RectTransform rTrans = (RectTransform) transform.GetComponent<RectTransform>();
		Vector2 size = rTrans.sizeDelta;
		size.y = (smallImage.Length) * 120.0f + 24.0f;
		rTrans.sizeDelta = size;

		for (int i = 0; i < smallImage.Length; i++) {
			smallImage[i] = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			smallImage[i].transform.parent = transform;
			smallImage[i].transform.position = transform.position;
			Vector3 pos = smallImage[i].transform.position;
			pos.x -= 15.0f;
			pos.y -= (i * 120.0f) + 72.0f;
			smallImage[i].transform.position = pos;
			smallImage[i].name += "_" + i.ToString();

			Toggle toggle = smallImage[i].GetComponentInChildren<Toggle>();
			if (toggle) {
				toggle.name += "_" + i.ToString();
			}
			Image image = smallImage [i].GetComponentInChildren<Image> ();
			if (image) {
				image.name += "_" + i.ToString ();
				image.tag = FishConstance.SMALL_IMG_TAG;
				image.sprite = LoadSpriteImage (FISH_SOURCE_IMG + "_" + (i).ToString () + ".png") as Sprite;
			}
		}
	}

	public void onClick() {
		Application.LoadLevel (FishConstance.CAMERA_SCENE);
	}

	private static Sprite LoadSpriteImage(string imageName)
	{
		var filePath = m_FileLocation + FISHSCR_FOLDER + "/" + imageName;
		if (System.IO.File.Exists(filePath))
		{
			byte[] bytes = System.IO.File.ReadAllBytes(filePath);   
			Texture2D tex = new Texture2D(128, 96, TextureFormat.RGBA32, false);
			tex.LoadImage(bytes);
			Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0f, 0f));
			return sprite;
		}
		return null;
	}
}
