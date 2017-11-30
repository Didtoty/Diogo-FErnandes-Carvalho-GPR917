using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenue : MonoBehaviour {
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject uiGamePanel;

    private bool isInPause = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetButton("Pause") && !isInPause){
            isInPause = true;
            pausePanel.SetActive(true);
            uiGamePanel.SetActive(false);
            Time.timeScale = 0;
        }	

	}
    public void OnResumeGameButtonDown()
    {
        isInPause = false;
        pausePanel.SetActive(false);
        uiGamePanel.SetActive(true);
        Time.timeScale = 1;
    
    }
}

