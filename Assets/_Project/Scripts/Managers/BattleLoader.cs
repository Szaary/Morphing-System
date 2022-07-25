public class BattleLoader 
{
    private SceneLoader _sceneLoader;
    
    public BattleLoader(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        
        _sceneLoader.LoadBattleScenes();
    }
}
