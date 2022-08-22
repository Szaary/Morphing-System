using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterCreator : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Creator Config")] public List<BaseStatistic> statistics = new List<BaseStatistic>();
   
    [Header("Creator")]
    public string characterName;

    public void CreateCharacter()
    {
        if (characterName == "")
        {
            Debug.LogError("Character name is empty");
            return;
        }

        if (CreateFolder("Assets/_Project/Data/Characters", characterName))
        {
            characterName = "";
            return;
        }


        var character = ScriptableObject.CreateInstance<Character>();
        AssetDatabase.CreateAsset(character,
            "Assets/_Project/Data/Characters/" + characterName + "/CHA_" + characterName + ".asset");
        
        var data = ScriptableObject.CreateInstance<CharacterData>();
        AssetDatabase.CreateAsset(data,
            "Assets/_Project/Data/Characters/" + characterName + "/CDA_" + characterName + ".asset");
        character.data = data;
        character.data.characterName = characterName;
        
        var characterStatistics = ScriptableObject.CreateInstance<CharacterStatistics>();
        AssetDatabase.CreateAsset(characterStatistics,
            "Assets/_Project/Data/Characters/" + characterName + "/CS_" + characterName + ".asset");
        character.SetBaseStats(characterStatistics);

        var vfx = ScriptableObject.CreateInstance<CharacterVFX>();
        AssetDatabase.CreateAsset(vfx,
            "Assets/_Project/Data/Characters/" + characterName + "/CVFX_" + characterName + ".asset");
        character.vfx = vfx;
        
        var sfx = ScriptableObject.CreateInstance<CharacterSFX>();
        AssetDatabase.CreateAsset(sfx,
            "Assets/_Project/Data/Characters/" + characterName + "/SFX_" + characterName + ".asset");
        character.sfx = sfx;
        

        if (CreateFolder("Assets/_Project/Data/Characters/" + characterName, "Stats")) return;

        foreach (var baseStat in this.statistics)
        {
            var stat = ScriptableObject.CreateInstance<Statistic>();
            AssetDatabase.CreateAsset(stat,
                "Assets/_Project/Data/Characters/" + characterName + "/Stats/STA_" + baseStat.statName + ".asset");
            stat.baseStatistic = baseStat;
            characterStatistics.statistics.Add(stat);
        }


        AssetDatabase.SaveAssets();

        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = character;
        characterName = "";


        EditorUtility.SetDirty(character);
        EditorUtility.SetDirty(data);
        EditorUtility.SetDirty(characterStatistics);
        EditorUtility.SetDirty(vfx);
        EditorUtility.SetDirty(sfx);
    }

    private bool CreateFolder(string path, string folderName)
    {
        if (AssetDatabase.IsValidFolder(path + "/" + folderName))
        {
            Debug.LogError("Character with that name already exists");
            return true;
        }

        AssetDatabase.CreateFolder(path, folderName);
        return false;
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(CharacterCreator))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CharacterCreator myTarget = (CharacterCreator) target;

        DrawDefaultInspector();

        if (GUILayout.Button("Create character"))
        {
            myTarget.CreateCharacter();
        }
    }
}
#endif