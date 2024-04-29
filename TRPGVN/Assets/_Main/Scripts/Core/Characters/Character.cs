using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
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
            dialogueSytem.ApplySpeakerDataToDialogueContainer(name);
            return dialogueSytem.Say(dialogue);
        }

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