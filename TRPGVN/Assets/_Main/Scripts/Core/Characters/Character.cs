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

        public DialogueSystem dialogueSytem => DialogueSystem.instance;

        public Character(string name)
        {
            this.name = name;
            this.displayname = name;
        }

        public Coroutine Say(string dialogue) => Say(new List<string> { dialogue });

        public Coroutine Say(List<string> dialogue)
        {
            dialogueSytem.ShowSpeakerName(displayname);
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