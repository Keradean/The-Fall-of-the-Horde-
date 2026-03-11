using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
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
