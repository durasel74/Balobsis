using System;
using System.Collections.Generic;
using System.IO;

namespace Balobsis.Model
{
	public class BalobsisCore
	{
		Random random = new Random();
		List<List<string>> parsedText;
		Dictionary<string, string> nextWords;
		bool isImported = false;

		public string FileName { get; private set; }

		public bool ImportFile(string filePath)
		{
			if (!File.Exists(filePath)) return false;

			FileInfo file = new FileInfo(filePath);
			FileName = file.Name;
			var text = File.ReadAllText(filePath);
			parsedText = Parser.ParseText(text);
			nextWords = NgramGenerator.GetMostFrequentNextWords(parsedText);
			isImported = true;
			return true;
		}

		public string ContinuePhrase(string phrase, uint continueCount = 1)
		{
			if (!isImported) return phrase;
			var resultText = TextGenerator.ContinuePhrase(nextWords,
				phrase, continueCount);
			return resultText;
		}

		public string ContinueRandomPhrase(uint continueCount = 1)
		{
			if (!isImported) return "";
			string randomWord = GetRandomWord();
			string resultPhrase = ContinuePhrase(randomWord, continueCount);
			return resultPhrase;
		}

		private string GetRandomWord()
		{
			string resultWord = "";
			int wordsCount = nextWords.Count;
			var randomIndex = random.Next(wordsCount);

			int i = 0;
			foreach (var element in nextWords)
			{
				if (i == randomIndex)
				{
					resultWord += element.Key;
					break;
				}
				i++;
			}
			return resultWord;
		}
	}
}
