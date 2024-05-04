using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    private string lastLevelName;
    // Start is called before the first frame update
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

    public void Die()
    {
        // Set the last level name before loading the death screen
        gameOverScreen.SetLastLevel(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("DeathScreen");
    }
}
