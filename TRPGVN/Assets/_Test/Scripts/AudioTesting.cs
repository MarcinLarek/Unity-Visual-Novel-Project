using CHARACTERS;
using DIALOGUE;
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

        IEnumerator Running2()
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

        IEnumerator Running()
        {
            yield return new WaitForSeconds(1);

            Character_Sprite Affir = CreateCharacter("Affir") as Character_Sprite;

            yield return DialogueSystem.instance.Say("Narrator", "Can we see your ship?");

            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphic/BG_Images/5");
            AudioManager.instance.PlayTrack("Audio/Music/Calm", startingVolume: 0.7f, ambience: true);
            AudioManager.instance.PlayVoice("Audio/Voices/wakeup");

            Affir.SetSprite(Affir.GetSprite("Affir_Happy"), 1);
            Affir.MoveToPosition(new Vector2(0.7f, 0), speed: 0.5f);
            yield return Affir.Say("Yes of course!");

            yield return null;
        }
    }
}