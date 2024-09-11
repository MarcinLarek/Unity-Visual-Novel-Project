using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Mediation;
using UnityEngine;

namespace History
{
    [System.Serializable]
    public class DialogueData
    {
        public string currentDialogue = "";
        public string currentSpeaker = "";

        public string dialogueFont;
        public Color dialogueColor;
        public float dialogueScale;

        public string speakerFont;
        public Color speakerNameColor;
        public float speakerScale;

        public static DialogueData Capture()
        {
            DialogueData data = new DialogueData();

            var ds = DialogueSystem.instance;
            var dialogueText = ds.dialogueContainer.dialogueText;
            var nameText = ds.dialogueContainer.nameContainer.nameText;

            data.currentDialogue = dialogueText.text;
            data.dialogueFont = FilePaths.resources_fonts + dialogueText.font.name;
            data.dialogueColor = dialogueText.color;
            data.dialogueScale = dialogueText.fontSize;

            data.currentSpeaker = nameText.text;
            data.speakerFont = FilePaths.resources_fonts + nameText.font.name;
            data.speakerNameColor = nameText.color;
            data.speakerScale = nameText.fontSize;

            return data;
        }

        public static void Apply( DialogueData data)
        {
            var ds = DialogueSystem.instance;
            var dialogueText = ds.dialogueContainer.dialogueText;
            var nameText = ds.dialogueContainer.nameContainer.nameText;

            dialogueText.text = data.currentDialogue;
            dialogueText.color = data.dialogueColor;
            dialogueText.fontSize = data.dialogueScale;

            nameText.text = data.currentSpeaker;
            nameText.color = data.speakerNameColor;
            nameText.fontSize = data.speakerScale;

            if (data.dialogueFont != dialogueText.font.name)
            {
                TMP_FontAsset fontAsset = HistoryCache.LoadFont(data.dialogueFont);
                if(fontAsset != null)
                    dialogueText.font = fontAsset;
            }

            if (data.speakerFont != nameText.font.name)
            {
                TMP_FontAsset fontAsset = HistoryCache.LoadFont(data.speakerFont);
                if (fontAsset != null)
                    nameText.font = fontAsset;
            }

        }
    }
}