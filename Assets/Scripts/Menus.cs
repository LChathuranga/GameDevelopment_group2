
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject pauseMenu;
    public GameObject EndGameMenu;
    public GameObject ObjectiveMenu;

    public static bool GameisStopped = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
                    if(GameisStopped)
                {
                    Resume();
                    Cursor.lockState = CursorLockMode.Locked;
                } 
                else
                {
                    Pause();
                    Cursor.lockState = CursorLockMode.None;
                }
            
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
           if(GameisStopped)
           {
               RemoveObjective();

                Cursor.lockState = CursorLockMode.Locked;
           }
           else
           {
               showObjective();

                Cursor.lockState = CursorLockMode.None;
           }
        }
        
    }

    public void showObjective()
    {
        ObjectiveMenu.SetActive(true);
        Time.timeScale = 0f;
        GameisStopped = true;
    }

    public void RemoveObjective()
    {
        ObjectiveMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameisStopped = false;
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameisStopped = false;

    }

    public void Restart()
    {
       SceneManager.LoadScene("MainMenu");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameisStopped = true;
    }



}
