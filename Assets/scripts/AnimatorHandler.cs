using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatorHandler : MonoBehaviour 
{
	[SerializeField] List <SpriteRenderer>_sprite;
	[SerializeField]	Sprite image;
	[SerializeField] List<Animator> _anim;

	void OnEnable ()
	{

		AnimManagement (true);
	}
	
	// Update is called once per frame
	void OnDisable () 
	{
		for (int j=0; j<_sprite.Count; j++) 
		{
			_sprite[j].sprite = image;
		}

		AnimManagement (false); 
	}

	void AnimManagement(bool boolValue)
	{


		for(int i= 0; i< _anim.Count; i++)
		{
			_anim[i].enabled = boolValue;
		}
	} 
}
