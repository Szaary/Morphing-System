using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedSpawnZone : BaseSpawnZone
{
    protected override void SetPosition(CharacterFacade facade, List<SpawnLocation> spawnLocations)
    {
        Debug.Log("Changed position");
        var position = spawnLocations.First(x => x.occupied == 0);
        facade.SetPosition(position);
        position.index = spawnLocations.IndexOf(position);
        position.occupied++;

        StartCoroutine(SetTransformPosition(facade, position));
    }

    private IEnumerator SetTransformPosition(CharacterFacade facade, SpawnLocation position)
    {
        // Other scripts prevent character to move to starting positions
        yield return null;
        facade.transform.position = position.transform.position;
    }
}