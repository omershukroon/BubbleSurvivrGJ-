using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManagar : MonoBehaviour
{
    public int sceneIndex;
    public GameObject[] FullStars;
    public GameObject[] EmptyStars;
    public GameObject[] Medals;
    public TextMeshProUGUI textComponent;
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void SetEndText(string text)
    {
        if (textComponent != null)
        {
            // שינוי הערך של הטקסט
            textComponent.text = text;
        }
        else
        {
            Debug.LogError("האובייקט נמצא, אבל אין לו קומפוננטה של TextMeshPro.");
        }
    }
    public void SetMedls(int numOfStars)
    {

        if (numOfStars < 2)
        {
            Medals[0].SetActive(true);
            Medals[1].SetActive(false);
            Medals[2].SetActive(false);
        }
        else if (numOfStars == 2)
        {
            Medals[0].SetActive(false);
            Medals[1].SetActive(true);
            Medals[2].SetActive(false);
        }
        else
        {
            Medals[0].SetActive(false);
            Medals[1].SetActive(false);
            Medals[2].SetActive(true);
        }
        
    }
    public void SetStars(int numOfStars)
    {
        for (int i = 0; i < FullStars.Length; i++)
        {
            if (i < numOfStars)
            {
                FullStars[i].SetActive(true);
            }
            else
            {
                FullStars[i].SetActive(false);
            }
        }
    }

    public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadScene(sceneIndex); //Load Current Level Scene
    }
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(1); //Load Levels Menu Scene
    }

}
