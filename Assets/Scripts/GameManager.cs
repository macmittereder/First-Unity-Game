using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int currentSkinIndex = 0;
    public int currency = 0;
    public int skinAvailability = 1;

    private void Awake()
    {
        instance = this;
        // Doesn't destroy the Game Manager when changing scenes
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            // There has been a previous session
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            currency = PlayerPrefs.GetInt("Currency");
            skinAvailability = PlayerPrefs.GetInt("SkinAvailability");
        } 
        else
        {
            // Set up new game session
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("SkinAvailability", skinAvailability);
    }
}
