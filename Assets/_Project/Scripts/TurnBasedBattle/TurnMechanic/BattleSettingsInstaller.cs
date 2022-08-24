using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BattleSettingsInstaller", menuName = "Installers/BattleSettingsInstaller")]
public class BattleSettingsInstaller : ScriptableObjectInstaller<BattleSettingsInstaller>
{
    public TurnController.Settings turnController;
    
    
    public override void InstallBindings()
    {
        Container.BindInstance(turnController).IfNotBound();
        
    }
}