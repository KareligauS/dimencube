using UnityEngine;
using VContainer;

public interface IAudioManager
{
    void PlaySFX(AudioClipEnum audioClipEnum);
    void PlaySFX(SoundDefinition sound);
    void StopSFX();
    void PlayBackgroundMusic(AudioClipEnum audioClipEnum);
    void PlayBackgroundMusic(SoundDefinition sound);
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    [Inject] private readonly AudioLibrary _audioLibrary;

    [SerializeField, Range(0f, 1f)] private float _backgroundMusicVolume = 0.3f;

    private AudioSource _musicAudioSource;
    private AudioSource _sfxAudioSource;
    private IAudioSoundResolver _audioSoundResolver;

    void Awake()
    {
        _musicAudioSource = CreateAudioSource("MusicAudioSource");
        _sfxAudioSource = CreateAudioSource("SFXAudioSource");
        _audioSoundResolver = new AudioSoundResolver(_audioLibrary);
    }

    private AudioSource CreateAudioSource(string name)
    {
        var audioSourceObject = new GameObject(name);
        audioSourceObject.transform.SetParent(transform);
        return audioSourceObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        PlayBackgroundMusic(AudioClipEnum.BackgroundMusic);
    }

    public void PlaySFX(AudioClipEnum audioClipEnum)
    {
        var sound = _audioSoundResolver.Resolve(audioClipEnum);
        PlaySFX(sound);
    }

    public void StopSFX()
    {
        _sfxAudioSource.Stop();
    }

    public void PlaySFX(SoundDefinition sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("SFX clip not found!");
            return;
        }

        _sfxAudioSource.PlayOneShot(sound.Clip, sound.Volume);
    }

    public void PlayBackgroundMusic(AudioClipEnum audioClipEnum)
    {
        var sound = _audioSoundResolver.Resolve(audioClipEnum);
        PlayBackgroundMusic(sound);
    }

    public void PlayBackgroundMusic(SoundDefinition sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("Background music clip not found!");
        }

        _musicAudioSource.clip = sound.Clip;
        _musicAudioSource.volume = _backgroundMusicVolume;
        _musicAudioSource.pitch = sound.Pitch;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }
}
