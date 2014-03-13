using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public const int NormalWidth = 1080;
    public const int NormalHeight = 1920;
    public static float GuiRatio;
    public static float GuiRatioWidth;
    public static float GuiRatioHeight;
    public static int NativeWidth;
    public static int NativeHeight;
    public static float VisibleBoardWidth;
    public const float BasicallyZero = 0.0001f;

	public static int FontLargest = 300;
	public static int FontLarge = (int) (FontLargest * 0.75f);

	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);

	public const float PreviewSeconds = 0f;
	public const float ShiftSeconds = 3f;
	public const float FullScore = 300;
	public static GUIStyle OptionStyle;
	public static GUIStyle LetteralStyle;
	public static GUIStyle MenuDifficultyStyle;
	public static GUIStyle TitleStyle;
	public static GUIStyle BackStyle;
	public static GUIStyle NextWordStyle;
	public static GUIStyle SessionScoreStyle;

	public static Rect BackRect;
	public static Rect NextWordRect;
	public static Rect SessionScoreRect;

	public enum Difficulty {None, Easy, Medium, Hard};
	public static Difficulty CurrentDifficulty = Difficulty.None;

	public static bool Clicked { get { return click; } }

	public static Vector2 TouchLocation { get { return touchLocation; } }
	public static Vector2 TouchGuiLocation { get { return TouchLocationToGuiLocation(touchLocation); } }
    public static bool Touching { get { return touching; } }

    static bool click;
    static Vector2 touchLocation;
    static bool touching;

	private List<string> allWords;
	private Dictionary<int,List<string>> wordsBySize;
	private List<List<string>> anagramLists;

	void Start () {
		TextAsset wordsText = (TextAsset) Resources.Load("words");

		allWords = new List<string>();
		wordsBySize = new Dictionary<int,List<string>>();
		anagramLists = new List<List<string>>();

		List<string> currentAnagrams =  new List<string>();

		foreach(string word in wordsText.text.Split('\n')) {
			if(word.Length == 0) {
				if(currentAnagrams.Count > 2) {
					anagramLists.Add(currentAnagrams);
				}
				currentAnagrams = new List<string>();
			} else {
				currentAnagrams.Add(word.ToUpper());
				if(!wordsBySize.ContainsKey(word.Length)){
					wordsBySize.Add(word.Length, new List<string>());
				}
				wordsBySize[word.Length].Add(word.ToUpper());
				allWords.Add(word.ToUpper());
			}
		}

		Screen.orientation = ScreenOrientation.Portrait;
        
        NativeWidth = Screen.width;
        NativeHeight = Screen.height;
        
        GUIStyle testStyle = new GUIStyle();
        testStyle.fontSize = FontLargest;
        GuiRatio = NativeWidth / testStyle.CalcSize(new GUIContent(allWords[allWords.Count - 1] + "WWW")).x;

		FontLargest = (int) (FontLargest * GuiRatio);
		FontLarge = (int) (FontLarge * GuiRatio);

		Camera.main.orthographicSize = 5;

		OptionStyle = new GUIStyle();
		OptionStyle.fontSize = Main.FontLargest;
		OptionStyle.normal.textColor = Color.black;
		OptionStyle.alignment = TextAnchor.MiddleCenter;

		LetteralStyle = new GUIStyle();
		LetteralStyle.fontSize = Main.FontLargest;
		LetteralStyle.normal.textColor = Color.yellow;
		LetteralStyle.alignment = TextAnchor.UpperCenter;

		MenuDifficultyStyle = new GUIStyle();
		MenuDifficultyStyle.fontSize = Main.FontLargest;
		MenuDifficultyStyle.normal.textColor = NavyBlue;
		MenuDifficultyStyle.alignment = TextAnchor.MiddleCenter;

		TitleStyle = new GUIStyle();
		TitleStyle.fontSize = Main.FontLargest * 2;
		TitleStyle.normal.textColor = Color.black;
		TitleStyle.alignment = TextAnchor.MiddleCenter;

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Color.black;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		NextWordStyle = new GUIStyle();
		NextWordStyle.fontSize = Main.FontLarge;
		NextWordStyle.normal.textColor = Color.black;
		NextWordStyle.alignment = TextAnchor.MiddleCenter;

		SessionScoreStyle = new GUIStyle();
		SessionScoreStyle.fontSize = Main.FontLarge;
		SessionScoreStyle.normal.textColor = Color.black;
		SessionScoreStyle.alignment = TextAnchor.MiddleRight;

		BackRect = new Rect(NativeWidth * 0.05f, NativeHeight - (((NativeHeight / 12f) - (NativeWidth * 0.05f)) + (NativeWidth * 0.05f)), (NativeWidth / 3) - (NativeWidth * 0.1f), (NativeHeight / 12f) - (NativeWidth * 0.05f));
		NextWordRect = new Rect(NativeWidth * 0.05f, NativeWidth * 0.05f, NativeWidth - (NativeWidth * 0.1f), (NativeHeight / 12f) - (NativeWidth * 0.05f));
		SessionScoreRect = new Rect(NativeWidth * 0.05f, NativeHeight - (((NativeHeight / 12f) - (NativeWidth * 0.05f)) + (NativeWidth * 0.05f)), NativeWidth - (NativeWidth * 0.1f), (NativeHeight / 12f) - (NativeWidth * 0.05f));
        
	}
	
	void Update () {

    }

    void OnGUI(){

        if (Input.touchCount > 0 || Input.GetMouseButton (0)) {
            Vector2 tempLocation = Input.touchCount > 0 ? Input.GetTouch (0).position : (Vector2)Input.mousePosition;
            touchLocation = new Vector2 (tempLocation.x, tempLocation.y);
            click = !touching;
            touching = true;
        } else {
            click = false;
            touching = false;
        }

    	if (CurrentDifficulty == Difficulty.None) {
    		MenuScreen();
    	} else {
	        GameUpdate();
	    }

	}

	public static Vector2 TouchLocationToGuiLocation (Vector2 touchLocation)
	{
		return new Vector2 (touchLocation.x, NativeHeight - touchLocation.y);
	}

	private void MenuScreen(){

		GUI.Label(BackRect, "EXIT", BackStyle);
		Utils.DrawRectangle(BackRect, 50, Color.black);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Application.Quit();
		}

		Rect titleRect = new Rect(0, 0, NativeWidth, (NativeHeight / 6) * 2);

		Rect easyRect = new Rect(0 + (NativeWidth * 0.05f), ((NativeHeight / 6f) * 2) + (NativeWidth * 0.025f), NativeWidth - (NativeWidth * 0.1f), (NativeHeight / 6f) - (NativeWidth * 0.05f));
		Rect mediumRect = new Rect(0 + (NativeWidth * 0.05f), ((NativeHeight / 6f) * 3) + (NativeWidth * 0.025f), NativeWidth - (NativeWidth * 0.1f), (NativeHeight / 6f) - (NativeWidth * 0.05f));
		Rect hardRect = new Rect(0 + (NativeWidth * 0.05f), ((NativeHeight / 6f) * 4) + (NativeWidth * 0.025f), NativeWidth - (NativeWidth * 0.1f), (NativeHeight / 6f) - (NativeWidth * 0.05f));

		GUI.Label(titleRect, "LETTERALS", TitleStyle);

		GUI.Label(easyRect, "EASY", MenuDifficultyStyle);
		Utils.DrawRectangle(easyRect, 50, Color.black);
		if(Main.Clicked && easyRect.Contains(Main.TouchGuiLocation)){
			CurrentDifficulty = Difficulty.Easy;
		}

		GUI.Label(mediumRect, "MEDIUM", MenuDifficultyStyle);
		Utils.DrawRectangle(mediumRect, 50, Color.black);
		if(Main.Clicked && mediumRect.Contains(Main.TouchGuiLocation)){
			CurrentDifficulty = Difficulty.Medium;
		}

		GUI.Label(hardRect, "HARD", MenuDifficultyStyle);
		Utils.DrawRectangle(hardRect, 50, Color.black);
		if(Main.Clicked && hardRect.Contains(Main.TouchGuiLocation)){
			CurrentDifficulty = Difficulty.Hard;
		}
	}

	private string currentWord;
	private List<string> currentOptions;
	private float startTime = -ShiftSeconds;
	private List<Letteral> letterals;
	private long sessionScore;
	private bool guessed;
	
	private void GameUpdate() {
		
		if (guessed || startTime < Time.time - ShiftSeconds) {
			GUI.Label(NextWordRect, "next word...", NextWordStyle);
			Utils.DrawRectangle(NextWordRect, 50, Color.black);

			if(!guessed){
				guessed = true;
				sessionScore -= 10;
			}

			if(Main.Clicked && NextWordRect.Contains(Main.TouchGuiLocation)) {
				if(currentWord == null){
					sessionScore = 0;
				}

				guessed = false;
				startTime = Time.time;
				switch (CurrentDifficulty){
					case Difficulty.Easy: currentOptions = EasyStrings(); break;
					case Difficulty.Medium: currentOptions = MediumStrings(); break;
					case Difficulty.Hard: currentOptions = HardStrings(); break;
				}
				currentWord = currentOptions[(int) (currentOptions.Count * Random.value)];
				letterals = LetteralGenerator.GenerateLetterals(currentWord);
			}
		}

		if (currentWord != null) {

			for(int i=0; i<currentOptions.Count; i++){
				Rect rect = new Rect(0 + (NativeWidth * 0.05f), ((NativeHeight / 6f) * (i + 2)) + (NativeWidth * 0.025f), NativeWidth - (NativeWidth * 0.1f), (NativeHeight / 6f) - (NativeWidth * 0.05f));

				GUI.Label(rect, currentOptions[i], OptionStyle);
				Utils.DrawRectangle(rect, 50, Color.black);
				if(!guessed && Main.Clicked && rect.Contains(Main.TouchGuiLocation)) {
					guessed = true;
					long scoreImpact = (long) Mathf.Round(FullScore * (Mathf.Max(0, ShiftSeconds - (Time.time - startTime)) / ShiftSeconds));
					sessionScore += currentOptions[i] == currentWord? scoreImpact : -scoreImpact;
				}
			}

			foreach(Letteral letteral in letterals){
				letteral.Update();
			}

		}

		GUI.Label(SessionScoreRect, sessionScore.ToString(), SessionScoreStyle);

		GUI.Label(BackRect, "BACK", BackStyle);
		Utils.DrawRectangle(BackRect, 50, Color.black);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			currentWord = null;
			startTime = -ShiftSeconds;
			sessionScore = 0;

			CurrentDifficulty = Difficulty.None;
		}

	}

	private List<string> EasyStrings() {
		List<string> strings = new List<string>();
		while(strings.Count < 3){
			string word = allWords[(int) (allWords.Count * Random.value)];
			if(!strings.Contains(word)){
				strings.Add(word);
			}
		}
		return strings;
	}

	private List<string> MediumStrings() {
		List<string> strings = new List<string>();
		string baseWord = allWords[(int) (allWords.Count * Random.value)];
		strings.Add(baseWord);
		while(strings.Count < 3){
			string word = wordsBySize[baseWord.Length][(int) (wordsBySize[baseWord.Length].Count * Random.value)];
			if(!strings.Contains(word)){
				strings.Add(word);
			}
		}
		return strings;
	}

	private List<string> HardStrings() {
		List<string> baseAnagrams = anagramLists[(int) (anagramLists.Count * Random.value)];
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
