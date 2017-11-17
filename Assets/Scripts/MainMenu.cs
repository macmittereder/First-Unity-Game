using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    private const float CAMERA_TRANSITION_SPEED = 3.0f;

    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;

    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;
    public Material playerMaterial;
    public Text currencyText;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    private void Start()
    {
        ChangePlayerSkin(GameManager.Instance.currentSkinIndex);
        currencyText.text = "Currency: " + GameManager.Instance.currency.ToString();

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

        int textureIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("Player");
        foreach(Sprite texture in textures)
        {
            GameObject textureButton = Instantiate(shopButtonPrefab) as GameObject;
            textureButton.GetComponent<Image>().sprite = texture;
            textureButton.transform.SetParent(shopButtonContainer.transform, false);

            int index = textureIndex;
            textureButton.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));

            if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
            {
                textureButton.transform.GetChild(0).gameObject.SetActive(false);
            }

            textureIndex++;
        }
    }

    private void Update()
    {
        if(cameraDesiredLookAt != null)
        {
            // Adding rotation effect to camera in 3d space
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
        }
    }

    private void LoadLevel(string sceneName)
    {
        Debug.Log(sceneName); // Debugging
        SceneManager.LoadScene(sceneName);
    }

    private void ChangePlayerSkin(int index)
    {
        if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
        {
            float x = (index % 4) * 0.25f; // modulus of 4 (how many rows) times 0.25f (division of sprites from image (ever 25% of the row) ) 
            float y = ((int)index / 4) * 0.25f; // now for columns

            if (y == 0.0f)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.5f;
            else if (y == 0.50f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0f;

            playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));

            // Save current skin in registry
            GameManager.Instance.currentSkinIndex = index;
            GameManager.Instance.Save();
        }
        else
        {
            // You do not have the skin do you want to buy it
            int cost = 0;

            if (GameManager.Instance.currency >= cost)
            {
                // Buy skin and remove overlay
                GameManager.Instance.currency -= cost;
                GameManager.Instance.skinAvailability += 1 << index;
                GameManager.Instance.Save();

                shopButtonContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                // Update game currency on screen
                currencyText.text = "Currency: " + GameManager.Instance.currency.ToString();
                // Call function again to set the skin that you bought
                ChangePlayerSkin(index);
            }
        }
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }
}
