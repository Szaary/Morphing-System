using TMPro;
using UnityEngine;
using Zenject;

public class FpsUiBinds : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statText;

    [SerializeField] private RectTransform statTextPosition;
    [SerializeField] private RectTransform outPosition;
    private Vector2 _startingPosition;
    private Vector2 _outPosition;

    [SerializeField] private GameObject cross;

    private CharactersLibrary _library;
    private CharacterFacade _facade;
    private Statistic _chosenStat;

    [SerializeField] protected BaseStatistic statistic;
    private GameManager _gameManager;

    [Inject]
    public void Construct(CharactersLibrary library, GameManager gameManager)
    {
        _library = library;
        _gameManager = gameManager;
    }

    private void Start()
    {
        _startingPosition = statTextPosition.anchoredPosition;
        _outPosition = outPosition.anchoredPosition;

        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;

        if (_library.GetControlledCharacter(out var facade)== Result.Success)
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
            UiExtensions.ChangePosition(statTextPosition, _startingPosition);
        }
        else
        {
            cross.gameObject.SetActive(false);
            UiExtensions.ChangePosition(statTextPosition, _outPosition);
        }
    }

    private void OnControlledCharacterChanged(CharacterFacade facade)
    {
        if (_chosenStat != null) _chosenStat.OnValueChanged -= OnValueChanged;

        _facade = facade;
        _facade.GetStatistic(statistic, out _chosenStat);

        SetBar(_chosenStat.CurrentValue, _chosenStat.maxValue);
        _chosenStat.OnValueChanged += OnValueChanged;
    }

    private void OnValueChanged(float modifier, float currentValue, float maxValue, Result result)
    {
        SetBar(currentValue, maxValue);
    }

    private void SetBar(float currentValue, float maxValue)
    {
        statText.text = $"{currentValue:0} / {maxValue:0}";
    }

    private void OnDestroy()
    {
        if (_chosenStat != null) _chosenStat.OnValueChanged -= OnValueChanged;
        _library.ControlledCharacterChanged -= OnControlledCharacterChanged;
        _gameManager.GameModeChanged -= OnGameModeChanged;
    }
}