using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharactersInstaller : MonoInstaller
{
    [SerializeField] private GameObject projectile;
    
    public override void InstallBindings()
    {
        Container.BindFactory<Object, Character, CharacterFacade, CharacterFacade.Factory>()
            .FromFactory<PrefabFactory<Character, CharacterFacade>>();
        Container.Bind<ICharacterFactory>().To<CharacterFactory>().AsSingle();
        
        
        Container.BindInterfacesAndSelfTo<ProjectileMemoryPool>().AsSingle().NonLazy();
        
        Container.BindMemoryPool<Projectile, Projectile.Pool>()
            .WithInitialSize(15)
            .FromComponentInNewPrefab(projectile)
            .UnderTransformGroup("Projectiles");
    }
}