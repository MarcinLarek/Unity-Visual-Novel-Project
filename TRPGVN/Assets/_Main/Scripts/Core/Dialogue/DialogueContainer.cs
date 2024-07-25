using UnityEngine;
using TMPro;
using System.Collections;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        public GameObject root;
        public NameContainer nameContainer;
        public TextMeshProUGUI dialogueText;

        private CanvasGroupController cgController;

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

        private bool initialized = false;
        public void Initialize()
        {
            if (initialized)
                return;

            cgController = new CanvasGroupController(DialogueSystem.instance, root.GetComponent<CanvasGroup>());
        }

        public bool isVisible => cgController.isVisible;
        public Coroutine Show(float speed = 1f, bool immediate = false) => cgController.Show(speed, immediate);
        public Coroutine Hide(float speed = 1f, bool immediate = false) => cgController.Hide(speed, immediate);

    }
}
