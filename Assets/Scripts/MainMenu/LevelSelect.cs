using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LevelSelect : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        void Start()
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
    }
}
