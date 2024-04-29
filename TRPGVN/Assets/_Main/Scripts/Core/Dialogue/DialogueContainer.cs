using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        public GameObject root;
        public NameContainer nameContainer;
        public TextMeshProUGUI dialogueText;

        public void SetConfig(CHARACTERS.CharacterConfigData config)
        {
            SetDialogueColor(config.dialogueColor);
            SetDialogueFont(config.dialogueFont);
            nameContainer.SetNameColor(config.nameColor);
            nameContainer.SetNameFont(config.nameFont);
        }
        public void SetDialogueColor(Color color) => dialogueText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;
    }
}
