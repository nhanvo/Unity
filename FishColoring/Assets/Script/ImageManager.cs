using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using gcs.FishColoring.ConstantVariable;
using gcs.FishColoring.FishObject;

public class ImageManager{

#region PRIVATE
	private Texture2D m_FishSource = null;
	private Fish m_Fish;
	private static ImageManager[] imageManager;
#endregion
	
	// Use this for initialization
	private ImageManager () {
		m_Fish = new Fish ();
	}

	public static ImageManager[] getInstance() {
		if (imageManager == null) {
			imageManager = new ImageManager[FishConstance.NUMBER_FISH];
			
			for (int i = 0; i < FishConstance.NUMBER_FISH; i ++) {
				imageManager[i] = new ImageManager();
			}
		}
		return imageManager;
	}

	public void setFishSource(Texture2D fishSource) {
		m_FishSource = fishSource;
	}

	public Texture2D getFishSource() {
		return m_FishSource;
	}

	public Fish getFish() {
		return m_Fish;
	}

	public void CropAllImage() {
		if (m_FishSource == null) {
			return;
		}
		CropImageExcute (m_FishSource, m_Fish.getNewPath(m_Fish.getHead()), m_Fish.getHead().getRect());

		CropImageExcute (m_FishSource, m_Fish.getNewPath(m_Fish.getBody()), m_Fish.getBody().getRect());

		CropImageExcute (m_FishSource, m_Fish.getNewPath(m_Fish.getTail()), m_Fish.getTail().getRect());

		CropImageExcute (m_FishSource, m_Fish.getNewPath(m_Fish.getPelvicFin()), m_Fish.getPelvicFin().getRect());

		CropImageExcute (m_FishSource, m_Fish.getNewPath(m_Fish.getDorsalFin()), m_Fish.getDorsalFin().getRect());
		for (int i = 0; i < (int)Fish.EYE_COUNT.EYE_MAX; i++) {
			CropImageExcute (m_FishSource, m_Fish.getNewPath(m_Fish.getEyes()[i]), m_Fish.getEyes()[i].getRect());
		}
	}	

	public Texture2D LoadImage(string filePath, Rect rect)
	{
		if (System.IO.File.Exists(filePath))
		{
			byte[] bytes = System.IO.File.ReadAllBytes(filePath);    	
			Texture2D tex = new Texture2D((int)rect.width, (int)rect.height);
			tex.LoadImage(bytes);									 	
			return tex;
		}
		return null;
	}
	
	public void FillAllColor() {
		Texture2D[] textureSource = null;
		Texture2D textureNew = null;
		Texture2D textureOld = null;

		// Head
		textureNew = LoadImage (m_Fish.getNewPath(m_Fish.getHead()), m_Fish.getHead().getRect());
		textureOld = Resources.Load(m_Fish.getHead().getOldPath().Split('.')[0]) as Texture2D;
		textureSource = new Texture2D[2] {textureOld, textureNew};
		TestFillColor (textureSource, m_Fish.getExportPath(m_Fish.getHead()));
		textureSource = null;

		// Body 
		textureNew = LoadImage (m_Fish.getNewPath(m_Fish.getBody()), m_Fish.getBody().getRect());
		textureOld = Resources.Load(m_Fish.getBody().getOldPath().Split('.')[0]) as Texture2D;
		textureSource = new Texture2D[2] {textureOld, textureNew};
		TestFillColor (textureSource, m_Fish.getExportPath(m_Fish.getBody()));
		textureSource = null;

		// Tail
		textureNew = LoadImage (m_Fish.getNewPath(m_Fish.getTail()), m_Fish.getTail().getRect());
		textureOld = Resources.Load(m_Fish.getTail().getOldPath().Split('.')[0]) as Texture2D;
		textureSource = new Texture2D[2] {textureOld, textureNew};
		TestFillColor (textureSource, m_Fish.getExportPath(m_Fish.getTail()));
		textureSource = null;

		// Pelvic Fin 
		textureNew = LoadImage (m_Fish.getNewPath(m_Fish.getPelvicFin()), m_Fish.getPelvicFin().getRect());
		textureOld = Resources.Load(m_Fish.getPelvicFin().getOldPath().Split('.')[0]) as Texture2D;
		textureSource = new Texture2D[2] {textureOld, textureNew};
		TestFillColor (textureSource, m_Fish.getExportPath(m_Fish.getPelvicFin()));
		textureSource = null;

		// Dorsal Fin 
		textureNew = LoadImage (m_Fish.getNewPath(m_Fish.getDorsalFin()), m_Fish.getDorsalFin().getRect());
		textureOld = Resources.Load(m_Fish.getDorsalFin().getOldPath().Split('.')[0]) as Texture2D;
		textureSource = new Texture2D[2] {textureOld, textureNew};
		TestFillColor (textureSource, m_Fish.getExportPath(m_Fish.getDorsalFin()));
		textureSource = null;

		for (int i = 0; i < (int)Fish.EYE_COUNT.EYE_MAX; i++) {
			textureNew = LoadImage (m_Fish.getNewPath(m_Fish.getEyes()[i]), m_Fish.getEyes()[i].getRect());
			textureOld = Resources.Load(m_Fish.getEyes()[i].getOldPath().Split('.')[0]) as Texture2D;
			textureSource = new Texture2D[2] {textureOld, textureNew};
			TestFillColor (textureSource, m_Fish.getExportPath(m_Fish.getEyes()[i]));
			textureSource = null;
		}
	}
	
	void TestFillColor(Texture2D[] texs, string savePath) {
		Texture2D tex = FillColor (texs[0], texs[1]);
		SaveImage (tex, savePath);
	}
	
	Texture2D FillColor(Texture2D oldTex, Texture2D newTex) {
		int w = oldTex.width;
		int h = oldTex.height;
		
		Color [] oldColor = oldTex.GetPixels (); 
		Color [] newColor = newTex.GetPixels (); 
		
		for (int i = 0; i < w * h; i++) {
			if (oldColor[i] == Color.white) {
				oldColor[i] = newColor[i];
			}
		}
		
		Texture2D retTex = new Texture2D(w, h);
		
		retTex.SetPixels (oldColor);
		retTex.Apply ();
		return retTex;
	}
	
	bool CropImageExcute(Texture2D tex, string savePath, Rect rect) {
		if (tex == null)
			return false;
		Texture2D sprite = CropImage (tex, rect);
		SaveImage (sprite, savePath);
		return true;
	}
	
	Texture2D CropImage(Texture2D tex, Rect rect) {
		Texture2D rettex = new Texture2D((int)rect.width, (int)rect.height);
		
		for (int i = 0; i < rettex.width; i++) {
			for (int j = 0; j < rettex.height; j++) {
				rettex.SetPixel (i, j, tex.GetPixel(i + (int)rect.x, j + (int)rect.y));
			}
		}
		
		rettex.Apply ();
		return rettex;
	}
	
	void SaveImage(Texture2D tex, string savePath) {
		byte[] bytes = tex.EncodeToPNG ();
		
		// Save PNG image
		File.WriteAllBytes(savePath, bytes);
	}	

	public string  GetPersistentDataPath()
	{ 
		return Application.persistentDataPath + "/";  
	}
}
