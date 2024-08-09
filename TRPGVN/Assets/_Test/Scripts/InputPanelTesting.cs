using CHARACTERS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanelTesting : MonoBehaviour
{
    public InputPanel inputPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        Character Affir = CharacterManager.instance.CreateCharacter("Affir", revealAfterCreation: true);
        
        yield return Affir.Say("Give me your name");

        inputPanel.Show("What is your name?");

        while (inputPanel.isWaitingOnUserInput)
            yield return null;

        string characterName = inputPanel.lastInput;

        yield return Affir.Say($"Welcome to the band {characterName}");

    }

}
