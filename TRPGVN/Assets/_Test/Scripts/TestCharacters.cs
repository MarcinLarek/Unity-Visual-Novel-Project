using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
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
                "Line {wa 1}",
            };
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