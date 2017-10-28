using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;

    private void Start()
    {
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach(Sprite thumbnail in thumbnails)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab) as GameObject;
            levelButton.GetComponent<Image>().sprite = thumbnail;
            levelButton.transform.SetParent(levelButtonContainer.transform, false);

            string sceneName = thumbnail.name; // Scene name
            levelButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }
    }

    private void LoadLevel(string sceneName)
    {
        Debug.Log(sceneName); // Debugging
        SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(string menu)
    {

    }
}
