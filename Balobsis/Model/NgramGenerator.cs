using System;
using System.Collections.Generic;

namespace Balobsis.Model
{
	public static class NgramGenerator
	{
        public static Dictionary<string, string> GetMostFrequentNextWords(
            List<List<string>> text)
        {
            var ngramsFrequency = FindNgrams(text);
            var result = CreateNgramByFrequency(ngramsFrequency);
            return result;
        }

        public static Dictionary<string, Dictionary<string, int>> FindNgrams(
            List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> resultFrequency =
                new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                if (sentence.Count < 2) continue;
                FindBigram(sentence, resultFrequency);
                FindThreegram(sentence, resultFrequency);
            }
            return resultFrequency;
        }

        private static void FindBigram(List<string> sentence,
            Dictionary<string, Dictionary<string, int>> bigramFrequency)
        {
            if (sentence.Count < 2) return;

            string currentWord;
            string nextWord;
            Dictionary<string, int> newNode;
            for (int i = 0; i < sentence.Count - 1; i++)
            {
                currentWord = sentence[i];
                nextWord = sentence[i + 1];

                if (!bigramFrequency.ContainsKey(currentWord))
                {
                    newNode = new Dictionary<string, int>();
                    newNode.Add(nextWord, 1);
                    bigramFrequency.Add(currentWord, newNode);
                }
                else
                {
                    if (!bigramFrequency[currentWord].ContainsKey(nextWord))
                        bigramFrequency[currentWord].Add(nextWord, 1);
                    else
                        bigramFrequency[currentWord][nextWord]++;
                }
            }
        }

        private static void FindThreegram(List<string> sentence,
            Dictionary<string, Dictionary<string, int>> threegramFrequency)
        {
            if (sentence.Count < 3) return;

            string currentWord;
            string nextWord;
            Dictionary<string, int> newNode;
            for (int i = 0; i < sentence.Count - 2; i++)
            {
                currentWord = sentence[i] + " " + sentence[i + 1];
                nextWord = sentence[i + 2];

                if (!threegramFrequency.ContainsKey(currentWord))
                {
                    newNode = new Dictionary<string, int>();
                    newNode.Add(nextWord, 1);
                    threegramFrequency.Add(currentWord, newNode);
                }
                else
                {
                    if (!threegramFrequency[currentWord].ContainsKey(nextWord))
                        threegramFrequency[currentWord].Add(nextWord, 1);
                    else
                        threegramFrequency[currentWord][nextWord]++;
                }
            }
        }

        public static Dictionary<string, string> CreateNgramByFrequency(
             Dictionary<string, Dictionary<string, int>> ngramFrequency)
        {
            var ngramsDictionary = new Dictionary<string, string>();
            var dropoutNgrams = new Dictionary<string, (string, int)>();

            string analysisWord;
            foreach (var node in ngramFrequency)
            {
                foreach (var word in node.Value)
                {
                    if (!dropoutNgrams.ContainsKey(node.Key))
                        dropoutNgrams[node.Key] = (word.Key, word.Value);

                    if (dropoutNgrams[node.Key].Item2 < word.Value)
                        dropoutNgrams[node.Key] = (word.Key, word.Value);
                    else if (dropoutNgrams[node.Key].Item2 == word.Value)
                    {
                        analysisWord = LexicographicAnalysis(dropoutNgrams[node.Key].Item1, word.Key);
                        dropoutNgrams[node.Key] = (analysisWord, word.Value);
                    }
                }
            }
            foreach (var pair in dropoutNgrams)
                ngramsDictionary.Add(pair.Key, pair.Value.Item1);
            return ngramsDictionary;
        }

        public static string LexicographicAnalysis(string firstWord,
            string secondWord)
        {
            string result = firstWord;
            var compareResult = String.CompareOrdinal(firstWord, secondWord);
            if (compareResult > 0) result = secondWord;
            return result;
        }
    }
}
