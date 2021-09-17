using System;
using System.Threading.Tasks;
using Microsoft.Win32;
using Balobsis.Model;

namespace Balobsis
{
	public class BalobsisViewModel : NotifyPropertyChanged
	{
		object locker = new object();
		BalobsisCore balobsis;
		private string phrase;
		private uint continueCount;
		private bool isProcessed;
		private string fileName;

		public BalobsisViewModel()
		{
			balobsis = new BalobsisCore();
			continueCount = 3;
			isProcessed = false;
			FileName = "Нет файла";
		}

		public string Phrase
		{
			get { return phrase; }
			set
			{
				lock (locker)
				{
					if (!string.IsNullOrEmpty(value))
					{
						phrase = value;
						OnPropertyChanged("Phrase");
					}
				}
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

		public string FileName
		{
			get { return fileName; }
			set
			{
				fileName = value;
				OnPropertyChanged("FileName");
			}
		}

		private ButtonCommand importFileCommand;
		public ButtonCommand ImportFileCommand
		{
			get
			{
				return importFileCommand ??
				  (importFileCommand = new ButtonCommand(obj =>
				  {
					  var dialog = new OpenFileDialog();
					  dialog.Filter = "Text files (*.txt)|*.txt";
					  if (dialog.ShowDialog() == true)
						  ImportFileAsync(dialog.FileName);
				  }));
			}
		}

		private ButtonCommand continuePhraseCommand;
		public ButtonCommand ContinuePhraseCommand
		{
			get
			{
				return continuePhraseCommand ??
				  (continuePhraseCommand = new ButtonCommand(obj =>
				  {
					  Phrase = balobsis.ContinuePhrase(Phrase, continueCount);
				  }));
			}
		}

		private ButtonCommand randomPhraseCommand;
		public ButtonCommand RandomPhraseCommand
		{
			get
			{
				return randomPhraseCommand ??
				  (randomPhraseCommand = new ButtonCommand(obj =>
				  {
					  Phrase = balobsis.ContinueRandomPhrase(continueCount);
				  }));
			}
		}

		private void ImportFile(string filePath)
		{
			var importResult = balobsis.ImportFile(filePath);
			if (importResult) FileName = balobsis.FileName;
		}

		private async void ImportFileAsync(string filePath)
		{
			isProcessed = true;
			await Task.Run(() => ImportFile(filePath));
			isProcessed = false;
		}
	}
}
