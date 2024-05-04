using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static string lastLevelName;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Call this method to set the last played level before loading the death screen
    public void SetLastLevel(string levelName)
    {
        lastLevelName = levelName;
    }

    // Call this method to retry the last played level
    public void Retry()
    {
        if (!string.IsNullOrEmpty(lastLevelName))
        {
            SceneManager.LoadScene(lastLevelName);
        }
        else
        {
            Debug.LogWarning("Last level name is not set.");
        }
    }
    public void PlayALevel1()
    {
        SceneManager.LoadScene("Level1");
        
    }
    public void PlayALevel2()
    {
        SceneManager.LoadScene("Level2");

    }
    public void PlayALevel3()
    {

        SceneManager.LoadScene("Level3");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        //Debug.Log("Quit!");
        Application.Quit();
    }
}
