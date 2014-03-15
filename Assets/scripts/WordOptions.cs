using UnityEngine;
using System.Collections.Generic;	

public class WordOptions {

	public enum Difficulty {Easy, Medium, Hard};

	public static List<string> GetStrings(Difficulty difficulty){
		switch (difficulty){
			case Difficulty.Easy: return EasyStrings();
			case Difficulty.Medium: return MediumStrings();
			case Difficulty.Hard: return HardStrings();
		}

		return null;
	}

	private static List<string> EasyStrings() {
		List<string> strings = new List<string>();
		while(strings.Count < 3){
			string word = Main.AllWords[(int) (Main.AllWords.Count * Random.value)];
			if(!strings.Contains(word)){
				strings.Add(word);
			}
		}
		return strings;
	}

	private static List<string> MediumStrings() {
		List<string> strings = new List<string>();
		string baseWord = Main.AllWords[(int) (Main.AllWords.Count * Random.value)];
		strings.Add(baseWord);
		while(strings.Count < 3){
			string word = Main.WordsBySize[baseWord.Length][(int) (Main.WordsBySize[baseWord.Length].Count * Random.value)];
			if(!strings.Contains(word)){
				strings.Add(word);
			}
		}
		return strings;
	}

	private static List<string> HardStrings() {
		List<string> baseAnagrams = Main.AnagramLists[(int) (Main.AnagramLists.Count * Random.value)];
		List<string> strings = new List<string>();
		while(strings.Count < 3){
			string word = baseAnagrams[(int) (baseAnagrams.Count * Random.value)];
			if(!strings.Contains(word)){
				strings.Add(word);
			}
		}
		return strings;
	}

}