using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    
    public class DialogueParser
    {
        private const string commandRegexPattern = @"[\w\[\]]*[^\s]\(";

        public static DIALOGUE_LINE Parse(string rawLine)
        {
            //Debug.Log($"Parsingline - '{rawLine}'");

            (string speaker, string dialogue, string commands) = RipContent(rawLine);

            //Debug.Log($"Speaker = '{speaker}' \nDialogue = '{dialogue}' \nCommands  = '{commands}' ");
            return new DIALOGUE_LINE(rawLine, speaker, dialogue, commands); 
        }

        private static (string, string, string) RipContent(string rawline)
        {
            string speaker = "", dialogue = "", commands = "";

            int dialogueStart = -1;
            int dialogueEnd = -1;
            bool isEscaped = false;

            for (int i = 0; i <rawline.Length; i++)
            {
                char current = rawline[i];
                if (current == '\\')
                    isEscaped = !isEscaped;
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if (dialogueEnd == -1)
                    {
                        dialogueEnd = i;
                        break;
                    }
                }
                else
                    isEscaped = false;
            }

            //Identigy Command Pattern
            Regex commandRegex = new Regex(commandRegexPattern);
            MatchCollection matches = commandRegex.Matches(rawline);
            int commandStart = -1;

            foreach(Match match in matches)
            {
                if(match.Index < dialogueStart || match.Index > dialogueEnd)
                {
                    commandStart = match.Index;
                    break;
                }
            }

            if (commandStart != -1 && (dialogueStart == -1 && dialogueEnd == -1))
                return ("", "", rawline.Trim());

            //If we are here them we either have dialogue or a multi word argument in a command. Figure out if this is dialogue
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {
                //We know that we have valid dialogue
                speaker = rawline.Substring(0, dialogueStart).Trim();
                dialogue = rawline.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"","\"");
                if (commandStart != -1)
                {
                    commands = rawline.Substring(commandStart).Trim();
                }
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
            {
                commands = rawline;
            }
            else
            {
                dialogue = rawline;
            }

            return (speaker, dialogue, commands);
        }
    }
}
