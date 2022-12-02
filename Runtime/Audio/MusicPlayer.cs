using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        #region Members
        AudioSource[] _layerSources = Array.Empty<AudioSource>();
        float[] _sourceStartVolumes = Array.Empty<float>();

        Coroutine _fadeVolumeRoutine;
        Coroutine _stopRoutine;

        public bool IsStopping { get; private set; }

        static MusicManager _manager => MusicManager.Instance;
        MusicData _data;
        #endregion

        #region Setup
        void Awake()
        {
            CreateLayerSources();
        }

        void CreateLayerSources()
        {
            _layerSources = new AudioSource[MusicManager.MaxLayers];
            _sourceStartVolumes = new float[MusicManager.MaxLayers];
            for (int i = 0; i < MusicManager.MaxLayers; i++)
            {
                AudioSource src = gameObject.AddComponent<AudioSource>();
                _layerSources[i] = src;
                src.playOnAwake = false;
                src.loop = true;
            }
        }
        #endregion

        #region Play
        public void Play(MusicData data, float fadeTime)
        {
            if (data == null) throw new NullReferenceException("No music tracks were provided");

            _data = data;
            for (int i = 0; i < _layerSources.Length && i < data.MusicLayers.Length; i++)
            {
                // skip, data is invalid
                if (data.MusicLayers[i] == null) continue;

                // load track
                AudioSource src = _layerSources[i];
                src.volume = 0;
                src.clip = data.MusicLayers[i];
                src.outputAudioMixerGroup = data.Mixer;

                src.Play();
            }
            
            FadeVolume(_manager.Volume, fadeTime);
        }

        public void Stop(float fadeTime)
        {
            if (_stopRoutine != null)
            {
                StopCoroutine(_stopRoutine);
            }
            
            _stopRoutine = StartCoroutine(StopRoutine(fadeTime));
        }

        public void FadeVolume(float volume, float fadeTime)
        {
            if (_data == null) return;

            volume = Mathf.Clamp(volume, 0, 1);
            if (fadeTime < 0) fadeTime = 0;

            if (_fadeVolumeRoutine != null)
            {
                StopCoroutine(_fadeVolumeRoutine);
            }

            // if single layer, lerp active layer
            if (_data.LayerType == LayerType.Single)
            {
                _fadeVolumeRoutine = StartCoroutine(LerpSourcesSingleRoutine(volume, fadeTime));
                return;
            }
            
            // if additive, lerp additively
            if (_data.LayerType == LayerType.Additive)
            {
                _fadeVolumeRoutine = StartCoroutine(LerpSourceAdditiveRoutine(volume, fadeTime));
            }
        }
        #endregion

        #region Utility
        IEnumerator StopRoutine(float fadeTime)
        {
            IsStopping = true;
            
            // cancel current running volume fades
            if (_fadeVolumeRoutine != null)
            {
                StopCoroutine(_fadeVolumeRoutine);
            }

            // start the fadeout
            // when done fading out, disable all sources
            // if additive, lerp additively
            // otherwise lerp up only the active layer
            _fadeVolumeRoutine = _data.LayerType switch
            {
                LayerType.Single => StartCoroutine(LerpSourcesSingleRoutine(0, fadeTime)),
                LayerType.Additive => StartCoroutine(LerpSourceAdditiveRoutine(0, fadeTime)),
                _ => throw new ArgumentOutOfRangeException(),
            };

            // wait for volume fade to finish
            yield return _fadeVolumeRoutine;

            foreach (AudioSource source in _layerSources)
            {
                source.Stop();
            }

            IsStopping = false;
        }

        void SaveSourceStartVolumes()
        {
            // store source start volumes independently for individual ASource lerping
            for (int i = 0; i < _layerSources.Length; i++)
            {
                _sourceStartVolumes[i] = (_layerSources[i].volume);
            }
        }

        // go through sources and fade in all layers up to active layer, fade down the rest
        IEnumerator LerpSourceAdditiveRoutine(float targetVolume, float fadeTime)
        {
            SaveSourceStartVolumes();

            for (float elapsedTime = 0; elapsedTime <= fadeTime; elapsedTime += Time.deltaTime)
            {
                // go through all layers
                for (int i = 0; i < _layerSources.Length; i++)
                {
                    float startVolume = _sourceStartVolumes[i];;
                    
                    // fade layers up until active layer
                    // otherwise fade it to 0 from its current position
                    float newVolume = (i <= _manager.ActiveLayerIndex)
                        ? Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime)
                        : Mathf.Lerp(startVolume, 0, elapsedTime / fadeTime);
                    
                    _layerSources[i].volume = newVolume;
                }

                yield return null;
            }
            
            // set final target just to make sure we hit the exact value
            for (int i = 0; i <= _manager.ActiveLayerIndex; i++)
            {
                _layerSources[i].volume = (i <= _manager.ActiveLayerIndex)
                    ? targetVolume
                    : 0;
            }
        }

        // go through all the sources and fade on the active layer, fade down the rest
        IEnumerator LerpSourcesSingleRoutine(float targetVolume, float fadeTime)
        {
            SaveSourceStartVolumes();

            for (float elapsedTime = 0; elapsedTime <= fadeTime; elapsedTime += Time.deltaTime)
            {
                for (int i = 0; i < _layerSources.Length; i++)
                {
                    float startVolume = _sourceStartVolumes[i];
                    
                    // fade up
                    // else fade down
                    float newVolume = (i == _manager.ActiveLayerIndex)
                        ? Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime)
                        : Mathf.Lerp(startVolume, 0, elapsedTime / fadeTime);
                    
                    _layerSources[i].volume = newVolume;
                }

                yield return null;
            }

            // set final target just to make sure we hit the exact value
            for (int i = 0; i <= _manager.ActiveLayerIndex; i++)
            {
                _layerSources[i].volume = (i <= _manager.ActiveLayerIndex)
                    ? targetVolume
                    : 0;
            }
        }
        #endregion
    }
}