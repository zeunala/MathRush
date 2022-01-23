using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour {

    public GameObject GameData;


	// Use this for initialization
	void Start () {
        //GameObject.Find("GameDirector");
        GameData = GameObject.Find("GameData");
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void NormalButtonDown()
    {
        GameData.GetComponent<DataScript>().gameLevel = 1;
        Debug.Log("Normal");
        Debug.Log(GameData.GetComponent<DataScript>().gameLevel);
        SceneManager.LoadScene("Ready");
    }
    public void HardButtonDown()
    {
        GameData.GetComponent<DataScript>().gameLevel = 2;
        Debug.Log("Hard");
        Debug.Log(GameData.GetComponent<DataScript>().gameLevel);
        SceneManager.LoadScene("Ready");

    }
    public void ExtremeButtonDown()
    {
        GameData.GetComponent<DataScript>().gameLevel = 3;
        Debug.Log("Extreme");
        Debug.Log(GameData.GetComponent<DataScript>().gameLevel);
        SceneManager.LoadScene("Ready");
    }
    public void ImpossibleButtonDown()
    {
        GameData.GetComponent<DataScript>().gameLevel = 4;
        Debug.Log("Impossible");
        Debug.Log(GameData.GetComponent<DataScript>().gameLevel);
        SceneManager.LoadScene("Ready");
    }
}
