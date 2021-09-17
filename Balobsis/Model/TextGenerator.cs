using System;
using System.Collections.Generic;
using System.Text;

namespace Balobsis.Model
{
	public static class TextGenerator
	{
        public static string ContinuePhrase(Dictionary<string, string> nextWords,
            string phraseBeginning, uint wordsCount)
        {
            string resultPhrase = phraseBeginning;

            List<string> words;
            string phrase;
            for (int i = 0; i < wordsCount; i++)
            {
                words = Parser.ParseWords(resultPhrase);
                if (words.Count >= 2)
                {
                    phrase = ContinueSentence(words, nextWords, 2);
                    if (phrase.Length > 0) resultPhrase += " " + phrase;
                }
                else if (words.Count == 1)
                {
                    phrase = ContinueSentence(words, nextWords, 1);
                    if (phrase.Length > 0) resultPhrase += " " + phrase;
                }
                else break;
            }
            return resultPhrase;
        }

        public static string ContinueSentence(List<string> sentence,
            Dictionary<string, string> nextWords, int lastWordCount = 1)
        {
            if (sentence.Count < lastWordCount) return "";
            string resultWord;

            StringBuilder word = new StringBuilder();
            for (int i = 0; i < lastWordCount; i++)
            {
                word.Clear();
                word.Append(sentence[sentence.Count - 1]);
                for (int j = 1; j < lastWordCount - i; j++)
                    word.Insert(0, sentence[sentence.Count - 1 - j] + " ");
                resultWord = word.ToString();
                if (nextWords.ContainsKey(resultWord))
                    return nextWords[resultWord];
            }
            return "";
        }
    }
}
