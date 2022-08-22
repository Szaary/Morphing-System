using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public List<SpawnLocation> spawnLocations= new List<SpawnLocation>();


    
    void OnDrawGizmos()
    {
        foreach (var location in spawnLocations)
        {
            if (location.transform == null) return;
            Gizmos.color = location.occupied == 0 ? Color.cyan : Color.red;
            Gizmos.DrawWireSphere(location.transform.position, 0.3f);
        }
    }

    [Serializable]
    public struct SpawnLocation
    {
        public Transform transform;
        public int occupied;
    }
}