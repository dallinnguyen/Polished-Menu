using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Playercontroller : MonoBehaviour 
{
	[SerializeField] GameObject gameOver,gameWin;
	[SerializeField] float MovementSpeed = 10f,delayMovement = 0.2f,time1,time2;	
	private Transform _mainCameraTransform;
	
	public List<Vector3> points;
	private Vector3 previouspos;
	private	Animator _anim;
	Vector2 stopOnReleaseVector;
	Rigidbody2D rigidBody2D;
	public float directionThreshold;

	[SerializeField] int animCallNumber;

	[SerializeField] bool movementCalled,movePlayer;
				
	void Start ()
	{ 
		previouspos=transform.position;
	}
	void OnEnable () 
	{  	
		_anim = GetComponent<Animator>();	 
	} 
	
	void FixedUpdate ()  
	{ 
		if (Input.GetMouseButtonUp (0))
		{
//			Debug.Log("mouseee");
			movementCalled = true;
			movePlayer = false;
			Vector3 pos = Input.mousePosition;
			pos.z = 10;
			pos = Camera.main.ScreenToWorldPoint (pos);
			points.Clear();
			points.Add (pos);
		} 			
		if(!GameController.Instance.GameOver)
		{		
			if(movePlayer)
				{

				if(!movementCalled)
					transform.position = Vector3.MoveTowards (transform.position, points[0], MovementSpeed * Time.fixedDeltaTime);

				if (transform.position == points [0]) 
				{  
					IdleAnimEffects();			
					movePlayer = false;	
					movementCalled = true;
//					Debug.Log("222222222222");
					points.Clear();
				}   
			}

			if (points.Count  > 0 && movementCalled) 
			{ 
				if((points[0].x - transform.position.x  > directionThreshold) && (points[0].y - transform.position.y  > directionThreshold))
				{
					_anim.SetTrigger("RunTopRight");
					animCallNumber = 1;
				}
				
				else if((points[0].x - transform.position.x  > directionThreshold) && (points[0].y - transform.position.y < -directionThreshold))
				{
					_anim.SetTrigger("RunDownRight");
					animCallNumber = 2;
				}

				else if((points[0].x - transform.position.x  < -directionThreshold) && (points[0].y - transform.position.y > directionThreshold))
				{
					_anim.SetTrigger("RunTopLeft");
					animCallNumber = 3;
				}
				
				else if((points[0].x - transform.position.x  < -directionThreshold) && (points[0].y - transform.position.y  < -directionThreshold))
				{
					_anim.SetTrigger("RunDownLeft");
					animCallNumber = 4;
				}

				else 
					if(((points[0].x - transform.position.x  >= -directionThreshold) && (points[0].x - transform.position.x  <= directionThreshold)) && (points[0].y - transform.position.y > directionThreshold))
				{
					_anim.SetTrigger("RunTop");
					animCallNumber = 5;
				}
				
				else if(((points[0].x - transform.position.x  >= -directionThreshold) && (points[0].x - transform.position.x  <= directionThreshold)) && (points[0].y - transform.position.y  < directionThreshold))
				{
					_anim.SetTrigger("RunDown");
					animCallNumber = 6;
				}
				
				else if((points[0].x - transform.position.x  > directionThreshold) && ((points[0].y - transform.position.y  < directionThreshold)||(points[0].y - transform.position.y  > -directionThreshold)))
				{
					_anim.SetTrigger("RunRight");
					animCallNumber = 7;
				}
				
				else if((points[0].x - transform.position.x  < 0) && ((points[0].y - transform.position.y  < directionThreshold)||(points[0].y - transform.position.y  > -directionThreshold)))
				{
					_anim.SetTrigger("RunLeft");
					animCallNumber = 8;
				}
				
				movementCalled = false;
				movePlayer = true; 
			}

		}
	}

	void IdleAnimEffects()
	{
//		Debug.Log("idleeee");
		switch(animCallNumber)
		{
			case 1:
				_anim.SetTrigger("IdleTopRight");
				break;
				
			case 2:

			_anim.SetTrigger("IdleDownRight");
				break;
				
			case 3:

			_anim.SetTrigger("IdleTopLeft");
				break;
				
			case 4:

			_anim.SetTrigger("IdleDownLeft");
				break;
				
			case 5:

			_anim.SetTrigger("IdleTop");
				break;
				
			case 6:

			_anim.SetTrigger("IdleDown");
				break;
				
			case 7:

			_anim.SetTrigger("IdleRight");
				break;
				
			case 8:

			_anim.SetTrigger("IdleLeft");
				break;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll) 
	{
		switch(coll.tag)
		{
			case "Enemy":

				IdleAnimEffects();
				EnemyEffects();
				break;

			case "FinishPoint":
				
				IdleAnimEffects();
				FinishPointsEffects(); 
				break; 

			case "Background":

				IdleAnimEffects();
				break;
		}	
	}

	void EnemyEffects()
	{ 
		GameController.Instance.GameOver = true;
		gameOver.SetActive (true); 

		gameOver.transform.FindChild ("Background").GetComponent<RectTransform> ().DOLocalMove (Vector3.zero,0.25f);

		gameOver.transform.FindChild ("Home").GetComponent<RectTransform> ().DOLocalMoveX (0,0.25f);

		//Application.LoadLevel ("Scene_Menu");
	} 

	void FinishPointsEffects() 
	{
		//if (Application.loadedLevelName == "Level1")
		//	Application.LoadLevel ("Level7");
		//else
		//	EnemyEffects();
		gameWin.SetActive(true); // Just For Time Being

		gameWin.transform.FindChild ("Background").GetComponent<RectTransform> ().DOLocalMove (Vector3.zero,0.25f);

		gameWin.transform.FindChild ("Home").GetComponent<RectTransform> ().DOLocalMoveX (0,0.25f);

		gameWin.transform.FindChild ("NextLevel").GetComponent<RectTransform> ().DOLocalMoveX (0,0.25f);

//			.DOScale (Vector3.one, 0.25f);

		/*
		GameController.currentLevel++; 
		PlayerPrefs.SetInt("UnlockedLevels",GameController.currentLevel);
		if (GameController.currentLevel > 16) {
			GameController.currentLevel = 1;
		}		
		Application.LoadLevel ("GamePlay");
		*/
	} 
} 
