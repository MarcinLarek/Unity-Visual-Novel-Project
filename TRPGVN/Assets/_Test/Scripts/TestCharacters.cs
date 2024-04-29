using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using TMPro;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {

        public TMP_FontAsset tempFont;

        // Start is called before the first frame update
        void Start()
        {
            //Character Mercenary = CharacterManager.instance.CreateCharacter("Mercenary");
            //Character Affir2 = CharacterManager.instance.CreateCharacter("Affir");
            //Character Aiden = CharacterManager.instance.CreateCharacter("Aiden");

            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character Affir = CharacterManager.instance.CreateCharacter("Affir");
            Character Mercenary = CharacterManager.instance.CreateCharacter("Mercenary");
            Character Aiden = CharacterManager.instance.CreateCharacter("Aiden");

            List<string> lines = new List<string>()
            {
                "Line One",
                "Line Two",
                "Line Three",
                "Line",
            };

            yield return Affir.Say(lines);
            Affir.SetNameColor(Color.red);
            Affir.SetDialogueColor(Color.green);
            Affir.SetNameFont(tempFont);
            Affir.SetDialogueFont(tempFont);
            yield return Affir.Say(lines);
            Affir.ResetConfigurationData();
            yield return Affir.Say(lines);

            lines = new List<string>()
            {
                "Merc Merc",
                "Crem{c}Crem",
            };
            yield return Mercenary.Say(lines);

            yield return Aiden.Say(" Floris ty kurwo");

            Debug.Log("Finished");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}