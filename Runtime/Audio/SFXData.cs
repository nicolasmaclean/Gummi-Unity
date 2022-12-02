using Gummi.Data;
using Gummi.Utility;
using UnityEngine;
using UnityEngine.Audio;

namespace Gummi.Audio
{
    [CreateAssetMenu(menuName = "Audio/SFX Data", fileName = "SFX_")]
    public class SFXData : ScriptableObject
    {
        #region Inspector Variables
        [Header("General Settings")]
        [SerializeField]
        AudioClip[] _possibleClips;
        
        [SerializeField]
        AudioMixerGroup _mixer;

        [Space]
        
        [Range(0, 128)][SerializeField] 
        int _priority = 128;

        [SerializeField]
        [MinMaxRange(0, 1)]
        RangedFloat _volume = new RangedFloat(.8f, .8f);

        [SerializeField]
        [MinMaxRange(0, 2)]
        RangedFloat _pitch = new RangedFloat(.95f, 1.05f);

        [SerializeField, Range(-1, 1)]
        float _stereoPan;

        [Header("3D Settings")]
        [SerializeField, Range(0, 1)]
        float _spatialBlend;

        [SerializeField, ShowIf(nameof(_spatialBlend))]
        float _attenuationMin = 1;
        
        [SerializeField, ShowIf(nameof(_spatialBlend))]
        float _attenuationMax = 500;
        #endregion

        #region Public Accessors
        public AudioMixerGroup Mixer => _mixer;
        public int Priority => _priority;
        public float StereoPan => _stereoPan;
        public float SpatialBlend => _spatialBlend;
        public float AttenuationMin => _attenuationMin;
        public float AttenuationMax => _attenuationMax;
        #endregion
        
        #region Randomization
        public AudioClip Clip => _possibleClips.Length switch
        {
            0 => null,
            1 => _possibleClips[0],
            _ => _possibleClips[Random.Range(0, _possibleClips.Length)],
        };

        public float Volume => _volume.Random();
        public float Pitch => _pitch.Random();
        #endregion

        #region Play
        public void Play() => R_Play();
        public AudioSource R_Play(bool oneShot = true)
        {
            GameObject go = Pool.CheckOut<AudioSource>();
            AudioSource src = go.GetComponent<AudioSource>();
            LoadInto(src);
            src.Play();

            // exit, check-in is the caller's responsibility 
            if (!oneShot) return src;

            if (!src.clip)
            {
                Return(src);
                throw new System.NullReferenceException($"{ name } is missing an AudioClip.");
            }

            // assume one-shot
            float duration = src.clip.length;
            Noop run = go.GetComponent<Noop>() ?? go.AddComponent<Noop>();
            run.StartCoroutine(Coroutines.WaitThen(duration, realTime: true, callback: () =>
            {
                Return(src);
            }));

            return src;
        }

        public void Play(TransformData target) => R_Play(target);
        public AudioSource R_Play(TransformData target, bool oneShot=true)
        {
            AudioSource src = R_Play(oneShot);

            Transform transform = src.transform;
            transform.SetPositionAndRotation(target.Position, target.Rotation);

            return src;
        }

        public static void Return(AudioSource source)
        {
            Pool.CheckIn<AudioSource>(source.gameObject);
        }
        #endregion

        #region Utility
        public void LoadInto(AudioSource source)
        {
            source.clip = Clip;
            source.outputAudioMixerGroup = Mixer;
            source.priority = Priority;
            source.volume = Volume;
            source.pitch = Pitch;
            source.panStereo = StereoPan;
            source.spatialBlend = SpatialBlend;
        }
        #endregion
    }
}