using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject explosion;
    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){	
			
			Destroy (gameObject);
			Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
		}	
	}
}
