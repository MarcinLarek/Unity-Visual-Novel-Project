using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CHARACTERS
{
    public abstract class Character 
    {
        public string name = "";
        public string displayname = "";
        public RectTransform root = null;
        public CharacterConfigData config;

        public DialogueSystem dialogueSytem => DialogueSystem.instance;

        public Character(string name, CharacterConfigData config)
        {
            this.name = name;
            displayname = name;
            this.config = config;
        }

        public Coroutine Say(string dialogue) => Say(new List<string> { dialogue });

        public Coroutine Say(List<string> dialogue)
        {
            dialogueSytem.ShowSpeakerName(displayname);
            UpdateTextCustomizationsOnScreen();
            return dialogueSytem.Say(dialogue);
        }

        public void SetNameColor(Color color) => config.nameColor = color;
        public void SetDialogueColor(Color color) => config.dialogueColor = color;
        public void SetNameFont(TMP_FontAsset font) => config.nameFont = font;
        public void SetDialogueFont(TMP_FontAsset font) => config.dialogueFont = font;
        public void UpdateTextCustomizationsOnScreen() => dialogueSytem.ApplySpeakerDataToDialogueContainer(config);
        public void ResetConfigurationData() => config = CharacterManager.instance.GetCharacterConfig(name);

        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet,
            Live2D,
            Model3D
        }
    }
}