using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTimer : MonoBehaviour {

    public float tempoVida;
    private float tempoTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        tempoTime += Time.deltaTime;
        if (tempoTime >= tempoVida) {
            Destroy(this.gameObject);
        }

	}
}
