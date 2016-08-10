using UnityEngine;
using System.Collections;
using gcs.FishColoring.ConstantVariable;

public class FishAmethystController : MonoBehaviour {
	public Animator amethystAnimator = null;
	private AnimatorStateInfo currState;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (amethystAnimator == null)
			return;
		// Current animation state on Base Layer(0)
		currState = animator.GetCurrentAnimatorStateInfo (FishConstance.ANIM_BASE_LAYER);

		// Object translate position from left to right, call animation left for fish amethyst
		if (currState.IsName (FishConstance.FISH_AMETHYST_ANIM_LEFT) == true) {
			amethystAnimator.SetBool(FishConstance.AMETHYST_ANIM_DIRECTION, true);

		// Object translate position from right to left, call animation right for fish amethyst
		} else if (currState.IsName (FishConstance.FISH_AMETHYST_ANIM_RIGHT) == true) {
			amethystAnimator.SetBool(FishConstance.AMETHYST_ANIM_DIRECTION, false);
		}
	}
}
