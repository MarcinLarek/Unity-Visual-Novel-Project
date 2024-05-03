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
            Character Mercenary1 = CreateCharacter("Mercenary1 as Affir");
            Character Mercenary2 = CreateCharacter("Mercenary2 as Affir");
            Character Mercenary3 = CreateCharacter("Mercenaru3 as Affir");

            Mercenary1.SetPosition(Vector2.zero);
            Mercenary2.SetPosition(new Vector2(0.5f, 0.5f));
            Mercenary3.SetPosition(Vector2.one);

            
            Mercenary2.Show();
            Mercenary3.Show();

            yield return Mercenary1.Show();

            yield return Mercenary1.MoveToPosition(Vector2.one, smooth: true);
            yield return Mercenary1.MoveToPosition(Vector2.zero, smooth: true);            

            Mercenary1.SetDialogueFont(tempFont);
            Mercenary1.SetNameFont(tempFont);
            Mercenary2.SetDialogueColor(Color.cyan);
            Mercenary3.SetNameColor(Color.red);

            yield return Mercenary1.Say("Mercy Merc");
            yield return Mercenary2.Say("No Mercy Merc");
            yield return Mercenary3.Say("Merc Merc");

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}