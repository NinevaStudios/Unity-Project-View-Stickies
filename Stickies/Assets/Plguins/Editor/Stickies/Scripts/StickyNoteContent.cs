using UnityEngine;
using UnityEditor;

public class StickyNoteContent : PopupWindowContent
{
    bool toggle1 = true;
    bool toggle2 = true;
    bool toggle3 = true;

    Color m_ColorDark;
    Color m_ColorGray;

    const float k_Height = 32f;

    string _guid;

    Vector3[] m_RectVertices = new Vector3[4];

    GUIStyle m_MiddleCenterStyle;

    public StickyNoteContent(string guid)
    {
        _guid = guid;

        if (EditorGUIUtility.isProSkin)
        {
            m_ColorDark = new Color(0.18f, 0.18f, 0.18f);
            m_ColorGray = new Color(0.43f, 0.43f, 0.43f);
        }
        else
        {
            m_ColorDark = new Color(0.64f, 0.64f, 0.64f);
            m_ColorGray = new Color(0.92f, 0.92f, 0.92f);
        }

        m_MiddleCenterStyle = new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleCenter };
    }

    public override Vector2 GetWindowSize()
    {
        return new Vector2(200, 150);
    }

    public override void OnGUI(Rect rect)
    {
        Handles.DrawSolidRectangleWithOutline(rect, Color.yellow, Color.yellow);
        if (DrawUtils.DrawColorChooser(new Rect(15, 15, 32, 32), Colors.YellowHeader, Colors.YellowBg))
        {
            Debug.Log("Color click");
        }
        editorWindow.Repaint();
    }

//    public void DrawShutterGraph(float angle)
//    {
//        var center = GUILayoutUtility.GetRect(128, k_Height).center;
//
//        // Parameters used to make transitions smooth.
//        var zeroWhenOff = Mathf.Min(1f, angle * 0.1f);
//        var zeroWhenFull = Mathf.Min(1f, (360f - angle) * 0.02f);
//
//        // Shutter angle graph
//        var discCenter = center - new Vector2(k_Height * 2.4f, 0f);
//        // - exposure duration indicator
//        DrawDisc(discCenter, k_Height * Mathf.Lerp(0.5f, 0.38f, zeroWhenFull), m_ColorGray);
//        // - shutter disc
////        DrawDisc(discCenter, k_Height * 0.16f * zeroWhenFull, m_ColorDark);
//        // - shutter blade
////        DrawArc(discCenter, k_Height * 0.5f, 360f - angle, m_ColorDark);
//        // - shutter axis
////        DrawDisc(discCenter, zeroWhenOff, m_ColorGray);
//    }


    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
}