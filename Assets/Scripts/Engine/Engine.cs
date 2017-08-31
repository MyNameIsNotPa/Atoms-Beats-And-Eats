using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Provides an interface for Events to use to display things on the screen

public class Engine : MonoBehaviour
{
	private SongPlayer player;
	private SpriteRenderer recipe;

	private Sprite success;
	private Sprite failure;

	private Animator popper;
	private Animator playerAnimator;

	private int speechHideStack;

	private float lastMeasure = Mathf.Infinity;

	[Header("Debugger Visuals")]
	public Slider beat;
	public Slider measure;

	[Header("Game Visuals")]
	public Text textbox;
	public GameObject speechBubble;
	public Transform customers;
	public GameObject playerObject;

	[Header("Sound")]
	public AudioSource recipeSuccess;
	public AudioSource orderSuccess;

	// Initializer
	public void Start()
	{
		speechHideStack = 0;
		recipe = speechBubble.transform.Find("Image").GetComponent<SpriteRenderer> ();
		popper = speechBubble.GetComponent<Animator> ();
		playerAnimator = playerObject.GetComponent<Animator> ();
		success = Resources.Load<Sprite> ("RecipeIcons/Success");
		failure = Resources.Load<Sprite> ("RecipeIcons/Failure");
	}


	// Update the beat visuals for debugging
	public void DEBUG_updateBeatVisuals()
	{
		beat.value = (float) player.getSongTime () - Mathf.Floor((float) player.getSongTime());
		measure.value = Mathf.Floor ((float)player.getSongTime ()) % 4;
		if (player.getSongTime () >= 0 && measure.value != lastMeasure && measure.value % 2 == 0)
		{
			lastMeasure = measure.value;
			playerAnimator.SetTrigger ("Bounce");
		}
	}


	// Querying state of the engine
	//============================================================================================
	public bool isKeyDown()
	{
		return Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0);
	}

	public bool isPauseKeyDown()
	{
		return Input.GetKeyDown (KeyCode.Escape);
	}


	// Visual updates
	//============================================================================================
	public void showHitResult(HIT_RESULT result)
	{
		Debug.Log (result);
	}

	public void displayText(string text)
	{
		textbox.text = text;
	}

	public void showRecipeStart(Sprite image)
	{
		recipe.sprite = image;
		popper.SetTrigger ("Pop");
	}

	public void showStartOrder()
	{
		speechBubble.SetActive (true);
		speechHideStack++;
	}

	public void showRecipeResult(bool didSucceed)
	{
		recipe.sprite = didSucceed ? success : failure;
		if (didSucceed)
		{
			recipeSuccess.Play ();
		}
		popper.SetTrigger ("Pop");
	}

	public void showOrderResult(bool didSucceed)
	{
		speechHideStack--;
		if (didSucceed)
		{
			orderSuccess.Play ();
		}
		if (speechHideStack == 0)
		{
			speechBubble.SetActive (false);
		}
	}


	// Getters and setters
	//============================================================================================
	public void setPlayer(SongPlayer player)
	{
		this.player = player;
	}

	public int getNearestBeat()
	{
		return Mathf.RoundToInt((float) player.getSongTime ());
	}

	public double getSongTime()
	{
		return player.getSongTime ();
	}

	public double getSecondTime()
	{
		return player.getSecondTime ();
	}

	public double toSecondTime(double time)
	{
		return player.getSong ().toMillisecondTime (time);
	}

	public void addHit(HIT_RESULT result)
	{
		player.addResult (result);
	}

	public GameObject addCustomer(GameObject customer)
	{
		return GameObject.Instantiate (customer, customers);
	}
}