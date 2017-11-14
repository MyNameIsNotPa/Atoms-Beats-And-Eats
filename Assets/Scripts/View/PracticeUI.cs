using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeUI : MonoBehaviour
{
	private Transform dots;
	private Slider slider;
    private GameObject dot;
    private GameObject downArrow;
    private GameObject upArrow;
	private GameObject bar;
    private float lastTime;

	private Text headerText;

	private Image preview;
	private int numLeft;

	private Animator animator;

	private List<double> hitTimes;
	private List<Image> sprites;
    private List<string> hitTypes;

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
        dot = Resources.Load<GameObject>("Prefabs/Dot");
		bar = Resources.Load<GameObject> ("Prefabs/Bar");
        downArrow = Resources.Load<GameObject>("Prefabs/DownArrow");
        upArrow = Resources.Load<GameObject>("Prefabs/UpArrow");
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

	IEnumerator endLevel()
	{
		yield return new WaitForSeconds (4);
		GameObject.FindGameObjectWithTag ("LevelChanger")
			.GetComponent<LevelChanger> ().loadLevel ("LevelSelect");
	}

	public void onSongBegin()
	{
		StartCoroutine (showPracticeUI ());
	}

	public void onSongEnd()
	{
		transform.Find ("Header").gameObject.SetActive (false);
		transform.Find ("BeatIndicator").gameObject.SetActive (false);
		StartCoroutine (endLevel ());
	}

	public void onRecipeStart(Recipe recipe, Sprite s)
	{
		float start = (float) recipe.getStartSongTime ();
		hitTimes.Clear ();
		sprites.Clear ();
        hitTypes = recipe.getHitTypes();

		List<double> recipeHitTimes = recipe.getHitTimes ();
		for (int i = 0; i < recipeHitTimes.Count; i++)
		{
			double hit = recipeHitTimes[i];
			hitTimes.Add (hit);
			float offset = ((float)hit) - start;
			float num = -475f + (offset * 475) / 4f;
            GameObject newdot;
			if (hitTypes[i] == "hit")
            {
                newdot = GameObject.Instantiate(dot, dots);
            }
			else if (hitTypes[i] == "holdStart")
            {
				GameObject newbar = GameObject.Instantiate (bar, dots);
				float offset2 = ((float)recipeHitTimes[i + 1]) - start;
				float num2 = -475f + (offset2 * 475) / 4f;
				float dist = offset2 - offset;
				float average = dist / 2.0f;
				float midpos = -475f + ((offset + average) * 475) / 4f;
				float scaledDist = dist;
				newbar.GetComponent<RectTransform>().localPosition = new Vector3(dist == 1 ? -170 : 0, 0, 0);
				newbar.GetComponent<RectTransform> ().localScale = new Vector3 (scaledDist, 1, 1);
				newdot = GameObject.Instantiate(downArrow, dots);
            }
			else// if (hitTypes[hitTypesNum] == "holdEnd")
            {
                newdot = GameObject.Instantiate(upArrow, dots);
            }
            newdot.GetComponent<RectTransform>().localPosition = new Vector3(num, 0, 0);
            sprites.Add(newdot.GetComponent<Image>());
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
            //if the current event is a HoldEvent (meaning the current sprite is a down arrow), change the following up arrow as well
            if (sprites[0].name == GameObject.Instantiate(downArrow).GetComponent<Image>().name)
            {
                sprites[1].sprite = result == HIT_RESULT.MISS ? failure : success;
                sprites.RemoveAt(1);
            }
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
