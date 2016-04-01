using UnityEngine;
using System.Collections;
using MySql.Data;

public class VerificaColisaoCubo : MonoBehaviour {

	void OnCollisionEnter(Collision c) 
	{
		if (c.gameObject.transform.parent.name.Equals ("index")) 
		{
			Debug.Log ("Colidiu");
		}		

		Debug.Log ("Colidiu 2");
	}


}
