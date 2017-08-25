﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D playerRb;
	private Animator playerAnimator;

	public float velocidade;
	private int direcao;

	public Transform arma;
	public GameObject tiroPrefab;
	public float forcaTiro;

	// Use this for initialization
	void Start () {
		playerRb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();
	}

	void Update(){
		if(Input.GetButtonDown("Fire1") ){
			atirar ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float movimentoX = Input.GetAxis ("Horizontal");
		float movimentoY = Input.GetAxis ("Vertical");
		//adaptacao para uso do analogico
		direcao = 0;
		if(movimentoX < 0){
			direcao = -1;
		}else if(movimentoX > 0){
			direcao = 1;
		}





		playerRb.velocity = new Vector2 (movimentoX * velocidade, movimentoY * velocidade);
		playerAnimator.SetInteger ("direcao", direcao);

	}

	void atirar(){
		GameObject tempPrefab = Instantiate (tiroPrefab) as GameObject;
		tempPrefab.transform.position = arma.position;
		tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, forcaTiro));
	}
}
