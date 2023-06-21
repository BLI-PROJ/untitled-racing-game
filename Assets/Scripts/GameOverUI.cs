using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Exit()
    {
        Application.Quit();

    }
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
