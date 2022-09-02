using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedSpawnZone : BaseSpawnZone
{
    protected override SpawnLocation GetPosition(Character character, List<SpawnLocation> spawnLocations)
    {
        Debug.Log("Changed position");
        var position = spawnLocations.First(x => x.occupied == 0);
        position.index = spawnLocations.IndexOf(position);
        position.occupied++;
        return position;
    }
}