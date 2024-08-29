using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class TagManager
{
    private static readonly Dictionary<string, Func<string>> tags = new Dictionary<string, Func<string>>()
    {
        { "<mainChar>", () => "Protagon" },
        { "<input>", () => InputPanel.instance.lastInput }

    };

    private static readonly Regex tagRegex = new Regex("<\\w+>");

    public static string Inject(string text)
    {
        if (tagRegex.IsMatch(text))
        {
            foreach(Match match in tagRegex.Matches(text))
            {
                if(tags.TryGetValue(match.Value, out var tagValueRequest))
                {
                    text = text.Replace(match.Value, tagValueRequest());
                }
            }
        }
        return text;
    }
}
