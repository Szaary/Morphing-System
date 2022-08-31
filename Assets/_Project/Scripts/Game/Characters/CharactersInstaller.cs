using UnityEngine;
using Zenject;

public class CharactersInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.BindFactory<Object, Character, CharacterFacade, CharacterFacade.Factory>().FromFactory<PrefabFactory<Character, CharacterFacade>>();
        Container.Bind<ICharacterFactory>().To<CharacterFactory>().AsSingle();
    }
}