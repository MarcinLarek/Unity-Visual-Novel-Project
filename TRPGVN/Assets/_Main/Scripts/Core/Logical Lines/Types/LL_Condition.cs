using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DIALOGUE.LogicalLines.LogicalLineUtils.Encapsulation;
using static DIALOGUE.LogicalLines.LogicalLineUtils.Conditions;


namespace DIALOGUE.LogicalLines
{
    public class LL_Condition : ILogicalLine
    {
        public string keyword => "if";
        private const string ELSE = "else";
        private readonly string[] CONTAINERS = new string[] { "(", ")" };

        public IEnumerator Execute(DIALOGUE_LINE line)
        {
            string rawCondition = ExtractCondition(line.rawData.Trim());
            bool conditionResult = EvaluateCondition(rawCondition);

            Conversation currentConversation = DialogueSystem.instance.conversationManager.conversation;
            int currentProgess = DialogueSystem.instance.conversationManager.conversationProgess;

            EncapulatedData ifData = RipEncapsulationData(currentConversation, currentProgess, ripHeaderAndEncapsulators: false);
            EncapulatedData elseData = new EncapulatedData();

            if(ifData.endingIndex + 1 < currentConversation.Count)
            {
                string nextline = currentConversation.GetLines()[ifData.endingIndex + 1].Trim();
                if (nextline == ELSE)
                {
                    elseData = RipEncapsulationData(currentConversation, ifData.endingIndex + 1, false);
                    ifData.endingIndex = elseData.endingIndex;
                }
            }

            currentConversation.SetProgress(ifData.endingIndex);
            EncapulatedData selData = conditionResult ? ifData : elseData;
            if (!selData.isNull && selData.lines.Count > 0)
            {
                Conversation newConversation = new Conversation(selData.lines);
                DialogueSystem.instance.conversationManager.EnqueuePriority(newConversation);
            }


            yield return null;
        }

        public bool Matches(DIALOGUE_LINE line)
        {
            return line.rawData.Trim().StartsWith(keyword);
        }

        private string ExtractCondition(string line)
        {
            int startIndex = line.IndexOf(CONTAINERS[0]) + 1;
            int endIndex = line.IndexOf(CONTAINERS[1]);

            return line.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}