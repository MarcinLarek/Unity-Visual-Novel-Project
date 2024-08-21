using System.Collections.Generic;

namespace DIALOGUE
{
    public class Conversation
    {
        private List<string> lines = new List<string>();
        private int progress = 0;

        public Conversation(List<string> lines, int progress = 0)
        {
            this.lines = lines;
            this.progress = progress;
        }

        public int GetProgress() => progress;
        public void SetProgress(int value) => progress = value;
        public void IncrementProgrss() => progress++;
        public int Count => lines.Count;
        public List<string> GetLines() => lines;
        public string CurrentLiune() => lines[progress];
        public bool hasReachedEnd() => progress >= lines.Count;
    }
}