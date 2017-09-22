using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeUI : MonoBehaviour
{
	private Transform dots;
	private Slider slider;
	private GameObject dot;
	private float lastTime;

	private Text headerText;

	private Image preview;
	private int numLeft;

	private Animator animator;

	private List<double> hitTimes;
	private List<Image> sprites;

	private Sprite success;
	private Sprite failure;

	void Start ()
	{
		hitTimes = new List<double> ();
		sprites = new List<Image> ();
		numLeft = 3;
		dots = transform.Find ("BeatIndicator/Slider/Dots");
		headerText = transform.Find ("Header/Text").GetComponent<Text> ();
		slider = GetComponentInChildren<Slider> ();
		dot = Resources.Load<GameObject> ("Prefabs/Dot");
		preview = transform.Find ("Header/Preview").GetComponent<Image> ();
		preview.sprite = Practice.firstRecipe.getSprite();
		animator = GetComponent<Animator> ();

		success = Resources.Load<Sprite> ("RecipeIcons/Success");
		failure = Resources.Load<Sprite> ("RecipeIcons/Failure");
	}

	IEnumerator showPracticeUI()
	{
		yield return new WaitForSeconds (8);
		animator.SetTrigger ("StartPractice");
	}

	public void onSongBegin()
	{
		StartCoroutine (showPracticeUI ());
	}

	public void onSongEnd()
	{
		transform.Find ("Header").gameObject.SetActive (false);
		transform.Find ("BeatIndicator").gameObject.SetActive (false);
	}

	public void onRecipeStart(Recipe recipe, Sprite s)
	{
		float start = (float) recipe.getStartSongTime ();
		hitTimes.Clear ();
		sprites.Clear ();
		foreach (double hit in recipe.getHitTimes())
		{
			hitTimes.Add (hit);
			GameObject newdot = GameObject.Instantiate (dot, dots);
			float offset = ((float)hit) - start;
			float num = -475f + (offset * 475) / 4f;
			newdot.GetComponent<RectTransform> ().localPosition = new Vector3 (num, 0, 0);
			sprites.Add (newdot.GetComponent<Image> ());
		}
	}

	public void onNewRecipe(Recipe recipe)
	{
		preview.sprite = recipe.getSprite ();
	}

	public void onOrderEnd (ORDER_RESULT result)
	{
		if (result != ORDER_RESULT.FAILURE)
		{
			numLeft--;
			if (numLeft == 0)
			{
				numLeft = 3;
			}
		}
		headerText.text = (numLeft > 1) ? numLeft + " more times" : numLeft + " more time";
	}

	public void onHitEvent(HIT_RESULT result)
	{
		if (sprites.Count > 0)
		{
			sprites [0].sprite = result == HIT_RESULT.MISS ? failure : success;
			sprites.RemoveAt (0);
		}
	}

	public void onBeatUpdate(float songTime)
	{
		if (hitTimes.Count > 0 && songTime >= hitTimes [0])
		{
			hitTimes.RemoveAt (0);
			animator.SetTrigger ("BounceHandle");
		}
		slider.value = songTime % 8.0f;
		if (slider.value < lastTime)
		{
			foreach (Transform d in dots)
			{
				GameObject.Destroy (d.gameObject);
			}
		}
		lastTime = slider.value;
	}
}
