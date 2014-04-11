using UnityEngine;
using System.Collections.Generic;	

public class Scores {

	private Dictionary<WordOptions.Difficulty, float> lifetimeScores;
	private Dictionary<WordOptions.Difficulty, float> lifetimeAverages;

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
		lifetimeScores = new Dictionary<WordOptions.Difficulty, float>();
		lifetimeAverages = new Dictionary<WordOptions.Difficulty, float>();

		foreach(WordOptions.Difficulty difficulty in WordOptions.Difficulty.GetValues(typeof(WordOptions.Difficulty))) {
			if( PlayerPrefs.HasKey(LifetimeScoreName(difficulty))) {
				lifetimeScores.Add(difficulty, PlayerPrefs.GetFloat(LifetimeScoreName(difficulty)));
			}

			if( PlayerPrefs.HasKey(LifetimeAverageName(difficulty))) {
				lifetimeAverages.Add(difficulty, PlayerPrefs.GetFloat(LifetimeAverageName(difficulty)));
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
	}

	public void ResetSession(){
		lastScoreImpact = 0;
		sessionScore = 0;
		sessionAverage = 0;
		sessionCount = 0;
	}

	public void CheckForLifetimeRecords(){
		if(!lifetimeScores.ContainsKey(this.sessionDifficulty) || lifetimeScores[this.sessionDifficulty] < sessionScore){
			lifetimeScores[this.sessionDifficulty] = sessionScore;
			PlayerPrefs.SetFloat(LifetimeScoreName(this.sessionDifficulty), sessionScore);
		}
		if(!lifetimeAverages.ContainsKey(this.sessionDifficulty) || lifetimeAverages[this.sessionDifficulty] < sessionAverage){
			lifetimeAverages[this.sessionDifficulty] = sessionAverage;
			PlayerPrefs.SetFloat(LifetimeAverageName(this.sessionDifficulty), sessionAverage);
		}
	}

	public bool HasLifetime(WordOptions.Difficulty difficulty){
		return lifetimeScores.ContainsKey(difficulty);
	}

	public float LifetimeScore(WordOptions.Difficulty difficulty){
		return lifetimeScores[difficulty];
	}

	public float LifetimeAverage(WordOptions.Difficulty difficulty){
		return lifetimeAverages[difficulty];
	}

	private static string LifetimeScoreName(WordOptions.Difficulty difficulty) {
		return difficulty.ToString() + "Score";
	}

	private static string LifetimeAverageName(WordOptions.Difficulty difficulty) {
		return difficulty.ToString() + "Average";
	}

}