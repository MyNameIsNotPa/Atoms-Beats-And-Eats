using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData {

    static public void setHighScore(string level, int score)
    {
        if (score > PlayerPrefs.GetInt(level + " High Score"))
        {
            PlayerPrefs.SetInt(level + " High Score", score);
        }
        if (score >= 70)
        {
            if (level == "Monday")
            {
                unlockLevel("Tuesday");
            }
            else if (level == "Tuesday")
            {
                unlockLevel("Wednesday");
            }
            else if (level == "Wednesday")
            {
                unlockLevel("Thursday");
            }
            else if (level == "Thursday")
            {
                unlockLevel("Friday");
            }
        }
    }

    static public void unlockLevel(string level)
    {
        PlayerPrefs.SetInt(level + " Unlocked", 1);
        if (isUnlocked("Friday"))
        {
            PlayerPrefs.SetInt("Number of Levels Unlocked", 5);
        }
        else if (isUnlocked("Thursday"))
        {
            PlayerPrefs.SetInt("Number of Levels Unlocked", 4);
        }
        else if (isUnlocked("Wednesday"))
        {
            PlayerPrefs.SetInt("Number of Levels Unlocked", 3);
        }
        else if (isUnlocked("Tuesday"))
        {
            PlayerPrefs.SetInt("Number of Levels Unlocked", 2);
        }
    }

    static public bool isUnlocked(string level)
    {
        return PlayerPrefs.GetInt(level + " Unlocked") == 1;
    }

    static public int getHighScore(string level)
    {
        return PlayerPrefs.GetInt(level + " High Score");
    }
}
