using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;

    public void Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }
}
