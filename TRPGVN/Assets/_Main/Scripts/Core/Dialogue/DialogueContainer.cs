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
            SetDialogueFontSize(config.dialogueFontSize * DialogueSystem.instance.config.dialogueFontScale);
            nameContainer.SetNameColor(config.nameColor);
            nameContainer.SetNameFont(config.nameFont);
            nameContainer.SetNameFontSize(config.nameFontSize * DialogueSystem.instance.config.nameFontScale);
        }
        public void SetDialogueColor(Color color) => dialogueText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;
        public void SetDialogueFontSize(float size) => dialogueText.fontSize = size;
    }
}
