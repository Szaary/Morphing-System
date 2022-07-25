using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BattleSettingsInstaller", menuName = "Installers/BattleSettingsInstaller")]
public class BattleSettingsInstaller : ScriptableObjectInstaller<BattleSettingsInstaller>
{
    public PlayerTurn.Settings playerTurnSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(playerTurnSettings).IfNotBound();
    }
}