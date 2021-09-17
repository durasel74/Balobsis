using System;
using System.Collections.Generic;
using System.Text;

namespace Balobsis.Model
{
	public static class Parser
	{
        public static List<List<string>> ParseText(string text)
        {
            var parsedText = new List<List<string>>();
            var sentencesOfText = ParseSentences(text);
            if (sentencesOfText == null) return null;

            List<string> parsedSentence;
            foreach (var sentence in sentencesOfText)
            {
                parsedSentence = ParseWords(sentence);
                if (parsedSentence != null && parsedSentence.Count != 0)
                    parsedText.Add(parsedSentence);
            }
            return parsedText;
        }

        public static List<string> ParseSentences(string text)
        {
            if (text == null) return null;
            var sentencesList = new List<string>();
            var splitedSentences = text.Split(
                new char[] { '.', '!', '?', ';', ':', '(', ')' });
            foreach (var sentence in splitedSentences)
                if (sentence.Length != 0) sentencesList.Add(sentence);
            return sentencesList;
        }

        public static List<string> ParseWords(string sentence)
        {
            if (sentence == null) return null;
            var words = new List<string>();

            bool isRecording = false;
            StringBuilder word = new StringBuilder();
            char symbol;
            for (int i = 0; i < sentence.Length; i++)
            {
                symbol = sentence[i];
                isRecording = CheckSymbolCorrectness(symbol);
                if (isRecording) word.Append(char.ToLower(symbol));
                else if (word.Length != 0)
                {
                    words.Add(word.ToString());
                    word.Clear();
                }
            }
            if (word.Length != 0) words.Add(word.ToString());
            return words;
        }

        public static bool CheckSymbolCorrectness(char symbol)
        {
            return char.IsLetter(symbol) || symbol == '\'';
        }
    }
}
