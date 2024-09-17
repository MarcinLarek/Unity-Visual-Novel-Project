using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace History
{
    public class HistoryLogManager : MonoBehaviour
    {
        private const float LOG_STARTING_HEIGHT = 43f;
        private const float LOG_HEIGHT_PER_LINE = 35f;
        private const float DEFAULT_HEIGHT = 43f;
        private const float TEXT_DEFAULT_SCALE = 1f;

        private const string NAMETEXT_NAME = "NameText";
        private const string DIALOGUETEXT_NAME = "DialogueText";

        private float logScaling = 1f;

        [SerializeField] private Animator anim;
        [SerializeField] private GameObject logPrefab;

        HistoryManager manager => HistoryManager.instance;
        private List<HistoryLog> logs = new List<HistoryLog>();

        public bool isOpen { get; private set; } = false;

        [SerializeField] private SliderJoint2D logScaleSlider;
        public void Open()
        {
            if (isOpen)
                return;

            anim.Play("Open");
            isOpen = true;
        }

        public void Close()
        {
            if (!isOpen)
                return;

            anim.Play("Close");
            isOpen = false;
        }
        
        public void AddLog(HistoryState state)
        {
            if (logs.Count >= HistoryManager.HISTORY_CACHE_LIMIT)
            {
                DestroyImmediate(logs[0].container);
                logs.RemoveAt(0);
            }

            CreateLog(state);
        }

        public void CreateLog(HistoryState state)
        {
            HistoryLog log = new HistoryLog();

            log.container = Instantiate(logPrefab, logPrefab.transform.parent);
            log.container.SetActive(true);

            log.nameText = log.container.transform.Find(NAMETEXT_NAME).GetComponent<TextMeshProUGUI>();
            log.dialogueText = log.container.transform.Find(DIALOGUETEXT_NAME).GetComponent<TextMeshProUGUI>();

            if(state.dialogue.currentSpeaker == string.Empty)
            {
                log.nameText.text = string.Empty;
            }
            else
            {
                log.nameText.text = state.dialogue.currentSpeaker;
                log.nameText.font = HistoryCache.LoadFont(state.dialogue.speakerFont);
                log.nameText.color = state.dialogue.speakerNameColor;
                log.nameFontSize = TEXT_DEFAULT_SCALE * state.dialogue.speakerScale;
            }
        }

    }
}