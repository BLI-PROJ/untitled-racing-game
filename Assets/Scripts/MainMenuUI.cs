using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject InstructionPanel;
    public GameObject MenuPanel;
    public GameObject OptionPanel;

    public void Start()
    {
        InstructionPanel.SetActive(false);
        OptionPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
    public void Play()
    {
    SceneManager.LoadScene("SampleScene");
    }

    public void Instruction()
    {
        InstructionPanel.SetActive(true);
        MenuPanel.SetActive(false);

    }

    public void ReturnToMenu()
    {
        MenuPanel.SetActive(true);
        InstructionPanel.SetActive(false);
        OptionPanel.SetActive(false);
        
    }

    public void Options()
    {
        OptionPanel.SetActive(true);
        MenuPanel.SetActive(false);

    }

    public void Exit()
    {
       
        Application.Quit();

    }


}
