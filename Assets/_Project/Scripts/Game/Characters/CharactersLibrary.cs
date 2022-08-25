using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class CharactersLibrary
{
    public event Action<CharacterFacade> ControlledCharacterChanged;
    public int PlayerCharacters { get; private set; }
    public int AiCharacters { get; private set; }
    public bool SpawnedAllCharacters { get; set; }

    private readonly List<CharacterFacade> _spawnedCharacters = new();

    internal void AddCharacter(CharacterFacade facade)
    {
        _spawnedCharacters.Add(facade);
        CountCharacters();
    }

    internal void RemoveCharacter(CharacterFacade facade)
    {
        _spawnedCharacters.Remove(facade);
        CountCharacters();
    }

    public void SetControlledCharacter(CharacterFacade characterFacade)
    {
        foreach (var character in _spawnedCharacters)
        {
            if (character != characterFacade)
            {
                character.isControlled = false;
                continue;
            }
            ControlledCharacterChanged?.Invoke(characterFacade);
        }
    }

    public Result GetControlledCharacter(out CharacterFacade facade)
    {
        if (_spawnedCharacters.Count == 0)
        {
            facade = null;
            return Result.NoTarget;
        }
        facade = _spawnedCharacters.First(x => x.isControlled);
        return facade == null ? Result.NoTarget : Result.Success;
    }

    private void CountCharacters()
    {
        PlayerCharacters = _spawnedCharacters.Where(x => x.Alignment.id == 0).ToList().Count;
        AiCharacters = _spawnedCharacters.Where(x => x.Alignment.id != 0).ToList().Count;
    }

    public CharacterFacade SelectRandomEnemy(Alignment userAlignment)
    {
        var enemies = _spawnedCharacters.Where(x => x.Alignment.id != userAlignment.id).ToList();
        var index = Random.Range(0, enemies.Count);
        return enemies[index];
    }

    public List<CharacterFacade> SelectAllEnemies(Alignment userAlignment)
    {
        var enemies = _spawnedCharacters.Where(x => x.Alignment.id != userAlignment.id).ToList();
        return enemies;
    }

    public List<CharacterFacade> SelectAllAllies(Alignment userAlignment)
    {
        var allies = _spawnedCharacters.Where(x => x.Alignment.id == userAlignment.id).ToList();
        return allies;
    }


    public List<CharacterFacade> SelectAll()
    {
        return _spawnedCharacters;
    }
}