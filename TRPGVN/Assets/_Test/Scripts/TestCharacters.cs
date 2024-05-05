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

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start()
        {
            //Character Affir = CharacterManager.instance.CreateCharacter("Affir");
            //Character Mercenary = CharacterManager.instance.CreateCharacter("Mercenary");
            //Character Affir2 = CharacterManager.instance.CreateCharacter("Affir");
            //Character Aiden = CharacterManager.instance.CreateCharacter("Aiden");

            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character_Sprite Affir = CharacterManager.instance.CreateCharacter("Affir") as Character_Sprite;
            //Character Mercenary2 = CreateCharacter("Mercenary2 as Affir");
            //Character Mercenary3 = CreateCharacter("Mercenary3 as Affir");

            yield return new WaitForSeconds(1);

            yield return Affir.TransitionColor(Color.red, speed: 0.3f);
            yield return Affir.TransitionColor(Color.blue);
            yield return Affir.TransitionColor(Color.yellow);
            yield return Affir.TransitionColor(Color.white);


            Sprite s1 = Affir.GetSprite("Affir_Angry");
            Affir.TransititionSprite(s1);

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}