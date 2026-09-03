using UnityEngine;

public interface IAudioSoundResolver
{
    SoundDefinition Resolve(AudioClipEnum audioClipEnum);
}

public class AudioSoundResolver : IAudioSoundResolver
{
    private readonly AudioLibrary _audioLibrary;

    public AudioSoundResolver(AudioLibrary audioLibrary)
    {
        _audioLibrary = audioLibrary;
    }

    public SoundDefinition Resolve(AudioClipEnum audioClipEnum)
    {
        var sound = audioClipEnum switch
        {
            AudioClipEnum.BackgroundMusic => _audioLibrary.backgroundMusic,
            AudioClipEnum.Jump => _audioLibrary.jumpSFX,
            AudioClipEnum.Death => _audioLibrary.deathSFX,
            AudioClipEnum.Chandelier => _audioLibrary.chandelierSFX,
            AudioClipEnum.Splash => _audioLibrary.splashSFX,
            AudioClipEnum.Walking => _audioLibrary.walkingSFX,
            AudioClipEnum.Switch => _audioLibrary.switchSFX,
            AudioClipEnum.Door => _audioLibrary.doorSFX,
            AudioClipEnum.Fall => _audioLibrary.fallSFX,
            AudioClipEnum.Sliding => _audioLibrary.slidingSFX,
            AudioClipEnum.Key => _audioLibrary.keySFX,
            _ => null,
        };

        if (sound == null || sound.Clip == null)
        {
            Debug.Log("SoundDefinition not found or AudioClip not exists for enum: " + audioClipEnum);
            return null;
        }

        return sound;
    }
}
