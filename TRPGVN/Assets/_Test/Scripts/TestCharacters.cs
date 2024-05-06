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
            Character Mercenary1 = CreateCharacter("Mercenary1 as Affir");
            Character Mercenary2 = CreateCharacter("Bob as Affir");
            Character Mercenary3 = CreateCharacter("Mercenary3 as Affir");

            Affir.SetPosition(new Vector2(0.3f, 1));
            Mercenary1.SetPosition(new Vector2(0.45f, 1));
            Mercenary2.SetPosition(new Vector2(0.6f, 1));
            Mercenary3.SetPosition(new Vector2(0.75f, 1));

            Mercenary1.SetColor(Color.red);
            Mercenary2.SetColor(Color.green);
            Mercenary3.SetColor(Color.blue);

            Affir.SetPriority(1000);
            Mercenary1.SetPriority(15);
            Mercenary2.SetPriority(8);
            Mercenary3.SetPriority(30);

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "Bob", "Affir" });

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters();

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "Mercenary3", "Bob", "Mercenary1", "Affir" });

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}