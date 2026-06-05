using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            Time.timeScale = 1; // wieder aus der Pause raus!
            AudioManager.Instance.PlayMenuMusic();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void NewGame()
        {
            SceneManager.LoadScene("LevelOne");   
        }        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void LevelSelect()
        {
            SceneManager.LoadScene("LevelSelect");   
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void Quit()
        {
            Application.Quit();
            Debug.Log("Raus Hier!!!");
        }
    }
}
