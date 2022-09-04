using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawnZone : MonoBehaviour
{
    [SerializeField] private Mesh basicPlayerModel;
    
    [SerializeField] protected List<SpawnLocation> playerCharactersSpawnLocations = new List<SpawnLocation>();
    [SerializeField] protected List<SpawnLocation> enemySpawnLocations = new List<SpawnLocation>();
    
    public SpawnLocation GetSpawnPosition(Character character)
    {
        if (character.Alignment.ID == 0)
        {
            return GetPosition(character, playerCharactersSpawnLocations);
        }
        else
        {
            return GetPosition(character, enemySpawnLocations);
        }
    }
    
    protected abstract SpawnLocation GetPosition(Character character, List<SpawnLocation> spawnLocations);


    void OnDrawGizmos()
    {
        foreach (var location in playerCharactersSpawnLocations)
        {
            if (location.transform == null) return;
            var loc = location.transform.position;
            var drawLocation = new Vector3(loc.x, loc.y + 1, loc.z);
            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(basicPlayerModel, drawLocation);
        }
        foreach (var location in enemySpawnLocations)
        {
            if (location.transform == null) return;
            var loc = location.transform.position;
            var drawLocation = new Vector3(loc.x, loc.y + 1, loc.z);
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(basicPlayerModel, drawLocation);
        }
    }

    [Serializable]
    public class SpawnLocation
    {
        public Transform transform;
        public int occupied;
        public int index;
    }


}    
