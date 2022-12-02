using UnityEngine;
using UnityEngine.Audio;

namespace Gummi.Audio
{
    public enum LayerType
    {
        Additive,
        Single,
    }
    
    [CreateAssetMenu(menuName = "Audio/Music Data", fileName = "MUS_")]
    public class MusicData : ScriptableObject
    {
        #region Inspector Variables
        [Header("General Settings")]
        [SerializeField]
        AudioClip[] _musicLayers = null;
        
        [SerializeField]
        [Tooltip("If true, layers will be added together, otherwise each layer will player independently")]
        LayerType _layerType = LayerType.Additive;
        
        [SerializeField]
        AudioMixerGroup _mixer;
        #endregion
        
        #region Public Accessors
        public AudioClip[] MusicLayers => _musicLayers;
        public LayerType LayerType => _layerType;
        public AudioMixerGroup Mixer => _mixer;
        #endregion

        #region Play
        public void Play(float fadeTime)
        {
            if (_musicLayers == null)
            {
                throw new System.NullReferenceException("MusicEvent.Play(): No musicClip specified");
            }

            MusicManager.Play(this, fadeTime);
        }
        #endregion
    }
}