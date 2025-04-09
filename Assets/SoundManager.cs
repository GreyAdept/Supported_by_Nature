using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [System.Serializable]
    public class SoundEffect
    {
        public string id;
        public AudioClip clip;
        public float volume = 1f;
        public bool loop = false;
        public SoundCategory category;
    }
    public enum SoundCategory
    {
        UI,
        Game,
        Music
    }
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource UIAudioSource;
    [SerializeField] private AudioSource gameAudioSource;
    [SerializeField] private List<SoundEffect> soundEffects;
    private Dictionary<string, SoundEffect> soundEffectDictionary;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else Destroy(gameObject);
    }
    private void Initialize()
    {
        soundEffectDictionary = new Dictionary<string, SoundEffect>();
        foreach(SoundEffect sound in soundEffects)
        {
            soundEffectDictionary[sound.id] = sound;
        }
    }
    public void PlayUISound(string soundId)
    {
        SoundEffect sound = soundEffectDictionary[soundId];
        UIAudioSource.loop = sound.loop;
        UIAudioSource.PlayOneShot(sound.clip, sound.volume);
    }
}
