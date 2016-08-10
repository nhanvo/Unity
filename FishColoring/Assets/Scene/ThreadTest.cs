using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThreadTest : MonoBehaviour {
	public Text lblText;
	public Button btn;

	void Start() {

	}
	IEnumerator WaitAndPrint(float waitTime) {

		for (float time = 0.0f; time <= waitTime; time += 0.1f) {
			for (int i = 0; i <= 10; i++)
				print (i.ToString());
			lblText.text = time.ToString();
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void OnClick() {
		StartCoroutine (WaitAndPrint (2));
	}
}
