using UnityEngine;
using Zenject;

public class UiPoolInstaller : MonoInstaller<UiPoolInstaller>
{
    public GameObject combatTextPrefab;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CombatTextFactory>().AsSingle().NonLazy();

        Container.BindMemoryPool<CombatText, CombatText.Pool>()
            .WithInitialSize(15)
            .FromComponentInNewPrefab(combatTextPrefab)
            .UnderTransformGroup("CombatTexts");
    }
}