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

    public void Initialize()
    {
        button.onClick.AddListener(OnButtonClicked);
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



}