using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public static GameController Instance;

	public bool GameOver;

	void Awake () 
	{
		Instance = this;
	} 
	

}
