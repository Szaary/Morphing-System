using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BindRepresentation
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI bindText;

    [SerializeField] private RectTransform statTextPosition;
    [SerializeField] private RectTransform outPosition;

    public int position;
    private Vector2 _startingPosition;
    private Vector2 _outPosition;

    public void Initialize(int buttonPosition)
    {
        button.onClick.AddListener(OnButtonClicked);
        this.position = buttonPosition;
        _startingPosition = statTextPosition.anchoredPosition;
        _outPosition = outPosition.anchoredPosition;
    }

    private void OnButtonClicked()
    {
        Debug.Log("Animate button");
    }

    public void PressButton(Result result)
    {
        button.onClick.Invoke();
    }

    public void ShowButton(Active active)
    {
        button.interactable = true;
        buttonText.text = active.name;
    }

    public void ShowButton(CharacterFacade possibleTarget)
    {
        button.interactable = true;
        buttonText.text = possibleTarget.name;
    }

    public void HideButton()
    {
        button.interactable = false;
        buttonText.text = "";
    }


    public void ChangePositions(GameMode gameMode)
    {
        if (gameMode == GameMode.TurnBasedFight)
        {
            UiExtensions.ChangePosition(statTextPosition, _startingPosition);
        }
        else
        {
            UiExtensions.ChangePosition(statTextPosition, _outPosition);
        }
    }
}