using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine.UI;
using gcs.FishColoring.ConstantVariable;

public class UnityCam : MonoBehaviour 
{
	public UIContentControl scrollViewContent;
	public Text lblPercent;

	#region private properties
	private Toggle[] toggles;
	private WebCamTexture 		m_WebCamTexture;
	private GameObject 			m_Screen;
	private static string 		m_FileLocation;
	private string 				m_BackCam;
	private string  			m_FrontCam = "Front Camera";
	private int 				m_ImageID;
	private ImageManager[]		m_ImageManager;
	#endregion

	#region constance
	private const string FISHSCR_FOLDER = "FishSourcePath";
	private const string NEW_FOLDER = "NewFishPath";
	private const string OLD_FOLDER = "OldFishPath";
	private const string EXPORT_FOLDER = "ExportFishPath";

	private const string SLASH = "/";
	private const string SCREEN_NAME = "Screen";
	private const string ASSEST_RESOURCE_FISH = "Assets/Resources/FishSource/";
	private const string FISH_SOURCE_IMG = "fish_source_image";
	#endregion

	public static string getFileLocation() {
		return m_FileLocation;
	}

	private void Start() {
		toggles = scrollViewContent.GetComponentsInChildren<Toggle> ();
	}

	private void Awake () 
	{
		m_FileLocation = GetiPhoneDocumentsPath ();		
		CreateAllDirectory ();
		LoadAllImage ();
		// Setup camera
		WebCamDevice[] devices = WebCamTexture.devices;
		m_WebCamTexture = new WebCamTexture(m_BackCam, 640, 480, 30);
		if (devices.Length != 0) {
			m_WebCamTexture.Play ();		
			for (var i = 0; i < devices.Length; i++) {
				if (devices [i].isFrontFacing) {
					m_BackCam = devices [i].name;
					m_WebCamTexture.deviceName = m_FrontCam;
				} else {
					m_BackCam = devices [i].name;
					m_WebCamTexture.deviceName = m_BackCam;
				}
			}

			m_Screen.GetComponent<Renderer> ().material.mainTexture = m_WebCamTexture;

		}

		m_ImageManager = ImageManager.getInstance ();
		for (int i = 0; i < m_ImageManager.Length; i++) {
			m_ImageManager[i].getFish().SetPersistentDataPath(m_FileLocation);
			m_ImageManager[i].getFish().SetNewFishFolderNumber(i.ToString());
			m_ImageManager[i].setFishSource(null);
		}
	}
	
	private string  GetiPhoneDocumentsPath()
	{ 
		string path = Application.persistentDataPath + SLASH;
		return path; 
	}

	private void CreateDirectory(string folder) 
	{		
		string _ImagePath = m_FileLocation + folder + SLASH;
		
		if (Directory.Exists(m_FileLocation + folder)) 
		{
			return;
		}
		
		DirectoryInfo t = new DirectoryInfo(m_FileLocation);		
		t.CreateSubdirectory(folder);
		Debug.Log (_ImagePath);
	}

	private void CreateAllDirectory() {
		CreateDirectory(FISHSCR_FOLDER);

		for (int i = 0; i < FishConstance.NUMBER_FISH; i++) {
			CreateDirectory (NEW_FOLDER + "/" + i.ToString());
			CreateDirectory (EXPORT_FOLDER + "/" + i.ToString());
		}
	}

	private void LoadAllImage() {
		// Retreve persistent data directory of the target platform
		for(int i = 0; i < transform.childCount; ++i)
		{
			GameObject child = transform.GetChild(i).gameObject;
			
			if (child.name.Equals(SCREEN_NAME)) {
					m_Screen = child.gameObject;
			}
		}
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(FishConstance.SMALL_IMG_TAG);
		for (int i = 0; i < gameObjects.Length; i++) {
			Image image = gameObjects[i].GetComponent<Image>();
			if (image.name.Equals("Image_" + i.ToString())) {
				image.sprite = LoadSpriteImage (FISH_SOURCE_IMG + "_" + (i).ToString () + ".png") as Sprite;
			}
		}
	}

	public void OnSelectCapture()
	{
		m_ImageID ++;
		string fileName = FISH_SOURCE_IMG + "_" + m_ImageID.ToString() + ".png";
		Texture2D snap  = new Texture2D(m_WebCamTexture.width,m_WebCamTexture.height); 
		
		Color[] c; 		
		if (m_WebCamTexture.isPlaying) {
			c = m_WebCamTexture.GetPixels ();
			snap.SetPixels (c); 
			snap.Apply ();
			System.IO.File.WriteAllBytes (m_FileLocation + FISHSCR_FOLDER + SLASH + fileName, snap.EncodeToPNG ());
			LoadAllImage ();
		}
	}
	public void OnApply() {
		Texture2D fishSource;
		int toggleCount = 0;
		int totalToggle = 0;
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				totalToggle++;
			}
		}
		StartCoroutine(ImageProcessing(totalToggle));
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn && toggle.name.Equals ("Toggle_" + toggleCount.ToString ())) {
				// Crop and Fill fish part
				fishSource = LoadTexture2DImage (FISH_SOURCE_IMG + "_" + toggleCount.ToString () + ".png");
				m_ImageManager [toggleCount].setFishSource (fishSource);
				m_ImageManager [toggleCount].CropAllImage ();
				m_ImageManager [toggleCount].FillAllColor ();
			} 
			toggleCount ++;
		}
	}

	IEnumerator ImageProcessing(float waitTime) {
		for (int i = 1; i <= waitTime; i++) {
			lblPercent.text = ((i / waitTime) * 100).ToString() + "%";
			yield return new WaitForSeconds (1.0f);
		}

		ChangeScene();		
		yield return null;
	}

	private Texture2D LoadTexture2DImage(string imageName)
	{
		var filePath = m_FileLocation + FISHSCR_FOLDER + SLASH + imageName;
		if (System.IO.File.Exists(filePath))
		{
			byte[] bytes = System.IO.File.ReadAllBytes(filePath);    	// Opens a binary file, reads the contents of the file into a byte array, and then closes the file
			Texture2D tex = new Texture2D(640, 480);
			tex.LoadImage(bytes);									 	// loads a JPG or PNG image from raw byte[] array
			return tex;
		}
		return null;
	}

	private Sprite LoadSpriteImage(string imageName)
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
	
	bool SaveImage(Texture2D tex, string savePath) {
		if (tex == null)
			return false;
		byte[] bytes = tex.EncodeToPNG ();
		
		// Save PNG image
		File.WriteAllBytes(savePath, bytes);
		return true;
	}

	void ChangeScene() {
		if (m_WebCamTexture.isPlaying)
			m_WebCamTexture.Stop ();
		Application.LoadLevel (FishConstance.MAIN_SCENE);
	}
	private void Run()
	{
		Debug.Log ("Hello");
	}
}
