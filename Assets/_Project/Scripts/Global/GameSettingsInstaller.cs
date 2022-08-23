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
        public GameManager.Settings game;
        public SoundManager.Settings sound;
        public SceneLoader.Settings scene;
    }


    public override void InstallBindings()
    {
        Container.BindInstance(settings.game).IfNotBound();
        Container.BindInstance(settings.sound).IfNotBound();
        Container.BindInstance(settings.scene).IfNotBound();
    }
}