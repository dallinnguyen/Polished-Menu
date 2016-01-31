using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ButtonEvents : MonoBehaviour
{ 	
	public Image fade;

	public void Restart () {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LoadLevel(string s){
		fade.DOColor (new Color (0, 0, 0, 1), 0.25f).OnComplete (()=>WaitAndLoadLevel(s));
	}

	private void WaitAndLoadLevel(string s){
		Application.LoadLevel (s);
	}

	public void Start(){
		fade.DOColor (new Color (0, 0, 0, 0), 0.25f);
	}
}		
