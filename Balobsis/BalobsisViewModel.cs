using System;
using Balobsis.Model;

namespace Balobsis
{
	public class BalobsisViewModel : NotifyPropertyChanged
	{
		BalobsisUnit balobsis;
		private string phrase;
		private uint continueCount;
		private bool isProcessed;

		public BalobsisViewModel()
		{
			balobsis = new BalobsisUnit();
			ContinueCount = 1;
		}

		public string Phrase
		{
			get { return phrase; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					phrase = value;
					OnPropertyChanged("Phrase");
				}
			}
		}

		public uint ContinueCount
		{
			get { return continueCount; }
			set
			{
				continueCount = value;
				OnPropertyChanged("ContinueCount");
			}
		}

		public bool IsProcessed
		{
			get { return isProcessed; }
			set
			{
				isProcessed = value;
				OnPropertyChanged("IsProcessed");
			}
		}
	}
}
