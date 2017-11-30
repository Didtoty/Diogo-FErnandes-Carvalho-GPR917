using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
 
public class PlayerController : MonoBehaviour {

    [Header("Physics")]
    [SerializeField] private float force = 10;

    [Range(0.0f,10.0f)]

    [Header("Jumps")]
    [SerializeField] private float forceJump = 5;
    [SerializeField] private Transform positionRaycastJump; // Position du détecteur de collision
    [SerializeField] private float radiusRaycastJump; // Rayon de la détection de collision
    [SerializeField] private LayerMask layerMaskJump; // Layout physique de l'objet

    [Header("Sounds")]
    


    [Header("Fire gun")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform GunTransform; // Pour la sortie du tire
    [SerializeField] private float bulletVelocity = 2.0f; // Vitesse de la balle 
    [SerializeField] private float time2Fire = 2.0f; // Cooldown de tir
    private float lastTimeFire = 0; // Dernier moment ou on a tiré pour la derniere fois 

    private int Timer = 5;
    private float life = 3;
    private Rigidbody2D rigid;
    private Transform spawnTransform;
    [SerializeField] private Animator PlayerAnimation;
    private GameManager gameManager;

   
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        spawnTransform = GameObject.Find("Spawn").transform;
        gameManager = FindObjectOfType<GameManager>();
       
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 forceDirection = new Vector2(horizontalInput, 0);
        forceDirection *= 10;
        rigid.AddForce(forceDirection);
       
        bool touchFloor = Physics2D.OverlapCircle(positionRaycastJump.position, radiusRaycastJump, layerMaskJump);

        if (Input.GetAxis("Jump") > 0 && touchFloor)
        {
         
            rigid.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
         
        }

        if(Input.GetAxis("Fire1") > 0)
        {
            Fire();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemys")
        {
            life--;
            gameManager.TakeDamage();
            StartCoroutine(Damage());
            Debug.Log("Message : Player Take damage");
            if (life == 0)
            {
                gameManager.TakeDamage();
            }
        }
        if (collision.tag == "Heart")
        {
            gameManager.Vie();
            Debug.Log("Message : you win one life");
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Limit")
        {
            transform.position = spawnTransform.position;
            gameManager.TakeDamage();
        }

        if (collision.tag == "BulletEnemy")
        {
            Destroy(collision.gameObject);
            gameManager.TakeDamage();
        }
       

    }

    private IEnumerator Damage()
    {
        for (int hit = 0; hit < Timer; hit++)
        {
            GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);

        }
    }

    private void Fire()
    {
        if(Time.realtimeSinceStartup - lastTimeFire > time2Fire)
        { 
            GameObject bullet = Instantiate(bulletPrefab, GunTransform.position, GunTransform.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = GunTransform.right * bulletVelocity; 
            Destroy(bullet, 5);
            lastTimeFire = Time.realtimeSinceStartup;
        }
    }

}

