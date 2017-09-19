using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private _GC _GC;

	private Rigidbody2D playerRb;
	private Animator playerAnimator;

	public float velocidade;
	private int direcao;

    public float HpMax;
    public float HP;
    private float percVida;
    public GameObject explosaoPrefab;

    public Transform barraHp;

	public GameObject[] armas;
	public int powerupColetados;

	private Transform top, down, left, right;
    

	// Use this for initialization
	void Start () {

		_GC = FindObjectOfType (typeof(_GC)) as _GC;
		top = GameObject.Find ("top").transform;
		down = GameObject.Find ("down").transform;
		left = GameObject.Find ("left").transform;
		right = GameObject.Find ("right").transform;


		playerRb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();
		barraHp = GameObject.Find ("BarraVida").transform;
		barraHp.localScale = new Vector3 (1,1,1);
        HP = HpMax;
        percVida = HP / HpMax;
		armas [powerupColetados].SetActive(true);


        
	}

	void Update(){
       

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

		if(transform.position.x < left.position.x){
			transform.position = new Vector3 (left.position.x, transform.position.y, transform.position.z);
		}else if(transform.position.x > right.position.x){
			transform.position = new Vector3 (right.position.x, transform.position.y, transform.position.z);
		}else if(transform.position.y > top.position.y){
			transform.position = new Vector3 (transform.position.x, top.position.y, transform.position.z);
		}else if(transform.position.y < down.position.y){
			transform.position = new Vector3 (transform.position.x, down.position.y, transform.position.z);
		}

		playerAnimator.SetInteger ("direcao", direcao);

	}



    void OnTriggerEnter2D(Collider2D col) {

        switch (col.gameObject.tag) {
            case "tiroInimigo":
                tomarDano(1);
                break;

		case "powerUp":
				Destroy (col.gameObject);
				powerUp ();
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
		_GC.morreu ();
         Destroy(this.gameObject);
    }

	void powerUp(){
		powerupColetados += 1;
		if (powerupColetados <= armas.Length - 1) {
			armas [powerupColetados].SetActive (true);
		}
		_GC.pontos += 250;
	}


}
