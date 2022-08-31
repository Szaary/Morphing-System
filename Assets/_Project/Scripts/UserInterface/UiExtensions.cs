using DG.Tweening;
using UnityEngine;

/// <summary>
/// position to change, starting position, out position (already setup for gameMode)
/// </summary>
public static class UiExtensions
{
    public static void ChangeFpsPosition(RectTransform selectedTransform, Vector2 startPosition, Vector2 outPosition, GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            selectedTransform.DOAnchorPos(outPosition, 2, false).SetEase(Ease.InCirc).SetUpdate(true);

        }
        else if (newMode == GameMode.Fps)
        {
            selectedTransform.DOAnchorPos(startPosition, 2, false).SetEase(Ease.InCirc).SetUpdate(true);
        }
    }
    
    public static void ChangeTurnBasedPosition(RectTransform selectedTransform, Vector2 startPosition, Vector2 outPosition, GameMode newMode)
    {
        if (newMode == GameMode.Fps)
        {
            selectedTransform.DOAnchorPos(outPosition, 2, false).SetEase(Ease.InCirc).SetUpdate(true);
            
        }
        else if (newMode == GameMode.TurnBasedFight)
        {
            selectedTransform.DOAnchorPos(startPosition, 2, false).SetEase(Ease.InCirc).SetUpdate(true);
        }
    }
}