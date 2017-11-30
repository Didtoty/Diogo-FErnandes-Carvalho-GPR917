using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private int lifes = 3;
    private int CountEnemys = 0;
    [SerializeField] private Text textLifes;
    [SerializeField] private Text textennemis;

    private const string TEXT_LIFES = "Lifes : ";
    private const string TEXT_ENNEMIS = "Ennemis : ";

    // Use this for initialization
    void Start () {
        CountEnemys = GameObject.FindGameObjectsWithTag("Enemys").Length;
        textLifes.text = TEXT_LIFES + lifes;
        textennemis.text = TEXT_ENNEMIS + CountEnemys;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerWin()
    {
        CountEnemys--;
        if (CountEnemys > 0)
        {
            textennemis.text = TEXT_ENNEMIS + CountEnemys;
        }
        else
        {
            SceneManager.LoadScene("WinMenu");

        }
    }
    public void Vie()
    {
        lifes++;
        textLifes.text = TEXT_LIFES + lifes;
    
    }
    public void TakeDamage()
    {
        
        lifes--;
        if(lifes > 0)
        {
            textLifes.text = TEXT_LIFES + lifes;
        }
        else
        {
            SceneManager.LoadScene("DeadMenu");
        }
    }

}
