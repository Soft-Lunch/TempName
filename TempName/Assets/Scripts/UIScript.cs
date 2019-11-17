using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIScript: MonoBehaviour
{
    public GameObject backgroundMenu;
    public GameObject backgroundCredits;

    public string nameScene;
    // Update is called once per frame
    void Update()
    {
        var gamePad = Gamepad.current;

        if (gamePad != null)
        {
            if (backgroundMenu.activeInHierarchy)
            {
                if (gamePad.buttonWest.wasPressedThisFrame)
                    Quit();
                else if (gamePad.buttonNorth.wasPressedThisFrame)
                    ChangeSceneByName(nameScene);
                else if (gamePad.buttonEast.wasPressedThisFrame)
                    Credits();
            }

            else
            {
                if (gamePad.buttonEast.wasPressedThisFrame)
                    Menu();
            }
        }
    }

    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        backgroundMenu.SetActive(false);
        backgroundCredits.SetActive(true);
    }

    public void Menu()
    {
        backgroundMenu.SetActive(true);
        backgroundCredits.SetActive(false);
    }
}
