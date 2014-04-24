using UnityEngine;
using System.Collections.Generic;	

public class Scores {

	public const int ScoreHistory = 5;

	private Dictionary<WordOptions.Difficulty, List<float>> lifetimeScores;
	private Dictionary<WordOptions.Difficulty, List<float>> lifetimeAverages;

	public float LastScoreImpact { get { return lastScoreImpact; } }
	public float SessionScore { get { return sessionScore; } }
	public float SessionAverage { get { return sessionAverage; } }
	public float SessionCount { get { return sessionCount; } }

	private WordOptions.Difficulty sessionDifficulty;
	private float lastScoreImpact;
	private float sessionScore;
	private float sessionAverage;
	private int sessionCount;

	public Scores(WordOptions.Difficulty sessionDifficulty){
		lifetimeScores = new Dictionary<WordOptions.Difficulty, List<float>>();
		lifetimeAverages = new Dictionary<WordOptions.Difficulty, List<float>>();

		foreach(WordOptions.Difficulty difficulty in WordOptions.Difficulty.GetValues(typeof(WordOptions.Difficulty))) {
			lifetimeScores.Add(difficulty, new List<float>());
			lifetimeAverages.Add(difficulty, new List<float>());

			for(int i=0; i<ScoreHistory; i++){
				if( PlayerPrefs.HasKey(LifetimeScoreName(difficulty, i)) ) {
					lifetimeScores[difficulty].Add(PlayerPrefs.GetFloat(LifetimeScoreName(difficulty, i)));
				}

				if( PlayerPrefs.HasKey(LifetimeAverageName(difficulty, i)) ) {
					lifetimeAverages[difficulty].Add(PlayerPrefs.GetFloat(LifetimeAverageName(difficulty, i)));
				}
			}
		}

		this.sessionDifficulty = sessionDifficulty;
		lastScoreImpact = 0;
		sessionScore = 0;
		sessionAverage = 0;
		sessionCount = 0;

	}

	public void ScorePoints(float points){
		sessionScore += points;
		sessionAverage = ((sessionAverage * sessionCount) + (points)) / (sessionCount + 1f);
		sessionCount++;
		lastScoreImpact = points;
	}

	public void ResetSession(){
		sessionScore = 0;
		sessionAverage = 0;
		sessionCount = 0;
	}

	public void CheckForLifetimeRecords(){
		lifetimeScores[this.sessionDifficulty].Add(sessionScore);
		lifetimeScores[this.sessionDifficulty].Sort();
		lifetimeScores[this.sessionDifficulty].Reverse();
		while(lifetimeScores[this.sessionDifficulty].Count > 5) {
			lifetimeScores[this.sessionDifficulty].RemoveAt(5);
		}
		for(int i=0; i<lifetimeScores[this.sessionDifficulty].Count; i++) {
			PlayerPrefs.SetFloat(LifetimeScoreName(this.sessionDifficulty, i), lifetimeScores[this.sessionDifficulty][i]);
		}

		lifetimeAverages[this.sessionDifficulty].Add(sessionAverage);
		lifetimeAverages[this.sessionDifficulty].Sort();
		lifetimeAverages[this.sessionDifficulty].Reverse();
		while(lifetimeAverages[this.sessionDifficulty].Count > 5) {
			lifetimeAverages[this.sessionDifficulty].RemoveAt(5);
		}
		for(int i=0; i<lifetimeAverages[this.sessionDifficulty].Count; i++) {
			PlayerPrefs.SetFloat(LifetimeAverageName(this.sessionDifficulty, i), lifetimeAverages[this.sessionDifficulty][i]);
		}
	}

	// public bool HasLifetime(WordOptions.Difficulty difficulty){
	// 	return lifetimeScores.ContainsKey(difficulty);
	// }

	public List<float> LifetimeScores(WordOptions.Difficulty difficulty){
		return lifetimeScores[difficulty];
	}

	public List<float> LifetimeAverages(WordOptions.Difficulty difficulty){
		return lifetimeAverages[difficulty];
	}

	private static string LifetimeScoreName(WordOptions.Difficulty difficulty, int place) {
		return difficulty.ToString() + "Score" + place.ToString();
	}

	private static string LifetimeAverageName(WordOptions.Difficulty difficulty, int place) {
		return difficulty.ToString() + "Average" + place.ToString();
	}

}