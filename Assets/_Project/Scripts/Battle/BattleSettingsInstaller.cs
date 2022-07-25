using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BattleSettingsInstaller", menuName = "Installers/BattleSettingsInstaller")]
public class BattleSettingsInstaller : ScriptableObjectInstaller<BattleSettingsInstaller>
{
    public PlayerTurn.Settings playerTurnSettings;
    public AiController.Settings aISettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(playerTurnSettings).IfNotBound();
        Container.BindInstance(aISettings).IfNotBound();
    }
}