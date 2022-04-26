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
    public string level1;
    public string level2;
    public string level3;

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void firstLevel()
    {
        SceneManager.LoadScene(level1);
    }

    public void secondLevel()
    {
        SceneManager.LoadScene(level2);
    }
    
    public void thirdLevel()
    {
        SceneManager.LoadScene(level3);
    }

    public void QuitGame()
        {
            Application.Quit();

        }
}
