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
    private TurnBasedInput _input;

    public void Initialize(int buttonPosition, TurnBasedInput turnBasedInput)
    {
        button.onClick.AddListener(OnButtonClicked);
        position = buttonPosition;
        _startingPosition = statTextPosition.anchoredPosition;
        _outPosition = outPosition.anchoredPosition;
        _input = turnBasedInput;
        Debug.Log("Bind initialized with position: "+ position);
    }

    private void OnButtonClicked()
    {
        _input.OnUiButton(position);
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