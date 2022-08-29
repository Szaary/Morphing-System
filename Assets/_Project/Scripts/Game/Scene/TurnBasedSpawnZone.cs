using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TurnBasedSpawnZone : BaseSpawnZone
{
    protected override void SetPosition(CharacterFacade facade, List<SpawnLocation> spawnLocations)
    {
        var position = spawnLocations.First(x => x.occupied == 0);
        facade.transform.position = position.transform.position;
        facade.SetPosition(position);
        position.index = spawnLocations.IndexOf(position);
        position.occupied++;
    }
}