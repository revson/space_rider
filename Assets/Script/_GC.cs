using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _GC : MonoBehaviour {
    public Text pontuacao;
    public int pontos;

	public int vidasExtras;
	public GameObject iconeVida;
	public Transform vidasExtrasIcones;
	public GameObject player;
	public GameObject spawnPlayer;

	public GameObject[] extras;
	// Use this for initialization
	void Start () {
		vidas ();
	}
	
	// Update is called once per frame
	void Update () {
        pontuacao.text = pontos.ToString();
	}

	// funcao que coloca os icones de vida embaixo da barra de hp
	void vidas(){

		GameObject tempVida;
		float posXico;

		// antes de criar os icones verifica se ja existem e destroy para criar os novos.
		foreach(GameObject v in extras){
			if(v != null){
				Destroy (v);
			}
		}

		for(int i = 0; i < vidasExtras; i++){

			posXico = vidasExtrasIcones.position.x + (0.9f * i);
			tempVida = Instantiate(iconeVida) as GameObject;
			extras [i] = tempVida;
			tempVida.transform.position = new Vector3 (posXico, vidasExtrasIcones.position.y, vidasExtrasIcones.position.z );

		}

		//GameObject tempPlayer = Instantiate (player) as GameObject;
		Instantiate (player, spawnPlayer.transform.position, spawnPlayer.transform.rotation);

	}

	public void morreu(){

		vidasExtras -= 1;
		if (vidasExtras >= 0) {
			vidas ();
		} else {
			Application.LoadLevel("gameOver");
		}

	}
}
