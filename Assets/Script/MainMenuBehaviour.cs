using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{

    public void OnPlayButtonClick()
    {
            SceneManager.LoadScene(1);
    }


    public void OnExitButtonClick()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        // Note: Application.Quit() does not work in the Unity Editor.
        // Use it only in a built application.
    }
}
