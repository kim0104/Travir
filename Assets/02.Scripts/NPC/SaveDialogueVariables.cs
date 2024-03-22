using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.IO;

public class SaveDialogueVariables : MonoBehaviour
{
    public void SaveVariables()
    {
        string result = DialogueLua.GetVariable("result").AsString;
        string title = DialogueLua.GetVariable("title").AsString;

        var dialogueData = new
        {
            result = result,
            title = title
        };
        string json = JsonUtility.ToJson(dialogueData);

        File.WriteAllText(Application.persistentDataPath + "/dialogueData.json", json);
    }
}
