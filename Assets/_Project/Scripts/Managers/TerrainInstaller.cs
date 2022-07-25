using Zenject;

public class TerrainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BattleLoader>().AsSingle().NonLazy();
    }
}