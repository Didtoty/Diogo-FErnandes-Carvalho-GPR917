using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cacEnnemis : MonoBehaviour {

    [SerializeField] private float PlayerDamage; 
    private int Timer = 5;
    private float life = 3;
    private GameManager gameManager;

    /*private Animator animator;*/
    public GameObject target; //the enemy's target
    [SerializeField]public float moveSpeed = 2  ; //move speed


    // Use this for initialization
    void Start()
    {
        /*animator = GetComponent<Animator>();*/
        target = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Bullet")
        {
            life--;
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
            Debug.Log("Message : Enemy Take damage");
            if (life == 0)
            {
                Destroy(this.gameObject);
                gameManager.PlayerWin();
            }
        }       
    }
    
}


