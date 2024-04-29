using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class Character_Live2D : Character
    {
        public Character_Live2D(string name, CharacterConfigData config) : base(name, config)
        {
            Debug.Log($"Created Live2D CharacterL '{name}'");
        }
    }
}