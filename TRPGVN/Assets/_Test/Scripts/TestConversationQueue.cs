using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConversationQueue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        List<string> lines = new List<string>()
        {
            "Line 1",
            "Line 2",
            "Line 3",
        };

        yield return DialogueSystem.instance.Say(lines);

        DialogueSystem.instance.Hide();
    }
    void Update()
    {
        List<string> lines = new List<string>();
        Conversation conversation = null;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            lines = new List<string>()
            {
                "Start of enqueued convesrsation",
                "We can keep it going"
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.Enqueue(conversation);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            lines = new List<string>()
            {
                "this is an important conversation",
                "feajfjaifjaias"
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.EnqueuePriority(conversation);
        }
    }
}
