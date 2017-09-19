using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaInimigo : MonoBehaviour {

    private _GC _GC;

    private Rigidbody2D inimigoRb;
    private Animator inimigoAnimator;

    public float velocidade;
    private int direcao;

    public Transform arma;
    public GameObject tiroPrefab;
    public float forcaTiro;

    private int movimentoX;
    private int movimentoY;

    public float tempoCurva;
    public int aleatorio;
    private float tempoTime;
    private int range;

    public int chanceTiro;
    public float tempoTiro;
    private float tempoTimeTiro;

    public int HP;

    public GameObject explosaoPrefab;

    public int pontosGanhos;

	public GameObject loot;
	public float chanceDrop;

    // Use this for initialization
    void Start () {
        _GC = FindObjectOfType(typeof(_GC)) as _GC;
        inimigoRb = GetComponent<Rigidbody2D>();
        inimigoAnimator = GetComponent<Animator>();
        movimentoY = -1;
	}
	
	// Update is called once per frame
	void Update () {

        tempoTime += Time.deltaTime;
        tempoTimeTiro += Time.deltaTime;

        if (tempoTime >= tempoCurva) {

            tempoTime = 0;
            range = Random.Range(0,100);
            
			if (range <= aleatorio) {
                range = Random.Range(0, 100);

               
                if (range < 50){
                    movimentoX = -1;
                    direcao = 1;
                }else{
                    movimentoX = 1;
                    direcao = -1;
                }

            }else{
                movimentoX = 0;
                direcao = 0;
            }
        }

        if (tempoTimeTiro >= tempoTiro) {

            tempoTimeTiro = 0;
            range = Random.Range(0, 100);

            if (range <= chanceTiro) {
                atirar();
            }
        }

        inimigoAnimator.SetInteger("direcao", direcao);
        inimigoRb.velocity = new Vector2(movimentoX * velocidade, movimentoY * velocidade);
    }

    void atirar() {
        GameObject tempPrefab = Instantiate(tiroPrefab) as GameObject;
        tempPrefab.transform.position = arma.position;
        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaTiro));
    }

    void OnTriggerEnter2D(Collider2D col) {

        switch (col.gameObject.tag) {

            case "tiro":
                tomarDano(1);
                break;
            
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        switch (col.gameObject.tag)
        {

            case "Player":
                Destroy(this.gameObject);
                break;

        }
    }

    void tomarDano(int danoTomado) {
        HP -= danoTomado;

        if (HP <= 0) {
            explodir();           
        }
    }

    void explodir() {
        //para instanciar o prefab sem manipular configuracoes, linha abaixo
        //Instantiate(explosaoPrefab, transform.position, Quaternion.identity);
        GameObject tempPrefab = Instantiate(explosaoPrefab) as GameObject;
        tempPrefab.transform.position = transform.position;
        tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidade * -1);
        _GC.pontos += pontosGanhos;

		aleatorio = Random.Range (0, 100);

		if(aleatorio <= chanceDrop){
			Instantiate(loot, transform.position, transform.rotation);
		}

        Destroy(this.gameObject);
    }
}
