using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform rectTransform;
    Rect previousArea;
    Vector2Int previousSize;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Apply();
    }

    void Update()
    {
        if (Screen.safeArea != previousArea ||
            Screen.width != previousSize.x ||
            Screen.height != previousSize.y)
        {
            Apply();
        }
    }

    void Apply()
    {
        if (Screen.width <= 0 || Screen.height <= 0)
            return;

        Rect area = Screen.safeArea;
        Vector2 size = new Vector2(Screen.width, Screen.height);

        rectTransform.anchorMin = area.position / size;
        rectTransform.anchorMax = (area.position + area.size) / size;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        previousArea = area;
        previousSize = new Vector2Int(Screen.width, Screen.height);
    }
}