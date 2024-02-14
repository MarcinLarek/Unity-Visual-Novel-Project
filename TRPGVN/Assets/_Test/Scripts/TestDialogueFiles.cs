using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class TestDialogueFiles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartConversation();

    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset("testFile", false);

        DialogueSystem.instance.Say(lines);
    }
}
