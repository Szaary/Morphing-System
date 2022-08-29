using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawnZone : MonoBehaviour
{
    [SerializeField] private Mesh basicPlayerModel;
    
    [SerializeField] protected List<SpawnLocation> playerCharactersSpawnLocations = new List<SpawnLocation>();
    [SerializeField] protected List<SpawnLocation> enemySpawnLocations = new List<SpawnLocation>();
    
    public void PlaceCharacter(CharacterFacade facade)
    {
        if (facade.Alignment.id == 0)
        {
            SetPosition(facade, playerCharactersSpawnLocations);
        }
        else
        {
            SetPosition(facade, enemySpawnLocations);
        }
    }

    protected abstract void SetPosition(CharacterFacade facade, List<SpawnLocation> spawnLocations);


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
    }

}    
