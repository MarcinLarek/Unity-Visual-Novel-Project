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
            Character Affir = CharacterManager.instance.CreateCharacter("Affir");
            Character Mercenary = CharacterManager.instance.CreateCharacter("Mercenary");
            Character Affir2 = CharacterManager.instance.CreateCharacter("Affir");
            Character Aiden = CharacterManager.instance.CreateCharacter("Aiden");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}