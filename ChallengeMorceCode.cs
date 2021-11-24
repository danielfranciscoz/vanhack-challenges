using System.Collections.Generic;
using System.Linq;

namespace base64
{
    public class ChallengeMorceCode
    {
        static string unKnownSignal = "?";

        static Dictionary<string, string> morceCode = new Dictionary<string, string>()
        {
            { "T", "-" },
            { "E", "." },
            { "M", "--" },
            { "N", "-." },
            { "A", ".-" },
            { "I", ".." },
            { "O", "---" },
            { "G", "--." },
            { "K", "-.-" },
            { "D", "-.." },
            { "W", ".--" },
            { "R", ".-." },
            { "U", "..-" },
            { "S", "..." },
        };

        public static string[] Possibilities(string signals)
        {
            string[] signalResult;

            if (signals.Contains(unKnownSignal))
            {
                signalResult = Combinations(signals).ToArray();
            }
            else
            {
                signalResult = new string[] { signals };
            }
            return morceCode
                      .Where(w =>
                          w.Value.Length == signals.Length &&
                          (signalResult.Any(a => a == w.Value))
                      )
                      .Select(s => s.Key)
                      .Reverse()
                      .ToArray();

        }

        public static List<string> Combinations(string data, List<string> temp = null)
        {
            if (temp == null)
            {
                temp = new List<string>();
            }

            string dot = "", dash = "";
            if (data.Contains(unKnownSignal))
            {
                dot = ReplaceFirstOccurrence(data, unKnownSignal, ".");
                dash = ReplaceFirstOccurrence(data, unKnownSignal, "-");

                Combinations(dot, temp);
                Combinations(dash, temp);
            }
            else
            {
                temp.Add(data);
            }


            return temp;
        }

        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

    }
}