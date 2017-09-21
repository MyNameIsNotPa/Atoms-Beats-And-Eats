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

	void Start ()
	{
		numLeft = 3;
		dots = transform.Find ("BeatIndicator/Slider/Dots");
		headerText = transform.Find ("Header/Text").GetComponent<Text> ();
		slider = GetComponentInChildren<Slider> ();
		dot = Resources.Load<GameObject> ("Prefabs/Dot");
		preview = transform.Find ("Header/Preview").GetComponent<Image> ();
		preview.sprite = Practice.firstRecipe.getSprite();
	}

	public void onSongEnd()
	{
		transform.Find ("Header").gameObject.SetActive (false);
		transform.Find ("BeatIndicator").gameObject.SetActive (false);
	}

	public void onRecipeStart(Recipe recipe, Sprite s)
	{
		float start = (float) recipe.getStartSongTime ();
		foreach (double hit in recipe.getHitTimes())
		{
			GameObject newdot = GameObject.Instantiate (dot, dots);
			float offset = ((float)hit) - start;
			float num = -475f + (offset * 475) / 4f;
			newdot.GetComponent<RectTransform> ().localPosition = new Vector3 (num, 0, 0);
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

	public void onBeatUpdate(float songTime)
	{
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
