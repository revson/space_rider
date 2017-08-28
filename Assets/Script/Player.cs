using System.Collections;
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

    public float HpMax;
    public float HP;
    private float percVida;
    public GameObject explosaoPrefab;

    public Transform barraHp;
    

	// Use this for initialization
	void Start () {
		playerRb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();
        HP = HpMax;
        percVida = HP / HpMax;
        
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

    void OnTriggerEnter2D(Collider2D col) {

        switch (col.gameObject.tag) {
            case "tiroInimigo":
                tomarDano(1);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        switch (col.gameObject.tag)
        {
            case "naveInimiga":
                tomarDano(5);
                break;
        }
    }

    void tomarDano(int danoTomado)
    {
        HP -= danoTomado;
        percVida = HP / HpMax;
        Vector3 theScale = barraHp.localScale;
        theScale.x = percVida;
        barraHp.localScale = theScale;        

        if (HP <= 0)
        {
            explodir();
        }
    }

    void explodir()
    {
        //para instanciar o prefab sem manipular configuracoes, linha abaixo
        //Instantiate(explosaoPrefab, transform.position, Quaternion.identity);
        GameObject tempPrefab = Instantiate(explosaoPrefab) as GameObject;
        tempPrefab.transform.position = transform.position;
         Destroy(this.gameObject);
    }
}
