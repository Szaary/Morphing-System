using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalInput : MonoBehaviour
{
	public void OnMenu(InputValue value)
	{
		Debug.Log("On menu clicked");
	}
}