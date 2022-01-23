using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    int player = 100; // 100: default, 1: player, 2:CPU
    int col = 100; // 0~4
    int row = 100; // 0~7

    public GameObject GameDirector;

    

    // Use this for initialization
    void Start ()
    {
        if (transform.position.x > -4.8f && transform.position.x < -4.4f) { player = 1; col = 0; }
        else if (transform.position.x > -4.0f && transform.position.x < -3.6f) { player = 1; col = 1; }
        else if (transform.position.x > -3.2f && transform.position.x < -2.8f) { player = 1; col = 2; }
        else if (transform.position.x > -2.4f && transform.position.x < -2.0f) { player = 1; col = 3; }
        else if (transform.position.x > -1.6f && transform.position.x < -1.2f) { player = 1; col = 4; }
        else if (transform.position.x > 1.2f && transform.position.x < 1.6f) { player = 2; col = 0; }
        else if (transform.position.x > 2.0f && transform.position.x < 2.4f) { player = 2; col = 1; }
        else if (transform.position.x > 2.8f && transform.position.x < 3.2f) { player = 2; col = 2; }
        else if (transform.position.x > 3.6f && transform.position.x < 4.0f) { player = 2; col = 3; }
        else if (transform.position.x > 4.4f && transform.position.x < 4.8f) { player = 2; col = 4; }

        if (transform.position.y > -2.16f && transform.position.y < -1.76f) { row = 0; }
        else if (transform.position.y > -1.36f && transform.position.y < -0.96f) { row = 1; }
        else if (transform.position.y > -0.56f && transform.position.y < -0.16f) { row = 2; }
        else if (transform.position.y > 0.24f && transform.position.y < 0.64f) { row = 3; }
        else if (transform.position.y > 1.04f && transform.position.y < 1.44f) { row = 4; }
        else if (transform.position.y > 1.84f && transform.position.y < 2.24f) { row = 5; }
        else if (transform.position.y > 2.64f && transform.position.y < 3.04f) { row = 6; }
        else if (transform.position.y > 3.44f && transform.position.y < 3.88f) { row = 7; }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player == 1)
        {
            GameDirector.GetComponent<GameDirector>().playField[row, col] = other.gameObject;
        }
        else if (player == 2)
        {
            GameDirector.GetComponent<GameDirector>().CPUField[row, col] = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (player == 1)
        {
            if(row>0)
            GameDirector.GetComponent<GameDirector>().playField[row, col] = null;
        }
        else if (player == 2)
        {
            if(row>0)
            GameDirector.GetComponent<GameDirector>().CPUField[row, col] = null;
        }
    }
}
