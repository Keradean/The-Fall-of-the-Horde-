using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayLevelSelectMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LevelOne()
    {
        SceneManager.LoadScene("Scenes/TestScene");
    }
}
