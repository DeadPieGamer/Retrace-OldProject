using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject deathPanel1;
    public GameObject deathPanel2;
    private bool pauseGame = false;
    public GameObject pausePanel1;
    public GameObject pausePanel2;
    
    public GameObject mainMenu1;
    public GameObject mainMenu2;
    public GameObject setMenu1;
    public GameObject setMenu2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void CursorSet()
    {
        if (SceneManager.GetActiveScene().name != "MenuMain")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (pausePanel1.activeSelf || setMenu1.activeSelf || mainMenu1.activeSelf || deathPanel1.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void Paused()
    {
        if (pausePanel1.activeSelf || setMenu1.activeSelf)
        {
            pausePanel1.SetActive(false);
            pausePanel2.SetActive(false);
            setMenu1.SetActive(false);
            setMenu2.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ToggleTime();
        }
        else if (!deathPanel1.activeSelf)
        {
            pausePanel1.SetActive(!pauseGame);
            pausePanel2.SetActive(!pauseGame);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ToggleTime();
        }
    }

    public void GameOver()
    {
        // Open deathPanel
        deathPanel1.SetActive(true);
        deathPanel2.SetActive(true);
        // See your mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Za Warudo
        ToggleTime();
    }

    public void ToggleTime()
    {
        pauseGame = !pauseGame;

        if (pauseGame)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Continue()
    {
        ToggleTime();
        pausePanel1.SetActive(false);
        pausePanel2.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Retry()
    {
        ToggleTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        ToggleTime();
        SceneManager.LoadScene("MenuMain");
    }

    public void OpenSettings()
    {
        if (mainMenu1.activeSelf)
        {
            mainMenu1.SetActive(false);
            mainMenu2.SetActive(false);
        }
        if (pausePanel1.activeSelf)
        {
            pausePanel1.SetActive(false);
            pausePanel2.SetActive(false);
        }
        setMenu1.SetActive(true);
        setMenu2.SetActive(true);
    }

    public void BackToPause()
    {
        if (setMenu1.activeSelf)
        {
            setMenu1.SetActive(false);
            setMenu2.SetActive(false);
        }
        pausePanel1.SetActive(true);
        pausePanel2.SetActive(true);
    }

    public void BackToMain()
    {
        if (setMenu1.activeSelf)
        {
            setMenu1.SetActive(false);
            setMenu2.SetActive(false);
        }
        mainMenu1.SetActive(true);
        mainMenu2.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
