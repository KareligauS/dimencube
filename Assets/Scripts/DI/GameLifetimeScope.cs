using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioLibrary _audioLibrary;

    [Header("UI")]
    [SerializeField] private VanishingTextUI _vanishingTextUI;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_audioManager).As<IAudioManager>().AsSelf();
        builder.RegisterInstance(_audioLibrary);
        builder.RegisterComponent(_vanishingTextUI).As<IVanishingTextUI>().AsSelf();
    }
}
