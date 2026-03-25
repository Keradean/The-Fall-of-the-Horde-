using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource menuMusic;
    [SerializeField] public AudioSource levelSelectMusic;
    [SerializeField] public AudioSource[] bgm;

    public static AudioManager Instance;

    private int _currentBGM = 0;
    private bool playingBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Play&StopMusik

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }


    public void PlayLevelSelectMusic()
    {
        StopMusic();
        levelSelectMusic.Play();
    }

    public void PlayBGM()
    {
        StopMusic();
        _currentBGM = Random.Range(0, bgm.Length);
        bgm[_currentBGM].Play();
        playingBGM = true;
    }

    private void StopMusic()
    {
        menuMusic.Stop();
        levelSelectMusic.Stop();

        foreach (AudioSource track in bgm)
        {
            track.Stop();
        }

        playingBGM = false;
    }

    #endregion

    #region BackGroundMusic

    public void IsBGMPlaying()
    {
        if (playingBGM)
        {
            // Wenn die Musik nicht mehr spielt gehe zum nächsten element
            if (bgm[_currentBGM].isPlaying == false)
            {
                _currentBGM++;
                // ist der Array durschgelaufen fange von vorne in der Liste an
                if (_currentBGM >= bgm.Length)
                {
                    _currentBGM = 0;
                }

                bgm[_currentBGM].Play();
            }
        }
    }


    #endregion
}
