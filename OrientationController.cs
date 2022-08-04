using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public sealed class OrientationController : MonoBehaviour
{
    public SavedRect verticalRect = new SavedRect();
    public SavedRect horizontalRect = new SavedRect();

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        OrientationChanged += OnOrientationChanged;
        OnOrientationChanged(this, isVertical);
    }

    public void SaveCurrentState()
    {
        if (isVertical)
            verticalRect.SaveDataFromRectTransform(_rect);
        else
            horizontalRect.SaveDataFromRectTransform(_rect);
    }

    public void PutCurrentState()
    {
        OnOrientationChanged(this, isVertical);
    }

    private void OnOrientationChanged(object sender, bool isVertical)
    {
        if (isVertical)
            verticalRect.PutDataToRectTransform(_rect);
        else
            horizontalRect.PutDataToRectTransform(_rect);
    }

    private void OnDestroy()
    {
        OrientationChanged -= OnOrientationChanged;
    }


    // Static
    public static bool isVertical;
    private static event EventHandler<bool> OrientationChanged;
    public static void FireOrientationChanged(object s, bool isVertical) => OrientationChanged?.Invoke(s, isVertical);

    static OrientationController()
    {
        OrientationChanged += (s, e) => isVertical = e;
    }
}

[Serializable]
public class SavedRect
{
    public bool isInitialized = false;

    public Vector3 anchoredPosition;
    public Vector2 sizeDelta;
    public Vector2 minAnchor;
    public Vector2 maxAnchor;
    public Vector2 pivot;
    public Vector3 rotation;
    public Vector3 scale;

    /// <summary>
    /// Сохраняет данные из RectTransform в этот объект.
    /// </summary>
    /// <param name="rect"></param>
    public void SaveDataFromRectTransform(RectTransform rect)
    {
        if (rect == null)
            return;

        isInitialized = true;

        anchoredPosition = rect.anchoredPosition3D;
        sizeDelta = rect.sizeDelta;
        minAnchor = rect.anchorMin;
        maxAnchor = rect.anchorMax;
        pivot = rect.pivot;
        rotation = rect.localEulerAngles;
        scale = rect.localScale;
    }

    /// <summary>
    /// Выгружает данные из этого объекта в RectTransform
    /// </summary>
    /// <param name="rect"></param>
    public void PutDataToRectTransform(RectTransform rect)
    {
        if (rect == null || !isInitialized)
            return;

        rect.anchoredPosition3D = anchoredPosition;
        rect.sizeDelta = sizeDelta;
        rect.anchorMin = minAnchor;
        rect.anchorMax = maxAnchor;
        rect.pivot = pivot;
        rect.localEulerAngles = rotation;
        rect.localScale = scale;
    }
}
