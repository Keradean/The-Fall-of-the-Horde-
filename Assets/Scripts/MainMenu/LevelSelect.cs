using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LevelSelect : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            AudioManager.Instance.PlayLevelSelectMusic();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void LevelOne()
        {
            SceneManager.LoadScene("Scenes/LevelOne");
        }        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void LevelTwo()
        {
            SceneManager.LoadScene("Scenes/LevelTwo");
        }        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void LevelThree()
        {
            SceneManager.LoadScene("Scenes/LevelThree");
        }
    }
}
