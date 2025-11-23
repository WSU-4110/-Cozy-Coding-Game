using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitGameTests.GameLogic
{
    public class SplitGameLogic
    {
        public string Normalize(string s)
        {
            return s.ToLower()
                .Replace(" ", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("\"", "")
                .Trim();
        }

        public bool CheckSplit(string userInput, string correctSolution)
        {
            string user = Normalize(userInput);
            string correct = Normalize(correctSolution);
            return user == correct;
        }
    }
}
