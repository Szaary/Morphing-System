using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharactersLibrary
{
    public event Action noAllies;
    public event Action noEnemies;
    public event Action<CharacterFacade> ControlledCharacterChanged;
    public int PlayerCharacters { get; private set; }
    public int AiCharacters { get; private set; }
    public bool SpawnedAllCharacters { get; set; }

    private readonly List<CharacterFacade> _spawnedCharacters = new();

    internal void AddCharacter(CharacterFacade facade)
    {
        _spawnedCharacters.Add(facade);
        CountCharacters();
        GainControl(facade);
    }

    private void GainControl(CharacterFacade newCharacter)
    {
        if (newCharacter.Alignment.id != 0) return;

        if (GetControlledCharacter(out var facade) != Result.Success)
        {
            newCharacter.GainControl();
        }
    }

    internal void RemoveCharacter(CharacterFacade facade)
    {
        _spawnedCharacters.Remove(facade);
        CountCharacters();
        if (AiCharacters == 0)
        {
            SpawnedAllCharacters = false;
        }

        GainControl(facade);

        if (PlayerCharacters == 0)
        {
            noAllies?.Invoke();
        }

        if (AiCharacters == 0)
        {
            noEnemies?.Invoke();
        }
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

        facade = _spawnedCharacters.FirstOrDefault(x => x.isControlled);
        return facade == null ? Result.NoTarget : Result.Success;
    }

    private void CountCharacters()
    {
        PlayerCharacters = _spawnedCharacters.Where(x => x.Alignment.id == 0).ToList().Count;
        AiCharacters = _spawnedCharacters.Where(x => x.Alignment.id != 0).ToList().Count;
    }


    public CharacterFacade SelectRandomEnemy(int userAlignmentId)
    {
        var enemies = _spawnedCharacters.Where(x => x.Alignment.id != userAlignmentId).ToList();
        if (enemies.Count == 0) return null;
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
        return _spawnedCharacters.ToList();
    }
}