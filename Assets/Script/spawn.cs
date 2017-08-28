﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject inimigoPrefab;
    public float tempoSpawn;
    public Transform limiteEsquerdo, limiteDireito;
    private float minX, maxX;

    private float tempoTime;


	// Use this for initialization
	void Start () {
        minX = limiteEsquerdo.position.x;
        maxX = limiteDireito.position.x;
	}
	
	// Update is called once per frame
	void Update () {

        tempoTime += Time.deltaTime;
        if (tempoTime >= tempoSpawn) {
            tempoTime = 0;
            Spawn();
        }

	}

    void Spawn() {

        GameObject tempPrefab = Instantiate(inimigoPrefab) as GameObject;
        float posX = Random.Range(minX, maxX);
        tempPrefab.transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
