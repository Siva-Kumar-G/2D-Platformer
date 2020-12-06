using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnClick_RestartBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClick_LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void OnClick_Pause()
    {
        Time.timeScale = 0;
    }

    public void OnClick_Play()
    {
        Time.timeScale = 1;
    }

    public void QuitApp()
    {
        Application.Quit();
    }
    
}