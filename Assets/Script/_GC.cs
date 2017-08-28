using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class _GC : MonoBehaviour {
    public Text pontuacao;
    public int pontos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pontuacao.text = pontos.ToString();
	}
}
