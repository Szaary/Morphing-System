using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public string characterName;
    
    public void CreateCharacter()
    {
       // var asset = ScriptableObject.CreateInstance<Character>();

        AssetDatabase.CreateFolder("Assets/_Project/Data/Characters/", characterName);
        
        
        //AssetDatabase.CreateAsset(asset, "Assets/_Project/Data/Characters/"+ characterName +".asset");
        //AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

       // Selection.activeObject = asset;
    }
}

[CustomEditor(typeof(CharacterCreator))]
public class LevelScriptEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        CharacterCreator myTarget = (CharacterCreator)target;

        myTarget.characterName = EditorGUILayout.TextField("Character Name", myTarget.characterName);
        if(GUILayout.Button("Create character"))
        {
            myTarget.CreateCharacter();
        }
    }
}

