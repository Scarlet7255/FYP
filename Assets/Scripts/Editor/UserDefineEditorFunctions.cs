using UnityEditor;
using UnityEngine;

public class UserDefineEditorFunctions
{
    [MenuItem("Edit/User Defines/Cleanup Missing Scripts")]
    static void CleanupMissingScripts()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(Selection.gameObjects[i]);
        }
    }
}
