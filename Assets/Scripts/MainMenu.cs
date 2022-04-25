using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
   {
       instance = this;
   }
    public static MainMenu instance;
    public string startScene;

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void QuitGame()
        {
            Application.Quit();

        }
}
