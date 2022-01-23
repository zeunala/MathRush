using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinScript : MonoBehaviour {

    public GameObject GameDirector;

    void OnTriggerEnter2D(Collider2D other)
    {/*
        bool IsGameWin = false;

        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                if (GameDirector.GetComponent<GameDirector>().CPUField[i, j] == null) break;
                else if (i == 7) IsGameWin = true;
            }
        }

        if (IsGameWin == true) */GameDirector.GetComponent<GameDirector>().GameWin();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
