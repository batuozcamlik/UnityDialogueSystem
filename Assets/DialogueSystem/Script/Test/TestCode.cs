using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    public List<CharacterInformation> currentDialogue;
    public void AddDialogueTest()
    {
        DialogueManager.Instance.AddDialogueAndStart(currentDialogue);
        Debug.Log("AddDialogueTest() executed!");
    }
}

[CustomEditor(typeof(TestCode))]
public class TestCodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TestCode testCode = (TestCode)target;

        if (GUILayout.Button("Dialogue Test"))
        {
            testCode.AddDialogueTest();
        }
    }
}
