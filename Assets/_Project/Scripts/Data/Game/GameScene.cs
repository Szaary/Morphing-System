using UnityEngine;

[CreateAssetMenu(fileName = "SCS_", menuName = "Settings/Scene")]
public class GameScene : ScriptableObject
{
    [Header("Information")]
    public string sceneName;
    public string shortDescription;
}