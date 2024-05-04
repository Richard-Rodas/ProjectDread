using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _UI;

    private bool isPaused;

    void Start()
    {
        _menu.SetActive(false);
        _UI.SetActive(true);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            if (isPaused)
            {

                AudioListener.pause = true;
            }
            else
            {
                AudioListener.pause = false;
            }
            
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            _menu.SetActive(true);
            _UI.SetActive(false);
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
        }
        else
        {
            Time.timeScale = 1;
            _menu.SetActive(false);
            _UI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        _menu.SetActive(false);
        _UI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        AudioListener.pause = false;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Main Menu");
    }
}
