using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class DL_SPEAKER_DATA
{
    public string name, castName;
    /// <summary>
    /// This is the name that wil display in the dialogue box to show who is speaking.
    /// </summary>
    public string displayName => (castName != string.Empty ? castName : name);
    public Vector2 castPosition;

    private const string NAMECAST_ID = " as ";
    private const string POSITIONCAST_ID = " at ";
    private const string EXPRESSIONCAST_ID = @" [";
    private const char AXIS_DELIMITER = ':';
    private const char EXPRESSIONLAYER_JOINER = ',';
    private const char EXPRESSIONLAYER_DELIMITER = ':';

    public List<(int layer, string expression)> CastExpressions { get; set; }

    public DL_SPEAKER_DATA(string rawSpeaker)
    {
        string pattern = @$"{NAMECAST_ID}|{POSITIONCAST_ID}|{EXPRESSIONCAST_ID.Insert(EXPRESSIONCAST_ID.Length-1, @"\")}";
        MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

        //Populate this data to avoid null references to values
        castName = "";
        castPosition = Vector2.zero;
        CastExpressions = new List<(int layer, string expression)>();

        //If there are no matches, then this entire line is the speaker name
        if (matches.Count == 0)
        {
            name = rawSpeaker;
            return;
        }

        //Otherwise, isolate the speakername from the casting data
        int index = matches[0].Index;
        name = rawSpeaker.Substring(0, index);

        for(int i = 0; i < matches.Count; i++)
        {
            Match match = matches[i];
            int startIndex = 0, endIndex = 0;

            if (match.Value == NAMECAST_ID)
            {
                startIndex = match.Index + NAMECAST_ID.Length;
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);
            }
            else if (match.Value == POSITIONCAST_ID)
            {
                startIndex = match.Index + POSITIONCAST_ID.Length;
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                string[] axis = castPos.Split(AXIS_DELIMITER, System.StringSplitOptions.RemoveEmptyEntries);

                float.TryParse(axis[0], out castPosition.x);

                if (axis.Length > 1)
                    float.TryParse(axis[1], out castPosition.y);
            }
            else if (match.Value == EXPRESSIONCAST_ID)
            {
                startIndex = match.Index + EXPRESSIONCAST_ID.Length;
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                string castExp = rawSpeaker.Substring(startIndex, endIndex - startIndex +1);

                CastExpressions = castExp.Split(EXPRESSIONLAYER_JOINER)
                    .Select(x => 
                    {
                        var parts = x.Trim().Split(EXPRESSIONLAYER_DELIMITER);
                        return (int.Parse(parts[0]), parts[1]);
                    }).ToList();
            }
        }

    }
}
