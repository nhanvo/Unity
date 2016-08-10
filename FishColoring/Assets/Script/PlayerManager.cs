using UnityEngine;
using System.Collections;
using gcs.FishColoring.ConstantVariable;
using gcs.FishColoring.FishObject;

public class PlayerManager : MonoBehaviour {

	public float playerSpeed = 0.0f;
	public Transform parent;
	private GameObject[] fishPlayer = new GameObject[FishConstance.NUMBER_FISH];

	private ImageManager[] m_ImageManager;
	// Use this for initialization
	void Start () {
		m_ImageManager = ImageManager.getInstance ();
		for (int i = 0; i < fishPlayer.Length; i++) {
			Object prefab = Resources.Load ("Prefab/FishPlayer", typeof(GameObject));
			fishPlayer[i] = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			fishPlayer[i].transform.SetParent(parent);
			fishPlayer[i].gameObject.SetActive(false);

			if (m_ImageManager[i].getFishSource() != null) {
				fishPlayer[i].gameObject.SetActive(true);

				setupAllSprite (i);
			} 
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void setupSprite(Transform obj, string exportPath, int playerCount) {
		if (!exportPath.Equals("")) {
			SpriteRenderer oldSprite = obj.GetComponent<SpriteRenderer> ();
			Texture2D tex = m_ImageManager[playerCount].LoadImage (exportPath, oldSprite.sprite.rect);
			if (tex == null) {
				Debug.Log ("FishColoring:: Error when load export path.");
				return;
			}
			Vector2 pivot = new Vector2 (oldSprite.sprite.pivot.x / oldSprite.sprite.rect.width,
			                             oldSprite.sprite.pivot.y / oldSprite.sprite.rect.height);
			Sprite newSprite = Sprite.Create (tex,
			                              oldSprite.sprite.rect,
			                              pivot,
			                              oldSprite.sprite.pixelsPerUnit);


			oldSprite.sprite = newSprite;
		}
	}
	
	private void setupAllSprite(int playerCount) {
		Transform[] fishParts = fishPlayer [playerCount].transform.GetComponentsInChildren<Transform> ();
		Fish tmpFish = m_ImageManager[playerCount].getFish ();

		foreach (Transform transform in fishParts) {
			string exportPath = "";
			switch (transform.name) 
			{
			case FishConstance.FISH_HEAD:
				exportPath = tmpFish.getExportPath(tmpFish.getHead());
				break;

			case FishConstance.FISH_BODY:
				exportPath = tmpFish.getExportPath(tmpFish.getBody());
				break;
			case FishConstance.FISH_TAIL:
				exportPath = tmpFish.getExportPath(tmpFish.getTail());
				break;
			case FishConstance.FISH_PELVIC_FIN:
				exportPath = tmpFish.getExportPath(tmpFish.getPelvicFin());
				break;
			case FishConstance.FISH_DORSAL_FIN:
				exportPath = tmpFish.getExportPath(tmpFish.getDorsalFin());
				break;
			case FishConstance.FISH_EYE_ONE:
				exportPath = tmpFish.getExportPath(tmpFish.getEyes()[(int)Fish.EYE_COUNT.EYE_ONE]);
				break;
			case FishConstance.FISH_EYE_TWO:
				exportPath = tmpFish.getExportPath(tmpFish.getEyes()[(int)Fish.EYE_COUNT.EYE_TWO]);
				break;
			}
			if (!exportPath.Equals(""))
				setupSprite (transform, exportPath, playerCount);
		}
	}
}
