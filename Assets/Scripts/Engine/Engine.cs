﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Provides an interface for the backend to do things in the frontend
// Use the events in this class for visuals and game stuff
// Also be aware of the variable Engine.gamePaused, which will globally keep track of the game being paused

[System.Serializable]
public class BeatUpdateEvent : UnityEvent<float>{};
[System.Serializable]
public class BoolEvent : UnityEvent<bool>{};
[System.Serializable]
public class BeatHitEvent : UnityEvent<HIT_RESULT>{};

[System.Serializable]
public class RecipeStartEvent : UnityEvent<Recipe, Sprite>{};
[System.Serializable]
public class RecipeEndEvent : UnityEvent<bool>{};

[System.Serializable]
public class OrderStartEvent : UnityEvent{};
[System.Serializable]
public class OrderEndEvent : UnityEvent<ORDER_RESULT>{};

[System.Serializable]
public class ShowTextEvent : UnityEvent<string>{};

[System.Serializable]
public class SongEndEvent : UnityEvent{};
[System.Serializable]
public class GameStartEvent : UnityEvent{};

[System.Serializable]
public class PlaySoundEvent : UnityEvent<AudioClip, double>{};

public class Engine : MonoBehaviour
{
	private SongPlayer player;

	[Header("Events")]
	[Space]
	[Header("Recipe Start")]
	public RecipeStartEvent recipeStart;
	[Header("Recipe End")]
	public RecipeEndEvent recipeEnd;
	[Header("Order Start")]
	public OrderStartEvent orderStart;
	[Header("Order End")]
	public OrderEndEvent orderEnd;
	[Header("Hit")]
	public BeatHitEvent hit;
	[Header("Beat Update")]
	public BeatUpdateEvent beatUpdate;
	[Header("Song End")]
	public SongEndEvent songEnd;
	[Header("Game Start")]
	public GameStartEvent gameStart;
	[Header("Play Sound")]
	public PlaySoundEvent soundPlay;

    private static bool keyDisabled;
	private AudioClip failClip;

	public bool levelEnded = false;

	// Game pausing
	//============================================================================================
	public bool gamePaused = false;

	public void Start ()
	{
		failClip = Resources.Load<AudioClip> ("Sounds/Fail");
	}

	public void Update ()
	{
		if (isPauseKeyDown () && (player.isPlaying() || gamePaused))
		{
			gamePaused = !gamePaused;
		}
		// Lock and hide mouse cursor if the game is running
		Cursor.visible = gamePaused;
		Cursor.lockState = gamePaused ? CursorLockMode.None : CursorLockMode.Locked;

		if (levelEnded && Input.anyKey)
		{
			GameObject.FindGameObjectWithTag ("LevelChanger")
				.GetComponent<LevelChanger> ().loadLevel ("LevelSelect");
		}
	}


	// Input state
	//============================================================================================
	public bool isKeyDown()
	{
        if (keyDisabled)
        {
            return false;
        }
        else
        {
            keyDisabled = true;
			return Input.GetButtonDown("Hit") || Input.GetMouseButtonDown(0);
        }
	}
		
	public bool getKeyHeld()
	{
		return Input.GetMouseButton (0) || Input.GetButton("Hit");
	}

	public bool isPauseKeyDown()
	{
		return Input.GetButtonDown ("Pause");
	}
		
	// Event invokers
	//============================================================================================
	public void addOrder(Order order)
	{
		player.getSong ().addOrder (order);
	}

	public void showHitResult(HIT_RESULT result)
	{
		hit.Invoke (result);
	}

	public void showRecipeStart(Recipe recipe, Sprite image)
	{
		recipeStart.Invoke (recipe, image);
	}

	public void showOrderStart()
	{
		orderStart.Invoke ();
	}

	public void showRecipeResult(bool didSucceed)
	{
		recipeEnd.Invoke (didSucceed);
	}

	public void showOrderResult(ORDER_RESULT result)
	{
		orderEnd.Invoke (result);
	}

	public void updateBeat()
	{
        keyDisabled = false;
        beatUpdate.Invoke ((float) player.getSongTime ());
	}

	public void finishSong()
	{
		songEnd.Invoke ();
	}

	public void startGame()
	{
		gameStart.Invoke ();
	}

	public void playSound(AudioClip clip, double time)
	{
		if (clip == null) {
			GetComponent<AudioSource> ().Play ();
		} else {
			soundPlay.Invoke (clip, time);
		}
	}

	// Getters and setters (Used internally by the engine. Don't use.)
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
}