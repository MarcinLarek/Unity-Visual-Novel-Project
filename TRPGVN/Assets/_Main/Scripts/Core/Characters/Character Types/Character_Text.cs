using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class Character_Text : Character
    {
        public Character_Text(string name, CharacterConfigData config) : base(name, config)
        {
            Debug.Log($"Created Text CharacterL '{name}'");
        }
    }
}