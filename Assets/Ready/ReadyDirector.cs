using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyDirector : MonoBehaviour {
    float Timer = 0;
    int audCount = 3;

    public GameObject readySign;
    public GameObject threeSign;
    public GameObject twoSign;
    public GameObject oneSign;
    public GameObject goSign;

    public AudioClip countdown;
    public AudioClip start;
    public AudioSource aud;

	// Use this for initialization
	void Start ()
    {
        Timer = 0;
        audCount = 3;
        aud = GetComponent<AudioSource>();
        readySign.SetActive(false);
        threeSign.SetActive(false);
        twoSign.SetActive(false);
        oneSign.SetActive(false);
        goSign.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Timer>=0 && Timer<=1)
        {
            readySign.SetActive(true);
        }
        else if(Timer>1 && Timer <=2)
        {
            readySign.SetActive(false);
            threeSign.SetActive(true);
            if (audCount == 3) { aud.PlayOneShot(countdown); audCount--; }
        }
        else if(Timer>2 && Timer <=3)
        {
            threeSign.SetActive(false);
            twoSign.SetActive(true);
            if (audCount == 2) { aud.PlayOneShot(countdown); audCount--; }
        }
        else if(Timer>3 && Timer <=4)
        {
            twoSign.SetActive(false);
            oneSign.SetActive(true);
            if (audCount == 1) { aud.PlayOneShot(countdown); audCount--; }
        }
        else if(Timer>=4 && Timer <=5)
        {
            oneSign.SetActive(false);
            goSign.SetActive(true);
            if (audCount == 0) { aud.PlayOneShot(start); audCount--; }
        }
        else if(Timer>5)
        {
            goSign.SetActive(false);
            SceneManager.LoadScene("Main");
        }
        Timer += Time.deltaTime;
        //Debug.Log(Timer);
    }
}
