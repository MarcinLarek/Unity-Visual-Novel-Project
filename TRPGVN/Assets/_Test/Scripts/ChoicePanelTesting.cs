using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChoicePanelTesting : MonoBehaviour
{
    ChoicePanel panel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

   IEnumerator Running()
    {
        panel = ChoicePanel.instance;

        string[] choices = new string[]
        {
            "First choice",
            "Second",
            "Third choice!",
            "Final but a little bit longer choice to test the width. Banana."
        };

        panel.Show("Make your choice", choices);

        while (panel.isWaitingOnUserChoice)
            yield return null;

        var decision = panel.lastDecision;

        Debug.Log($"Made choice {decision.answerIndex} '{decision.choices[decision.answerIndex]}'");
    }
}
