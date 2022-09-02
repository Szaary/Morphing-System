using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using StarterAssets;
using UnityEditor;
using UnityEngine.AI;
#endif

public class CharacterCreator : MonoBehaviour
{
#if UNITY_EDITOR

    private readonly Vector3 capsule = new Vector3(0, 1, 0);
    [Header("Creator configuration data")] [SerializeField]
    private CharacterConfiguratorData config;


    [Header("Character creator")] public string characterName;
    public GameObject characterFbxModel;

    public void CreateCharacter()
    {
        
        var folderPath = "Assets/_Project/Characters";
        var characterFolder = folderPath + "/" + characterName;
        var statsFolder = characterFolder + "/Stats";
        var dataFolder = characterFolder + "/Data";

        if (CheckInputs()) return;
        if (GenerateFolders(folderPath, characterFolder)) return;

        var prefabVariant = CreatePrefabVariantFromModel(characterFolder);
        CreateScriptableObjects(characterFolder, dataFolder, statsFolder, prefabVariant);
    }

    private void CreateScriptableObjects(string characterFolder, string dataFolder, string statsFolder,
        GameObject prefabVariant)
    {
        var character = ScriptableObject.CreateInstance<Character>();
        AssetDatabase.CreateAsset(character, characterFolder + "/CHA_" + characterName + ".asset");


        var data = ScriptableObject.CreateInstance<CharacterData>();
        AssetDatabase.CreateAsset(data, dataFolder + "/CDA_" + characterName + ".asset");


        var vfx = ScriptableObject.CreateInstance<CharacterVFX>();
        AssetDatabase.CreateAsset(vfx, dataFolder + "/CVFX_" + characterName + ".asset");


        var sfx = ScriptableObject.CreateInstance<CharacterSFX>();
        AssetDatabase.CreateAsset(sfx, dataFolder + "/SFX_" + characterName + ".asset");


        character.prefab = prefabVariant;
        character.data = data;
        character.data.characterName = characterName;
        character.vfx = vfx;
        character.sfx = sfx;

        foreach (var baseStat in config.statistics)
        {
            var stat = ScriptableObject.CreateInstance<Statistic>();
            AssetDatabase.CreateAsset(stat, statsFolder + "/STA_" + baseStat.statName + ".asset");
            stat.baseStatistic = baseStat;
            character.statisticsTemplate.Add(stat);
        }


        UnityEditorUtility(character, data, vfx, sfx);
    }

    private void UnityEditorUtility(Character character, CharacterData data, CharacterVFX vfx, CharacterSFX sfx)
    {
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = character;
        characterName = "";

        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(character);
        EditorUtility.SetDirty(data);
        EditorUtility.SetDirty(vfx);
        EditorUtility.SetDirty(sfx);
    }

    private GameObject CreatePrefabVariantFromModel(string characterFolder)
    {
        // Creating objects in inspector
        var objSource = PrefabUtility.InstantiatePrefab(characterFbxModel) as GameObject;
        if (objSource == null) throw new Exception("Prefabs were not created;");

        var logic = PrefabUtility.InstantiatePrefab(config.logicModule) as GameObject;
        if (logic == null) throw new Exception("Prefabs were not created;");
        logic.transform.parent = objSource.transform;

        var uiModule = PrefabUtility.InstantiatePrefab(config.uiModule) as GameObject;
        if (uiModule == null) throw new Exception("Prefabs were not created;");
        uiModule.transform.parent = objSource.transform;

        objSource.GetComponent<Animator>().runtimeAnimatorController = config.animator;

        var prefabVariant =
            PrefabUtility.SaveAsPrefabAsset(objSource, characterFolder + "/" + characterName + ".prefab");
        DestroyImmediate(objSource);


        var facade = prefabVariant.AddComponent<CharacterFacade>();
        var controller = prefabVariant.AddComponent<CharacterController>();
        controller.center = capsule;
        
        var capsuleCollider = prefabVariant.AddComponent<CapsuleCollider>();
        capsuleCollider.center = capsule;
        capsuleCollider.height = 2;
        
        var fps = prefabVariant.AddComponent<FirstPersonController>();
        var agent = prefabVariant.AddComponent<NavMeshAgent>();
        var relative = prefabVariant.AddComponent<RelativeController>();
        var animatorMovementController = prefabVariant.AddComponent<AnimatorMovementController>();
        prefabVariant.AddComponent<BasicRigidBodyPush>();
        prefabVariant.AddComponent<Damageable>();
        var navMeshMovement= prefabVariant.AddComponent<NavMeshAgentMovement>();
        
        // Saving prefab variant
        var movement = prefabVariant.GetComponentInChildren<MovementManager>();
        facade.movement = movement;


        movement.controller = controller;
        movement.fps = fps;
        movement.relativeController = relative;
        movement.animatorController = animatorMovementController;
        movement.navMeshAgentMovement = navMeshMovement;

        var animatorManager = prefabVariant.GetComponentInChildren<AnimatorManager>();
        var animator = prefabVariant.GetComponent<Animator>();
        animatorManager.animator = animator;
        
        fps.controller = controller;
        return prefabVariant;
    }

    private bool GenerateFolders(string folderPath, string characterFolder)
    {
        if (CreateFolder(folderPath, characterName)) return true;
        if (CreateFolder(characterFolder, "Stats")) return true;
        if (CreateFolder(characterFolder, "Data")) return true;
        return false;
    }

    private bool CheckInputs()
    {
        if (characterName == "")
        {
            Debug.LogError("Character name is empty");
            return true;
        }

        if (characterFbxModel == null)
        {
            Debug.LogError("Need to select character model");
            return true;
        }

        return false;
    }

    private bool CreateFolder(string path, string folderName)
    {
        if (AssetDatabase.IsValidFolder(path + "/" + folderName))
        {
            Debug.LogError("Character with that name already exists or wrong name");
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
[Serializable]
public struct CharacterConfiguratorData
{
    public List<BaseStatistic> statistics;
    public GameObject logicModule;
    public GameObject uiModule;
    public AnimatorController animator;
}