using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class becameInvisible : MonoBehaviour {

	void OnBecameInvisible(){
		Destroy (this.gameObject);
	}
}
