using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace WordCounterConsoleApp1
{
	static class TextExtensions
	{
		public static IEnumerable<string> SplitIntoSentences(this string text)
		{
			string pattern = @"(?<!Mr|Mrs|Dr|Ms)\.(?<=['""A-Za-z0-9][\.\!\?])\s+(?=[A-Z])";
			return Regex.Split(text, pattern);
		}

		public static string CountWord(this IEnumerable<string> sentences, string findWord)
		{
			List<string> sentenceNumbers = new List<string>();
			sentences
				.Select((sentence, pos) => new
				{
					Index = pos + 1,
					Sentence = sentence
				})
				.ToList()
				.ForEach(element =>
				{
					string[] words = element.Sentence.Split(new char[] { '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
					var matchWords = from word in words
							 where word.Replace(".", "").ToLowerInvariant() == findWord.Replace(".", "").ToLowerInvariant()
							 select word;
					int wordCount = matchWords.Count();
					if (wordCount != 0)
                    {
						for (int i = 0; i < wordCount; i++)
                        {
							sentenceNumbers.Add(element.Index.ToString());
                        }
					}
				});
			string retval = findWord + " {" + sentenceNumbers.Count.ToString() + ":" + string.Join(",", sentenceNumbers) + "}";
			return retval;
		}
	}

	public class Program
	{
		public static void Main()
		{
			Console.Write(@"Please type in article file path, e.g., c:\temp\files\input\Article.txt : ");
			string articleFile;
			articleFile = Console.ReadLine();

			Console.Write(@"Please type in words file path, e.g., c:\temp\files\input\Words.txt : ");
			string wordsFile;
			wordsFile = Console.ReadLine();

			Console.Write(@"Please type in output file path, e.g., c:\temp\files\output\Output.txt : ");
			string outputFile;
			outputFile = Console.ReadLine();

			try
			{
				string article = System.IO.File.ReadAllText(@articleFile);
				string[] words = System.IO.File.ReadAllLines(@wordsFile);
				using (StreamWriter writer = new StreamWriter(@outputFile))
				{
					foreach (var (word, index) in words.Select((value, i) => (value, i)))
					{
						string wordStat = " " + AlphaBullet(index) + ". " + article.SplitIntoSentences().CountWord(word);
						Console.WriteLine(wordStat);
						writer.WriteLine(wordStat);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static string AlphaBullet(int wordIndex)
		{
			var alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
			if (wordIndex >= alphabet.Length)
			{
				return new String(alphabet[wordIndex % alphabet.Length], (wordIndex / alphabet.Length) + 1);
			}
			else
			{
				return "" + alphabet[wordIndex];
			}
		}

	}
}
