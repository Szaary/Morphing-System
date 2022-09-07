using System;
using TMPro;
using UnityEngine;
using Zenject;

public class FpsUiBinds : MonoBehaviour
{
    public FpsUiPosition health;
    public FpsUiPosition magazine;

    [SerializeField] private GameObject cross;

    private CharactersLibrary _library;
    private Statistic _chosenStat;

    [SerializeField] protected BaseStatistic statistic;
    private GameManager _gameManager;
    private RangedWeaponController weapon;
    [Inject]
    public void Construct(CharactersLibrary library, GameManager gameManager)
    {
        _library = library;
        _gameManager = gameManager;
    }

    private void Start()
    {
        health.StartingPositionVector = health.statTextPosition.anchoredPosition;
        health.OutPositionVector = health.outPosition.anchoredPosition;

        magazine.StartingPositionVector = magazine.statTextPosition.anchoredPosition;
        magazine.OutPositionVector = magazine.outPosition.anchoredPosition;


        health.statTextPosition.anchoredPosition = health.OutPositionVector;
        magazine.statTextPosition.anchoredPosition = magazine.OutPositionVector;
        
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;

        if (_library.GetControlledCharacter(out var facade) == Result.Success)
        {
            OnControlledCharacterChanged(facade);
        }

        _library.ControlledCharacterChanged += OnControlledCharacterChanged;
    }
    
    
    

    private void OnGameModeChanged(GameMode obj)
    {
        if (obj == GameMode.Fps)
        {
            cross.gameObject.SetActive(true);
            UiExtensions.ChangePosition(health.statTextPosition, health.StartingPositionVector);
            UiExtensions.ChangePosition(magazine.statTextPosition, magazine.StartingPositionVector);
        }
        else
        {
            cross.gameObject.SetActive(false);
            UiExtensions.ChangePosition(health.statTextPosition, health.OutPositionVector);
            UiExtensions.ChangePosition(magazine.statTextPosition, magazine.OutPositionVector);
        }
    }

    private void OnControlledCharacterChanged(CharacterFacade facade)
    {
        if(weapon!=null) weapon.magazineChanged -= OnMagazineChanged;
        weapon = facade.rangedWeaponController;
        weapon.magazineChanged += OnMagazineChanged;
        OnMagazineChanged(weapon.Magazine, weapon.weapon.MagazineSize);

        if (_chosenStat != null) _chosenStat.OnValueChanged -= OnValueChanged;
        facade.GetStatistic(statistic, out _chosenStat);
        SetBar(_chosenStat.CurrentValue, _chosenStat.maxValue);
        _chosenStat.OnValueChanged += OnValueChanged;
    }

    private void OnMagazineChanged(int magazineChanged, int magazineSize)
    {
        magazine.statText.text = $"{magazineChanged:0} / {magazineSize:0}";
    }

    private void OnValueChanged(float modifier, float currentValue, float maxValue, Result result)
    {
        SetBar(currentValue, maxValue);
    }

    private void SetBar(float currentValue, float maxValue)
    {
        health.statText.text = $"{currentValue:0} / {maxValue:0}";
    }

    private void OnDestroy()
    {
        if (_chosenStat != null) _chosenStat.OnValueChanged -= OnValueChanged;
        if (weapon != null) weapon.magazineChanged -= OnMagazineChanged;
        _library.ControlledCharacterChanged -= OnControlledCharacterChanged;
        _gameManager.GameModeChanged -= OnGameModeChanged;
    }

    [Serializable]
    public class FpsUiPosition
    {
        public TextMeshProUGUI statText;
        public RectTransform statTextPosition;
        public RectTransform outPosition;
        public Vector2 StartingPositionVector { get; set; }
        public Vector2 OutPositionVector { get; set; }
    }
}