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
            //Character Affir = CharacterManager.instance.CreateCharacter("Affir");
            //Character Mercenary = CharacterManager.instance.CreateCharacter("Mercenary");
            //Character Affir2 = CharacterManager.instance.CreateCharacter("Affir");
            //Character Aiden = CharacterManager.instance.CreateCharacter("Aiden");

            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(1f);
            Character Affir = CharacterManager.instance.CreateCharacter("Affir");
            yield return new WaitForSeconds(1f);
            yield return Affir.Hide();
            yield return new WaitForSeconds(0.5f);
            yield return Affir.Show();
            yield return Affir.Say("TOO MUCH MAGIC");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}