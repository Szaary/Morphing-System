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
    
    private Vector2 _startingPosition;
    private Vector2 _outPosition;
    
    public void Initialize()
    {
        button.onClick.AddListener(OnButtonClicked);
        
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
        button.interactable= true;
        buttonText.text = active.name;
    }
    public void ShowButton(CharacterFacade possibleTarget)
    {
        button.interactable= true;
        buttonText.text = possibleTarget.name;
    }

    public void HideButton()
    {
        button.interactable= false;
        buttonText.text = "";
    }


    public void ChangePositions(GameMode gameMode)
    {
        Debug.Log("Changing position");
        UiExtensions.ChangeTurnBasedPosition(statTextPosition, _startingPosition, _outPosition, gameMode);
    }
}