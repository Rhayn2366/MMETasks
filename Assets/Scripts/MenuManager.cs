using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool menuActive = false;
    private bool needUpdate = true;

    private void Update()
    {
        UserInput();

        MenuHandling();
    }

    private void MenuHandling()
    {
        if (!needUpdate) return;
        if (menuActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseMenu.SetActive(menuActive);
        needUpdate = false;
    }

    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
            needUpdate = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        menuActive = false;
        needUpdate = true;
    }
}