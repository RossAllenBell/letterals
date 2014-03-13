using UnityEngine;

public class Utils {
    
    public static void DrawOutline(Rect position, string text, GUIStyle style) {
            DrawOutline(position, text, style, 2);
    }
    
    public static void DrawOutline(Rect position, string text, GUIStyle style, int offset) {
            DrawOutline(position, text, style, offset, style.normal.textColor);
    }
            
    public static void DrawOutline(Rect position, string text, GUIStyle style, int offset, Color color) {
            DrawOutline(position, text, style, offset, color, InvertColor(color));
    }
    
    public static void DrawOutline(Rect position, string text, GUIStyle style, int offset, Color color, Color outColor){
        GUIStyle backupStyle = style;
        style.normal.textColor = outColor;
        position.x -= offset;
        GUI.Label(position, text, style);
        position.x += offset * 2;
        GUI.Label(position, text, style);
        position.x -= offset;
        position.y -= offset;
        GUI.Label(position, text, style);
        position.y += offset * 2;
        GUI.Label(position, text, style);
        position.y -= offset;
        style.normal.textColor = color;
        GUI.Label(position, text, style);
        style = backupStyle;
    }
    
    public static Color InvertColor (Color color) {
        return new Color (1.0f-color.r, 1.0f-color.g, 1.0f-color.b);
    }

    public static void DrawRectangle(Rect rect, int normalThickness, Color color) {
        float guiScaledThickness = Mathf.Max(1, normalThickness * Main.GuiRatio);

        Rect rectN = new Rect(rect.x, rect.y, rect.width, guiScaledThickness);
        Rect rectE = new Rect((rect.x + rect.width) - guiScaledThickness, rect.y, guiScaledThickness, rect.height);
        Rect rectS = new Rect(rect.x, (rect.y + rect.height) - guiScaledThickness, rect.width, guiScaledThickness);
        Rect rectW = new Rect(rect.x, rect.y, guiScaledThickness, rect.height);

        GUIStyle style = new GUIStyle();
        Texture2D texture = new Texture2D(1,1);
        texture.SetPixel(0,0,color);
        texture.Apply();
        style.normal.background = texture;

        GUI.Box(rectN, GUIContent.none, style);
        GUI.Box(rectE, GUIContent.none, style);
        GUI.Box(rectS, GUIContent.none, style);
        GUI.Box(rectW, GUIContent.none, style);
    }
        
}