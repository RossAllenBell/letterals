using UnityEngine;
using System.Collections.Generic;

public class GameScreen : Gui {

	public const float PreviewSeconds = 0f;
	public const float ShiftSeconds = 3f;
	public const float FullScore = 300;
	public const float NoGuessPenalty = -50;

	public const float BeginningSeconds = 15;
	public const float PointsToTimeConversion = ShiftSeconds / FullScore;

	public static readonly Color SessionHealthPercentageColor = new Color(1f, 0f, 0f, 50f/255);

	private GUIStyle BackStyle;
	private GUIStyle OptionStyle;
	private GUIStyle CorrectOptionStyle;
	private GUIStyle WrongOptionStyle;
	private GUIStyle ObsoleteOptionStyle;
	private GUIStyle SessionScoreStyle;
	private GUIStyle SessionScoreLabelStyle;

	private Rect BackRect;
	private Rect SessionScoreRect;
	private Rect SessionAverageRect;
	private Rect PhaseScoreImpactRect;
	private Rect SessionScoreLabelRect;
	private Rect SessionAverageLabelRect;
	private Rect PhaseScoreImpactLabelRect;

	private WordOptions.Difficulty difficulty;
	private string currentWord;
	private List<string> currentOptions;
	private float wordStartTime = -ShiftSeconds;
	private List<Letteral> letterals;
	private string guessedOption;

	private float sessionLowerBoundsTime;

	private Scores score;

	private bool active;

	public GameScreen(WordOptions.Difficulty difficulty){

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Colors.ClickableText;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		OptionStyle = new GUIStyle();
		OptionStyle.fontSize = Main.FontLargest;
		OptionStyle.normal.textColor = Colors.ClickableText;
		OptionStyle.alignment = TextAnchor.MiddleCenter;

		SessionScoreStyle = new GUIStyle();
		SessionScoreStyle.fontSize = Main.FontLarge;
		SessionScoreStyle.normal.textColor = Colors.ReadableText;
		SessionScoreStyle.alignment = TextAnchor.MiddleRight;

		SessionScoreLabelStyle = new GUIStyle();
		SessionScoreLabelStyle.fontSize = Main.FontLarge;
		SessionScoreLabelStyle.normal.textColor = Colors.ReadableText;
		SessionScoreLabelStyle.alignment = TextAnchor.MiddleLeft;

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));

		this.difficulty = difficulty;

		score = new Scores(this.difficulty);

		active = true;

		resetSession();
		resetWord();
		sessionLowerBoundsTime = Time.time;
	}

	public override void OnGUI(){

		if(active) {
			float sessionHealthPercentage = (BeginningSeconds - (Time.time - sessionLowerBoundsTime)) / BeginningSeconds;

			Rect sessionHealthPercentageRect = new Rect(0f, Main.NativeHeight * (sessionHealthPercentage), Main.NativeWidth, Main.NativeHeight * (1 - sessionHealthPercentage));
			Utils.FillRectangle(sessionHealthPercentageRect, SessionHealthPercentageColor);

			for(int i=0; i<currentOptions.Count; i++){
				Rect rect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * (i + 2)) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));

				if(Main.Clicked && rect.Contains(Main.TouchGuiLocation)) {
					guessedOption = currentOptions[i];
					float scoreImpact = Mathf.Round(FullScore * (Mathf.Max(0, ShiftSeconds - (Time.time - wordStartTime)) / ShiftSeconds));
					scoreImpact = guessedOption == currentWord? scoreImpact : -scoreImpact;
					score.ScorePoints(scoreImpact);
					sessionLowerBoundsTime += PointsToTimeConversion * scoreImpact;
					sessionLowerBoundsTime = Mathf.Min(sessionLowerBoundsTime, Time.time + (ShiftSeconds / 2));
					resetWord();
				}

				// Utils.DrawRectangle(rect, 50, Colors.ButtonOutline);
				Utils.FillRectangle(rect, Colors.ButtonBackground);
				GUI.Label(rect, currentOptions[i], OptionStyle);
			}

			foreach(Letteral letteral in letterals){
				letteral.Update();
			}
		}

		if(Time.time - sessionLowerBoundsTime > BeginningSeconds && active){
			active = false;
			score.CheckForLifetimeRecords();
			Main.SetGui(new Instructions(difficulty, score.LastScoreImpact, score.SessionScore, score.SessionAverage));
		}

		GUI.Label(SessionScoreLabelRect, "total", SessionScoreLabelStyle);
		GUI.Label(SessionAverageLabelRect, "average", SessionScoreLabelStyle);
		GUI.Label(PhaseScoreImpactLabelRect, "last word", SessionScoreLabelStyle);

		GUI.Label(SessionScoreRect, score.SessionScore.ToString("0"), SessionScoreStyle);
		GUI.Label(SessionAverageRect, score.SessionAverage.ToString("0.0"), SessionScoreStyle);
		GUI.Label(PhaseScoreImpactRect, score.LastScoreImpact.ToString("0"), SessionScoreStyle);

		// Utils.DrawRectangle(BackRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(BackRect, Colors.ButtonBackground);
		GUI.Label(BackRect, "BACK", BackStyle);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new Instructions(difficulty, score.LastScoreImpact, score.SessionScore, score.SessionAverage));
		}

	}

	private void resetSession(){
		score.ResetSession();
	}

	private void resetWord(){
		wordStartTime = Time.time;
		currentOptions = WordOptions.GetStrings(this.difficulty);
		currentWord = currentOptions[(int) (currentOptions.Count * Random.value)];
		letterals = LetteralGenerator.GenerateLetterals(currentWord);
	}

}