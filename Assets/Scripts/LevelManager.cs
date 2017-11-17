using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        // If the button is on it gets turned off
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
