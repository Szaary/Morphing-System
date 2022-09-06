using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ToUiEventsHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<CharactersLibrary>().AsSingle();
    }
}