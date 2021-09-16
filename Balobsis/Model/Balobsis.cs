using System;
using System.Collections.Generic;
using System.IO;

namespace Balobsis.Model
{
	public class BalobsisUnit
	{
		List<List<string>> parsedText;
		Dictionary<string, string> nextWords;

		public bool ImportFile(string filePath)
		{
			if (!File.Exists(filePath)) return false;

			var text = File.ReadAllText(filePath);
			parsedText = Parser.ParseText(text);
			nextWords = NgramGenerator.GetMostFrequentNextWords(parsedText);
			return true;
		}

		public string ContinuePhrase(string phrase, int continueCount = 1)
		{
			var resultText = TextGenerator.ContinuePhrase(nextWords, phrase,
				continueCount);
			return resultText;
		}
	}
}
