using UnityEngine;
using System.Collections.Generic;

public class GameScreen : Gui {

	public const float PreviewSeconds = 0f;
	public const float ShiftSeconds = 3f;
	public const float FullScore = 300;
	public const float NoGuessPenalty = -50;

	public static readonly Color PhaseProgressBarColor = new Color(150f/255, 150f/255, 150f/255, 150f/255);
	public static readonly Color LabelColor = new Color(150/255, 150/255, 150/255, 200f/255);
	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);

	private GUIStyle BackStyle;
	private GUIStyle OptionStyle;
	private GUIStyle CorrectOptionStyle;
	private GUIStyle WrongOptionStyle;
	private GUIStyle ObsoleteOptionStyle;
	private GUIStyle NextWordStyle;
	private GUIStyle SessionScoreStyle;
	private GUIStyle SessionScoreLabelStyle;

	private Rect BackRect;
	private Rect NextWordRect;
	private Rect SessionScoreRect;
	private Rect SessionAverageRect;
	private Rect PhaseScoreImpactRect;
	private Rect SessionScoreLabelRect;
	private Rect SessionAverageLabelRect;
	private Rect PhaseScoreImpactLabelRect;

	private WordOptions.Difficulty difficulty;
	private string currentWord;
	private List<string> currentOptions;
	private float startTime = -ShiftSeconds;
	private List<Letteral> letterals;
	private float sessionScore;
	private float sessionAverage;
	private int sessionCount;
	private bool guessed;
	private string guessedOption;
	private float lastScoreImpact;

	public GameScreen(WordOptions.Difficulty difficulty){

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = NavyBlue;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		CorrectOptionStyle = new GUIStyle();
		CorrectOptionStyle.fontSize = Main.FontLargest;
		CorrectOptionStyle.normal.textColor = Color.black;
		CorrectOptionStyle.alignment = TextAnchor.MiddleCenter;

		OptionStyle = new GUIStyle();
		OptionStyle.fontSize = Main.FontLargest;
		OptionStyle.normal.textColor = NavyBlue;
		OptionStyle.alignment = TextAnchor.MiddleCenter;

		WrongOptionStyle = new GUIStyle();
		WrongOptionStyle.fontSize = Main.FontLargest;
		WrongOptionStyle.normal.textColor = Color.red;
		WrongOptionStyle.alignment = TextAnchor.MiddleCenter;

		ObsoleteOptionStyle = new GUIStyle();
		ObsoleteOptionStyle.fontSize = Main.FontLargest;
		ObsoleteOptionStyle.normal.textColor = PhaseProgressBarColor;
		ObsoleteOptionStyle.alignment = TextAnchor.MiddleCenter;

		NextWordStyle = new GUIStyle();
		NextWordStyle.fontSize = Main.FontLarge;
		NextWordStyle.normal.textColor = NavyBlue;
		NextWordStyle.alignment = TextAnchor.MiddleCenter;

		SessionScoreStyle = new GUIStyle();
		SessionScoreStyle.fontSize = Main.FontLarge;
		SessionScoreStyle.normal.textColor = Color.black;
		SessionScoreStyle.alignment = TextAnchor.MiddleRight;

		SessionScoreLabelStyle = new GUIStyle();
		SessionScoreLabelStyle.fontSize = Main.FontLarge;
		SessionScoreLabelStyle.normal.textColor = LabelColor;
		SessionScoreLabelStyle.alignment = TextAnchor.MiddleLeft;

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		NextWordRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f));
		SessionScoreRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));

		this.difficulty = difficulty;

		sessionScore = 0f;
		sessionAverage = 0f;
		sessionCount = 0;
		guessed = false;
		lastScoreImpact = 0;
	}

	public override void OnGUI(){

		if (guessed || startTime < Time.time - ShiftSeconds) {
			GUI.Label(NextWordRect, "next word...", NextWordStyle);
			Utils.DrawRectangle(NextWordRect, 50, Color.black);

			if(!guessed && currentWord != null){
				guessed = true;
				lastScoreImpact = NoGuessPenalty;
				changeSessionScoreBy(lastScoreImpact);
			}

			if(Main.Clicked && NextWordRect.Contains(Main.TouchGuiLocation)) {
				lastScoreImpact = 0;
				guessed = false;
				startTime = Time.time;
				currentOptions = WordOptions.GetStrings(this.difficulty);
				currentWord = currentOptions[(int) (currentOptions.Count * Random.value)];
				letterals = LetteralGenerator.GenerateLetterals(currentWord);
			}
		}

		if (currentWord != null) {

			float phaseProgress = (Time.time - startTime) / ShiftSeconds;
			if(phaseProgress < 1f){
				Rect phaseProgressBarRect = new Rect(0f,0f,Main.NativeWidth * phaseProgress,Main.NativeWidth*0.05f);
				Utils.FillRectangle(phaseProgressBarRect, PhaseProgressBarColor);
			}

			for(int i=0; i<currentOptions.Count; i++){
				Rect rect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * (i + 2)) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));

								if(!guessed && Main.Clicked && rect.Contains(Main.TouchGuiLocation)) {
					guessed = true;
					guessedOption = currentOptions[i];
					lastScoreImpact = Mathf.Round(FullScore * (Mathf.Max(0, ShiftSeconds - (Time.time - startTime)) / ShiftSeconds));
					lastScoreImpact = guessedOption == currentWord? lastScoreImpact : -lastScoreImpact;
					changeSessionScoreBy(lastScoreImpact);
				}

				if(guessed) {
					if(currentOptions[i] == currentWord){
						GUI.Label(rect, currentOptions[i], CorrectOptionStyle);
						Utils.DrawRectangle(rect, 50, Color.black);
					} else if (currentOptions[i] == guessedOption) {
						GUI.Label(rect, currentOptions[i], WrongOptionStyle);
						Utils.DrawRectangle(rect, 50, Color.red);
					} else {
						GUI.Label(rect, currentOptions[i], ObsoleteOptionStyle);
						Utils.DrawRectangle(rect, 50, PhaseProgressBarColor);
					}
				} else {
					GUI.Label(rect, currentOptions[i], OptionStyle);
					Utils.DrawRectangle(rect, 50, Color.black);
				}
			}

			foreach(Letteral letteral in letterals){
				letteral.Update();
			}

		}

		GUI.Label(SessionScoreLabelRect, "total", SessionScoreLabelStyle);
		GUI.Label(SessionAverageLabelRect, "average", SessionScoreLabelStyle);
		GUI.Label(PhaseScoreImpactLabelRect, "this word", SessionScoreLabelStyle);

		GUI.Label(SessionScoreRect, sessionScore.ToString("0"), SessionScoreStyle);
		GUI.Label(SessionAverageRect, sessionAverage.ToString("0.0"), SessionScoreStyle);
		if(lastScoreImpact != 0) GUI.Label(PhaseScoreImpactRect, lastScoreImpact.ToString("0"), SessionScoreStyle);

		GUI.Label(BackRect, "BACK", BackStyle);
		Utils.DrawRectangle(BackRect, 50, Color.black);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}

	}

	private void changeSessionScoreBy(float change){
		sessionScore += change;
		sessionAverage = ((sessionAverage * sessionCount) + (change)) / (sessionCount + 1f);
		sessionCount++;
	}

}