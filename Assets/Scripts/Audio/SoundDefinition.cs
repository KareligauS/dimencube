using UnityEngine;

public interface ISoundDefinition
{
    AudioClip Clip { get; }
    float Volume { get; }
    float Pitch { get; }
    float Cooldown { get; }
}

[System.Serializable]
public class SoundDefinition : ISoundDefinition
{
    [SerializeField] private AudioClip _clip;

    [SerializeField, Range(0f, 1f)]
    private float _volume = 1f;

    [SerializeField, Range(0.5f, 2f)]
    private float _pitch = 1f;

    [SerializeField, Min(0f)]
    private float _cooldown = 0f;

    public AudioClip Clip => _clip;
    public float Volume => _volume;
    public float Pitch => _pitch;
    public float Cooldown => _cooldown;
}