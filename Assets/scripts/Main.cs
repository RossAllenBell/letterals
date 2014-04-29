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
    public static Vector2 GuiCenter;
    public static float VisibleBoardWidth;

    private const int NormalLargestFont = 300;
	public static int FontLargest;
	public static int FontLarge;
	public static int FontMedium;

	public static GUIStyle LetteralStyle;
	public static GUIStyle TitleLetteralStyle;
	
	public static bool Clicked { get { return click; } }
	public static Vector2 TouchLocation { get { return touchLocation; } }
	public static Vector2 TouchGuiLocation { get { return TouchLocationToGuiLocation(touchLocation); } }
    public static bool Touching { get { return touching; } }

    private static bool click;
    private static Vector2 touchLocation;
    private static bool touching;

	public static List<string> AllWords;
	public static Dictionary<int,List<string>> WordsBySize;
	public static List<List<string>> AnagramLists;

	private static Gui fadeTo;
	private static float guiSwitchTime;
	private static Gui currentGui;

	public void Start () {
		Camera.main.backgroundColor = Colors.Background;

		TextAsset wordsText = (TextAsset) Resources.Load("words");

		AllWords = new List<string>();
		WordsBySize = new Dictionary<int,List<string>>();
		AnagramLists = new List<List<string>>();

		List<string> currentAnagrams =  new List<string>();

		foreach(string word in wordsText.text.Split('\n')) {
			if(word.Length == 0) {
				if(currentAnagrams.Count > 2) {
					AnagramLists.Add(currentAnagrams);
				}
				currentAnagrams = new List<string>();
			} else {
				currentAnagrams.Add(word.ToUpper());
				if(!WordsBySize.ContainsKey(word.Length)){
					WordsBySize.Add(word.Length, new List<string>());
				}
				WordsBySize[word.Length].Add(word.ToUpper());
				AllWords.Add(word.ToUpper());
			}
		}

		Screen.orientation = ScreenOrientation.Portrait;
        
        NativeWidth = Screen.width;
        NativeHeight = Screen.height;
        GuiCenter = new Vector2(Main.NativeWidth / 2f, Main.NativeHeight / 2f);
        
        GUIStyle testStyle = new GUIStyle();
        testStyle.fontSize = NormalLargestFont;
        GuiRatio = NativeWidth / testStyle.CalcSize(new GUIContent(AllWords[AllWords.Count - 1] + "WWW")).x;

		FontLargest = (int) (NormalLargestFont * GuiRatio);
		FontLarge = (int) (NormalLargestFont * 0.75 * GuiRatio);
		FontMedium = (int) (NormalLargestFont * 0.50 * GuiRatio);

		LetteralStyle = new GUIStyle();
		LetteralStyle.fontSize = Main.FontLargest;
		LetteralStyle.normal.textColor = Colors.ReadableText;
		LetteralStyle.alignment = TextAnchor.UpperCenter;

		TitleLetteralStyle = new GUIStyle();
		TitleLetteralStyle.fontSize = Main.FontLargest * 2;
		TitleLetteralStyle.normal.textColor = Colors.ReadableText;
		TitleLetteralStyle.alignment = TextAnchor.MiddleCenter;
		
		SetGui(new Intro());
		
	}
	
	public void Update () {

    }

    public void OnGUI(){

        if (Input.touchCount > 0 || Input.GetMouseButton (0)) {
            Vector2 tempLocation = Input.touchCount > 0 ? Input.GetTouch (0).position : (Vector2)Input.mousePosition;
            touchLocation = new Vector2 (tempLocation.x, tempLocation.y);
            click = !touching;
            touching = true;
        } else {
            click = false;
            touching = false;
        }

        if (fadeTo == null) {
        	currentGui.OnGUI();
    	} else {
    		if (Time.time - guiSwitchTime >= Gui.FadeOut) {
    			GUI.color = new Color(1f, 1f, 1f, (Time.time - (guiSwitchTime + Gui.FadeOut)) / Gui.FadeIn);
    			fadeTo.OnGUI();
    			if (Time.time - (guiSwitchTime + Gui.FadeOut) > Gui.FadeIn){
    				currentGui = fadeTo;
    				fadeTo = null;
    			}
			} else {
				GUI.color = new Color(1f, 1f, 1f, 1f - ((Time.time - guiSwitchTime) / Gui.FadeOut));
				currentGui.OnGUI();
			}
    	}

	}

	public static void SetGui(Gui gui) {
		guiSwitchTime = Time.time;
		fadeTo = gui;
		if(currentGui == null){
			guiSwitchTime -= Gui.FadeOut;
		}
	}

	public static Vector2 TouchLocationToGuiLocation (Vector2 touchLocation)
	{
		return new Vector2 (touchLocation.x, NativeHeight - touchLocation.y);
	}

}
