using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Arenas : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChooseArenas()
    {
        SceneManager.LoadScene(1);
    }

    public void Arena1()
    {
        SceneManager.LoadScene(2);
    }
    public void Arena2()
    {
        SceneManager.LoadScene(3);
    }

    public void Arena3()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
