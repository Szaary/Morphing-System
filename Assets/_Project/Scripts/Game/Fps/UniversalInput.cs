using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalInput : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;
	public void OnTurn(InputValue value)
	{
		gameManager.SetGameMode(GameMode.TurnBasedFight);
	}
	public void OnFPS(InputValue value)
	{
		gameManager.SetGameMode(GameMode.Fps);
	}
	
	
	
	
	
	public void OnMenu(InputValue value)
	{
		Debug.Log("On menu clicked");
	}
	
	

}