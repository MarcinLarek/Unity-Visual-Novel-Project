using CHARACTERS;
using COMMANDS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;

        public TextArchitect architect = null;
        private bool userPrompt = false;

        private TagManager tagManager;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;

            //For future. If you want to use it outside ConversationManager, convert it to Singleton
            tagManager = new TagManager();
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }
        public Coroutine StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));

            return process;
        }
        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogueSystem.StopCoroutine(process);
            process = null;
        }
        IEnumerator RunningConversation(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {
                //Dont show any blank lines or try to run any logic on them
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);
                
                //Show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                //Run any commands
                if (line.hasCommands)
                    yield return Line_RunCommands(line);

                //Wait for user input if dialogue ws in this line
                if (line.hasDialogue)
                {
                    //Wait for user input
                    yield return WiatForUserInput();
                    CommandManager.instance.StopAllProcesses();
                }

            }
        }
        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            //Show or hide the speaker name if there is one present.
            if (line.hasSpeaker)
                HandleSpeakerLogic(line.speakerData);

            if(!dialogueSystem.dialogueContainer.isVisible)
                dialogueSystem.dialogueContainer.Show();

            //Build dialogue
            yield return BuildLineSegments(line.dialogueData);
            

        }
        private void HandleSpeakerLogic(DL_SPEAKER_DATA speakerData)
        {
            bool characterMustBeCreated = (speakerData.makeCharacterEnter || speakerData.isCastingPosition || speakerData.isCastingExpressions);
            Character character = CharacterManager.instance.GetCharacter(speakerData.name, createIfDoesNotExist: characterMustBeCreated);

            if (speakerData.makeCharacterEnter && (!character.isVisible && !character.isRevealing))
                character.Show();

            //Add character name to the UI
            dialogueSystem.ShowSpeakerName(tagManager.Inject(speakerData.displayName));

            //Now customize the dialogue for this character - if applicable
            dialogueSystem.ApplySpeakerDataToDialogueContainer(speakerData.name);

            //Cast position
            if (speakerData.isCastingPosition)
                character.SetPosition(speakerData.castPosition);

            //Cast Expression
            if (speakerData.isCastingExpressions)
            {
                foreach (var ce in speakerData.CastExpressions)
                    character.OnReceiveCastingExpression(ce.layer, ce.expression);
            }
        }
        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            List<DL_COMMAND_DATA.Command> commands = line.commandData.commands;

            foreach(DL_COMMAND_DATA.Command command in commands)
            {
                if (command.waitForCompletion || command.name == "wait")
                {
                    CoroutineWrapper cw = CommandManager.instance.Execute(command.name, command.arguments);
                    while (!cw.IsDone)
                    {
                        if (userPrompt)
                        {
                            CommandManager.instance.StopCurrentProcess();
                            userPrompt = false;
                        }
                        yield return null;
                    }
                }
                else
                    CommandManager.instance.Execute(command.name, command.arguments);
            }
            yield return null;
        }
        IEnumerator BuildLineSegments(DL_DIALOGUE_DATA line)
        {
            for( int i = 0; i <line.segments.Count; i++)
            {
                DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];

                yield return WiatForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }
        public bool isWaitingOnAutoTimer { get; private set; } = false;
        IEnumerator WiatForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment)
        {
            switch (segment.startSignal)
            {
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.C:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return WiatForUserInput();
                    break;
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    isWaitingOnAutoTimer = true;
                    yield return new WaitForSeconds(segment.signalDelay);
                    isWaitingOnAutoTimer = false;
                    break;
                default:
                    break;
            }
        }
        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            dialogue = tagManager.Inject(dialogue);

            //Build the dialogue
            if (!append)
                architect.Build(dialogue);
            else
                architect.Append(dialogue);

            //Wait for the dialogue to complete.
            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();

                    userPrompt = false;
                }
                yield return null;
            }
        }
        IEnumerator WiatForUserInput()
        {
            dialogueSystem.prompt.Show();

            while(!userPrompt)
                yield return null;

            dialogueSystem.prompt.Hide();

            userPrompt = false;
        }
    }
}
