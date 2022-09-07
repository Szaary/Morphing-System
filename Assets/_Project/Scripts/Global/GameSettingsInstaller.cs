using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameSettings settings;
    
    [Serializable]
    public class GameSettings
    {
        public SceneLoader.Settings scene;
    }


    public override void InstallBindings()
    {
        
        Container.BindInstance(settings.scene).IfNotBound();
    }
}