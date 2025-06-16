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
    private bool isUIMuted;
    private bool isMusicMuted;
    private bool isGameMuted;
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
    private void Start()
    {
        PlayMusicSound("music01");
    }
    private void ToggleMute(SoundCategory category)
    {
        switch (category)
        {
            case SoundCategory.UI:
                isUIMuted = !isUIMuted;
                UIAudioSource.mute = isUIMuted;
                break;
            case SoundCategory.Game:
                isGameMuted = !isGameMuted;
                gameAudioSource.mute = isGameMuted;
                break;
            case SoundCategory.Music:
                isMusicMuted = !isMusicMuted;
                gameAudioSource.mute = isMusicMuted;
                break;
        }
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
        UIAudioSource.Stop();
        if (isUIMuted) return;
        SoundEffect sound = soundEffectDictionary[soundId];
        UIAudioSource.loop = sound.loop;
        UIAudioSource.PlayOneShot(sound.clip, sound.volume);
    }
    public void PlayGameSound(string soundId)
    {
        gameAudioSource.Stop();
        if(isGameMuted) return;
        SoundEffect sound = soundEffectDictionary[soundId];
        gameAudioSource.loop = sound.loop;
        gameAudioSource.PlayOneShot(sound.clip, sound.volume);
    }
    public void PlayMusicSound(string soundId)
    {
        if (isMusicMuted) return;
        SoundEffect sound = soundEffectDictionary[soundId];
        musicAudioSource.loop = sound.loop;
        musicAudioSource.clip = sound.clip;
        musicAudioSource.volume = sound.volume;
        musicAudioSource.Play();
    }
}
