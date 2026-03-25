using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }

    public void NewGame()
    {
     SceneManager.LoadScene("LevelSelect");   
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Raus Hier!!!");
    }
}
