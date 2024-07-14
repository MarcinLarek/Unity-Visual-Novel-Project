using CHARACTERS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class AudioTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Running());
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name, true);

        IEnumerator Running()
        {
            Character_Sprite Affir = CreateCharacter("Affir") as Character_Sprite;
            Character me = CreateCharacter("ME");

            yield return new WaitForSeconds(0.5f);

            AudioManager.instance.PlaySoundEffect("Audio/SFX/RadioStatic", loop: true);

            yield return me.Say("Turn off the radio.");

            AudioManager.instance.StopSoundEffect("RadioStatic");
            AudioManager.instance.PlayVoice("Audio/Voices/exclamation");


            yield return Affir.Say("ok");



        }
    }
}