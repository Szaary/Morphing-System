using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] private TurnStateMachine turnStateMachineGameObject;
    [SerializeField] private CharacterFactory characterFactoryGameObject;
 
    
    public override void InstallBindings()
    {
        Container.Bind<TurnStateMachine>().FromComponentInNewPrefab(turnStateMachineGameObject).UnderTransformGroup("Managers").AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<AsyncStateMachine>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<BattleStart>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerTurn>().AsSingle();
        Container.BindInterfacesAndSelfTo<AiTurn>().AsSingle();
        Container.BindInterfacesAndSelfTo<Victory>().AsSingle();
        Container.BindInterfacesAndSelfTo<Defeat>().AsSingle();
        
        
        Container.Bind<CharacterFactory>().FromComponentInNewPrefab(characterFactoryGameObject).UnderTransformGroup("Factories").AsSingle().NonLazy();
       
    }
    
    
}