using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] private CharacterFactory characterFactoryGameObject;
    [SerializeField] private CharacterFacade emptyCharacterGameObject;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AsyncStateMachine>().AsSingle();

        Container.BindInterfacesAndSelfTo<BattleStart>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerTurn>().AsSingle();
        Container.BindInterfacesAndSelfTo<AiTurn>().AsSingle();
        Container.BindInterfacesAndSelfTo<Victory>().AsSingle();
        Container.BindInterfacesAndSelfTo<Defeat>().AsSingle();
        Container.BindInterfacesAndSelfTo<TurnReferences>().AsSingle();


        Container.Bind<CharacterFactory>().FromComponentInNewPrefab(characterFactoryGameObject)
            .UnderTransformGroup("Factories").AsSingle().NonLazy();
        Container.BindFactory<Character, CharacterFacade, CharacterFacade.Factory>()
            .FromComponentInNewPrefab(emptyCharacterGameObject);
        ;
    }
}