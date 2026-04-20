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
