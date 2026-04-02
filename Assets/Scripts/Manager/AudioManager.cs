using Extra;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] public AudioSource menuMusic;
        [SerializeField] public AudioSource levelSelectMusic;
        [SerializeField] public AudioSource[] bgm;

        public AudioSource[] sfx;
        
        private int _currentBGM;
        private bool _playingBGM;
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update()
        {
            IsBGMPlaying();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void PlayMenuMusic()
        {
            StopMusic();
            menuMusic.Play();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void PlayLevelSelectMusic()
        {
            StopMusic();
            levelSelectMusic.Play();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void PlayBGM()
        {
            StopMusic();
            _currentBGM = Random.Range(0, bgm.Length);
            bgm[_currentBGM].Play();
            _playingBGM = true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void StopMusic()
        {
            menuMusic.Stop();
            levelSelectMusic.Stop();

            foreach (var track in bgm)
            {
                track.Stop();
            }
            _playingBGM = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void IsBGMPlaying()
        {
            if (!_playingBGM) return;
            // Wenn die Musik nicht mehr spielt, gehe zum nächsten element
            if (bgm[_currentBGM].isPlaying) return;
            _currentBGM++;
            // ist der Array durschgelaufen fange von vorne an
            if (_currentBGM >= bgm.Length)
            {
                _currentBGM = 0;
            }
            bgm[_currentBGM].Play();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void PlaySfx(int sfxToPlay)
        {
            sfx[sfxToPlay].Stop();
            sfx[sfxToPlay].Play();
        }
    }
}
