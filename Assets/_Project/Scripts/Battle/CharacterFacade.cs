using UnityEngine;

public class CharacterFacade : MonoBehaviour
{
    [SerializeField] private Character character;

    private void Start()
    {
        character.InitializeStats();
        var stat = character.currentStats.statistics[0];
        var stat2 = character.currentStats.statistics[1];
        var value = stat==stat2;
        Debug.Log(value);
    }
}
