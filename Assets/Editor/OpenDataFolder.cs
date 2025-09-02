using UnityEngine;
using UnityEditor;

public class OpenDataFolder
{
    [MenuItem("Tools/Open Persistent Data Path")]
    public static void OpenPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}