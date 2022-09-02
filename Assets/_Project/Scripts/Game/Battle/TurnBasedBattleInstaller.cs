using Zenject;

public class TurnBasedBattleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();

        Container.BindInterfacesAndSelfTo<BattleStart>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerTurn>().AsSingle();
        Container.BindInterfacesAndSelfTo<AiTurn>().AsSingle();
        Container.BindInterfacesAndSelfTo<Victory>().AsSingle();
        Container.BindInterfacesAndSelfTo<Defeat>().AsSingle();
        Container.BindInterfacesAndSelfTo<Paused>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<TurnReferences>().AsSingle();
       

    }
}