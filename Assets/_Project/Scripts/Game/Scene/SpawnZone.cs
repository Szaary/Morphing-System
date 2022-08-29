using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnZone : BaseSpawnZone
{
    [SerializeField] private Mesh basicPlayerModel;
    public List<SpawnLocation> spawnLocations = new List<SpawnLocation>();


    public void PlaceCharacter(CharacterFacade facade)
    {
        var position = spawnLocations.First(x => x.occupied == 0);
        facade.transform.position = position.transform.position;
        facade.SetZoneIndex(spawnLocations.IndexOf(position));
        position.occupied++;
    }

    void OnDrawGizmos()
    {
        foreach (var location in spawnLocations)
        {
            if (location.transform == null) return;
            var loc = location.transform.position;
            var drawLocation = new Vector3(loc.x, loc.y + 1, loc.z);
            Gizmos.color = location.occupied == 0 ? Color.cyan : Color.red;
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