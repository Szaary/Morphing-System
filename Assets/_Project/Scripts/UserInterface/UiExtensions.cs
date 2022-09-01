using DG.Tweening;
using UnityEngine;

/// <summary>
/// position to change, starting position, out position (already setup for gameMode)
/// </summary>
public static class UiExtensions
{
    public static void ChangePosition(RectTransform selectedTransform, Vector2 outPosition)
    {
        selectedTransform.DOAnchorPos(outPosition, 1.5f, false).SetEase(Ease.InCirc).SetUpdate(true);
    }
    
   
}