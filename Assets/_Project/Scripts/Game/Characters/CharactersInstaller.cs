using UnityEngine;
using Zenject;

public class CharactersInstaller : MonoInstaller
{
    [SerializeField] private CharacterFactory characterFactoryGameObject;
    [SerializeField] private CharacterFacade emptyCharacterGameObject;

    public override void InstallBindings()
    {
        Container.Bind<CharacterFactory>().FromComponentInNewPrefab(characterFactoryGameObject)
            .UnderTransformGroup("Factories").AsSingle().NonLazy();
        Container.BindFactory<Character, CharacterFacade, CharacterFacade.Factory>()
            .FromComponentInNewPrefab(emptyCharacterGameObject);
    }
}