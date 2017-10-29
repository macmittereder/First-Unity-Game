using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
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

    private void Update()
    {
        if(cameraDesiredLookAt != null)
        {
            // Adding rotation effect to camera in 3d space
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, 3 * Time.deltaTime);
        }
    }

    private void LoadLevel(string sceneName)
    {
        Debug.Log(sceneName); // Debugging
        SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }
}
