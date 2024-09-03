using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DIALOGUE.LogicalLines.LogicalLineUtils.Encapsulation;
namespace DIALOGUE.LogicalLines
{
    public class LL_Choice : ILogicalLine
    {
        public string keyword => "choice";

        private const char CHOICE_IDENTIFIER = '-';

        public IEnumerator Execute(DIALOGUE_LINE line)
        {
            var currentConverstation = DialogueSystem.instance.conversationManager.conversation;
            var progess = DialogueSystem.instance.conversationManager.conversationProgess;

            EncapulatedData data = RipEncapsulationData(currentConverstation, progess, ripHeaderAndEncapsulators: true);
            List<Choice> choices = GetChoicesFromData(data);

            string title = line.dialogueData.rawData;
            ChoicePanel panel = ChoicePanel.instance;
            string[] choiceTitles = choices.Select(c => c.title).ToArray();

            panel.Show(title, choiceTitles);

            while(panel.isWaitingOnUserChoice)
                yield return null;

            Choice selectedChoice = choices[panel.lastDecision.answerIndex];

            Conversation newConversation = new Conversation(selectedChoice.resultLines);
            DialogueSystem.instance.conversationManager.conversation.SetProgress(data.endingIndex);
            DialogueSystem.instance.conversationManager.EnqueuePriority(newConversation);
        }

        public bool Matches(DIALOGUE_LINE line)
        {
            return (line.hasSpeaker && line.speakerData.name.ToLower() == keyword);
        }

        private List<Choice> GetChoicesFromData(EncapulatedData data)
        {
            List<Choice> choices = new List<Choice>();
            int encapsulationDepth = 0;
            bool isFirstChoice = true;

            Choice choice = new Choice
            {
                title = String.Empty,
                resultLines = new List<string>(),
            };
            foreach(var line in data.lines.Skip(1))
            {
                if (IsChoiceStart(line) && encapsulationDepth == 1)
                {
                    if (!isFirstChoice)
                    {
                        choices.Add(choice);
                        choice = new Choice
                        {
                            title = string.Empty,
                            resultLines = new List<string>(),
                        };
                    }

                    choice.title = line.Trim().Substring(1);
                    isFirstChoice = false;
                    continue;
                }

                AddLineToResults(line, ref choice, ref encapsulationDepth);
            }

            if (!choices.Contains(choice))
                choices.Add(choice);

            return choices;
        }
        private void AddLineToResults(string line, ref Choice choice, ref int encapsulationDepth)
        {
            line.Trim();
            if (IsIncapsulationStart(line))
            {
                if (encapsulationDepth > 0)
                    choice.resultLines.Add(line);

                encapsulationDepth++;
                return;
            }

            if (IsIncapsulationEnd(line))
            {
                encapsulationDepth--;
                if (encapsulationDepth > 0)
                    choice.resultLines.Add(line);

                return;
            }

            choice.resultLines.Add(line);

        }

        private bool IsChoiceStart(string line) => line.Trim().StartsWith(CHOICE_IDENTIFIER);

        private struct Choice
        {
            public string title;
            public List<string> resultLines;
        }
    }
}