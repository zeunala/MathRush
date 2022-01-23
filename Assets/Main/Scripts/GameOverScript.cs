using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    public GameObject GameDirector;

    void OnTriggerEnter2D(Collider2D other)
    {/*
        bool IsGameOver = false;

        for(int j=0; j<5; j++)
        {
            for(int i=0; i<8; i++)
            {
                if (GameDirector.GetComponent<GameDirector>().playField[i, j] == null) break;
                else if (i == 7) IsGameOver = true;
            }
        }

        if (IsGameOver == true) */GameDirector.GetComponent<GameDirector>().GameOver();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
