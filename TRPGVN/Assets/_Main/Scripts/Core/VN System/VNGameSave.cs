using DIALOGUE;
using History;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VISUALNOVEL
{
    [System.Serializable]
    public class VNGameSave
    {
        public static VNGameSave activeFile = null;

        public const string FILE_TYPE = ".save";
        public const string SCREENSHOT_FILE_TYPE = ".jpg";
        public const bool ENCRYPT_FILES = false;

        public string filePath => $"{FilePaths.gameSaves}{slotNumber}{FILE_TYPE}";
        public string screenshot => $"{FilePaths.gameSaves}{slotNumber}{SCREENSHOT_FILE_TYPE}";

        public string playerName;
        public int slotNumber = 1;

        public string[] activeConversations;
        public HistoryState activeState;
        public HistoryState[] historyLogs;

        public void Save()
        {
            activeState = HistoryState.Capture();
            historyLogs = HistoryManager.instance.history.ToArray();
            activeConversations = GetConversationData();

            string saveJson = JsonUtility.ToJson(this);
            FileManager.Save(filePath, saveJson);
        }

        public void Load()
        {
            if (activeFile != null)
                activeState.Load();

            HistoryManager.instance.history = historyLogs.ToList();

            SetConversationData();
        }

        private string[] GetConversationData()
        {
            List<string> retData = new List<string>();
            var conversations = DialogueSystem.instance.conversationManager.GetConversationQueue();

            for (int i = 0; i < conversations.Length; i++)
            {
                var conversation = conversations[i];
                string data = "";

                if(conversation.file != string.Empty)
                {
                    var compressedData = new VN_ConversationDataCompress();
                    compressedData.fileName = conversation.file;
                    compressedData.progress = conversation.GetProgress();
                    compressedData.startIndex = conversation.fileStartIndex;
                    compressedData.endIndex = conversation.fileEndIndex;
                    data = JsonUtility.ToJson(compressedData);
                }
                else
                {
                    var fullData = new VN_ConversationData();
                    fullData.conversation = conversation.GetLines();
                    fullData.progress = conversation.GetProgress();
                    data = JsonUtility.ToJson(fullData);
                }

                retData.Add(data);
            }
            return retData.ToArray();
        }

        private void SetConversationData()
        {
            for(int i = 0; i < activeConversations.Length; i++)
            {
                string data = activeConversations[i];
                Conversation conversation = null;
                var fullData = JsonUtility.FromJson<VN_ConversationData>(data);

                if(fullData != null)
                {
                    conversation = new Conversation(fullData.conversation, fullData.progress);
                }
                else
                {
                    var compressedData = JsonUtility.FromJson<VN_ConversationDataCompress>(data);
                    if(compressedData != null)
                    {
                        TextAsset file = Resources.Load<TextAsset>(compressedData.fileName);
                        List<string> lines = FileManager.ReadTextAsset(file);
                        conversation = new Conversation(lines, compressedData.progress, compressedData.fileName, compressedData.startIndex, compressedData.endIndex);
                    }
                }
            }
        }

    }
}