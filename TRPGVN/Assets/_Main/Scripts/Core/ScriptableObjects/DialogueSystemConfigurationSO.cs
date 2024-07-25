using CHARACTERS;
using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [CreateAssetMenu(fileName = "Dialogue System Configuration", menuName = "Dialogue System/Dialogue Configuration Asset")]
    public class DialogueSystemConfigurationSO : ScriptableObject
    {
        public CharacterConfigSO characterConfigurationAsset;

        public Color defaultTextColor = Color.white;
        public TMP_FontAsset defaultFont;

        public float dialogueFontScale = 1f;
        public float nameFontScale = 1f;


        public float defaultDialogueFontSize = 30;
        public float defaultNameFontSize = 42;

    }
}