// Taken (and adjusted) from Adam Chandler
// https://github.com/metalac190/ACDev_SoundSystem

using Gummi.Patterns;
using UnityEngine;

namespace Gummi.Audio
{
    /// <summary>
    /// This class is a Singleton that helps maintain consistency across MusicPlayers in the scene.
    /// The main approach is to control 2 'MusicPlayers' that can activate/deactivate and blend with
    /// each other. New 'track layers' can be faded up to create additive music tracks that emulate
    /// stems. This can be useful for building new musical instrument layers based on game events
    /// to increase or decrease intensity.
    /// </summary>
    public class MusicManager : PLazySingleton<MusicManager>
    {
        #region Memebers
        public const int MaxLayers = 3;

        // use 2 music sources so that we can do cross blending
        MusicPlayer _musicPlayer1;
        MusicPlayer _musicPlayer2;
        MusicData _activeSong;

        public int ActiveLayerIndex { get; private set; }
        bool _music1SourcePlaying;
        float _volume = .8f;

        public float Volume
        {
            get => _volume;
            private set => _volume = Mathf.Clamp(value, 0, 1);
        }
        public MusicPlayer ActivePlayer => (_music1SourcePlaying) ? _musicPlayer1 : _musicPlayer2;
        public MusicPlayer InactivePlayer => (_music1SourcePlaying) ? _musicPlayer2 : _musicPlayer1;
        #endregion

        #region Setup
        protected override void Awake()
        {
            base.Awake();
            SetupMusicPlayers();
        }
        
        void SetupMusicPlayers()
        {
            _musicPlayer1 = gameObject.AddComponent<MusicPlayer>();
            _musicPlayer2 = gameObject.AddComponent<MusicPlayer>();

            _musicPlayer1.FadeVolume(0, 0);
            _musicPlayer2.FadeVolume(0, 0);
        }
        #endregion
        
        #region Play
        public void SetVolume(float volume, float fadeTime)
        {
            Volume = volume;
            ActivePlayer.FadeVolume(Volume, fadeTime);
        }

        public void SetLayerLevel(int newLayerIndex, float fadeTime)
        {
            newLayerIndex = Mathf.Clamp(newLayerIndex, 0, MaxLayers-1);
            
            // if the layer changed, don't do anything different
            if (newLayerIndex == ActiveLayerIndex) return;

            ActiveLayerIndex = newLayerIndex;
            SetVolume(Volume, fadeTime);
        }

        public void IncreaseLayerLevel(float fadeTime)
        {
            int newLayerIndex = ActiveLayerIndex + 1;
            newLayerIndex = Mathf.Clamp(newLayerIndex, 0, MaxLayers - 1);

            // if the layer changed, don't do anything different
            if (newLayerIndex == ActiveLayerIndex) return;

            ActiveLayerIndex = newLayerIndex;
            ActivePlayer.FadeVolume(Volume, fadeTime);
        }

        public void DecreaseLayerLevel(float fadeTime)
        {
            int newLayerIndex = ActiveLayerIndex - 1;
            newLayerIndex = Mathf.Clamp(newLayerIndex, 0, MaxLayers - 1);

            // if the layer changed, don't do anything different
            if (newLayerIndex == ActiveLayerIndex) return;
                
            ActiveLayerIndex = newLayerIndex;
            ActivePlayer.FadeVolume(Volume, fadeTime);
        }

        public static void Play(MusicData data, float fadeTime)
        {
            Instance.PlayTrack(data, fadeTime);
        }
        
        void PlayTrack(MusicData data, float fadeTime)
        {
            // if it's the same song, no need to restart
            if (_activeSong == data) return;
            
            // if there's already a song, stop it
            if (_activeSong != null)
            {
                ActivePlayer.Stop(fadeTime);
            }

            // otherwise, play a new song
            _activeSong = data;
            _music1SourcePlaying = !_music1SourcePlaying;

            ActivePlayer.Play(data, fadeTime);
        }

        public void StopMusic(float fadeTime)
        {
            // if there's no song, there's nothing to stop
            if (_activeSong == null) return;

            _activeSong = null;
            ActivePlayer.Stop(fadeTime);
        }
        #endregion
    }
}