using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE.LogicalLines
{
    public static class LogicalLineUtils
    {

        public static class Encapsulation
        {
            public struct EncapulatedData
            {
                public List<string> lines;
                public int startingIndex;
                public int endingIndex;

            }

            private const char ENCAOSULATION_START = '{';
            private const char ENCAOSULATION_END = '}';

            public static bool IsIncapsulationStart(string line) => line.Trim().StartsWith(ENCAOSULATION_START);
            public static bool IsIncapsulationEnd(string line) => line.Trim().StartsWith(ENCAOSULATION_END);

            public static EncapulatedData RipEncapsulationData(Conversation conversation, int startingIndex, bool ripHeaderAndEncapsulators = false)
            {
                int encapsulationDepth = 0;
                EncapulatedData data = new EncapulatedData { lines = new List<string>(), startingIndex = startingIndex, endingIndex = 0 };

                for (int i = startingIndex; i < conversation.Count; i++)
                {
                    string line = conversation.GetLines()[i];

                    if (ripHeaderAndEncapsulators || (encapsulationDepth > 0 && !IsIncapsulationEnd(line)))
                        data.lines.Add(line);

                    if (IsIncapsulationStart(line))
                    {
                        encapsulationDepth++;
                        continue;
                    }
                    if (IsIncapsulationEnd(line))
                    {
                        encapsulationDepth--;
                        if (encapsulationDepth == 0)
                        {
                            data.endingIndex = i;
                            break;
                        }
                    }
                }
                return data;
            }


        }

    }
}