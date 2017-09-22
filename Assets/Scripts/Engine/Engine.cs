using System.Collections;
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
public class RecipeStartEvent : UnityEvent<Sprite>{};
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
	// Frame Data
	//============================================================================================
	public double frameDown = -1;
	public double frameUp = -1;

	// Game pausing
	//============================================================================================
	public static bool gamePaused = false;

	public void Update ()
	{
		if (isPauseKeyDown ())
		{
			gamePaused = !gamePaused;
		}
		// Sets the frame data values
		if (isKeyDown ()) {
			if (frameDown == -1) {
				frameDown = player.getSecondTime ();
				frameUp = -1;
			}
		} else {
			if (frameDown != -1) {
				frameUp = player.getSecondTime ();
				frameDown = -1;
			}
		}
		// Lock and hide mouse cursor if the game is running
		Cursor.visible = gamePaused;
		Cursor.lockState = gamePaused ? CursorLockMode.None : CursorLockMode.Locked;
	}


	// Input state
	//============================================================================================
	public bool isKeyDown()
	{
		return Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0);
	}

	public bool isPauseKeyDown()
	{
		return Input.GetKeyDown (KeyCode.Escape);
	}

	public double getFrameDown()
	{
		return frameDown;
	}

	public double getFrameUp()
	{
		return frameUp;
	}
	// Event invokers
	//============================================================================================
	public void showHitResult(HIT_RESULT result)
	{
		hit.Invoke (result);
	}

	public void showRecipeStart(Sprite image)
	{
		recipeStart.Invoke (image);
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
		soundPlay.Invoke (clip, time);
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