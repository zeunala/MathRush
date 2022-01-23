using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultDirector : MonoBehaviour {

    public GameObject[] resultText = new GameObject[4];
    public GameObject[] rank = new GameObject[13];
    public GameObject GameData;

    private bool goToTitle = false; // 결과 다 나오면 true가 되며 마우스 클릭시 타이틀로 이동


    // Use this for initialization
    void Start () {
        GameData = GameObject.Find("GameData");
        for (int i = 0; i < 4; i++)
            resultText[i].SetActive(false);
        for (int i = 0; i < 13; i++)
            rank[i].SetActive(false);

        Invoke("resultText0", 1);
        Invoke("resultText1", 1.5f);
        Invoke("resultText2", 2);
        Invoke("resultText3", 2.5f);
        Invoke("totalRank", 3.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (goToTitle==true && Input.GetMouseButtonDown(0))
        {
            GameData.GetComponent<DataScript>().gameLevel = 0; //check
            GameData.GetComponent<DataScript>().removeBlock = 0;
            GameData.GetComponent<DataScript>().attackBlock = 0;
            GameData.GetComponent<DataScript>().correctAnswer = 0;
            GameData.GetComponent<DataScript>().wrongAnswer = 0;
            GameData.GetComponent<DataScript>().playTime = 0;
            SceneManager.LoadScene("Title");
        }

    }

    void resultText0() // 시간
    {
        resultText[0].GetComponent<TextMesh>().text = (System.Math.Round(GameData.GetComponent<DataScript>().playTime, 1).ToString()) + " sec";
        resultText[0].SetActive(true);
    }

    void resultText1() // 평속
    {
        resultText[1].GetComponent<TextMesh>().text = (System.Math.Round((GameData.GetComponent<DataScript>().removeBlock)/GameData.GetComponent<DataScript>().playTime,1).ToString() + " blc/sec");
        resultText[1].SetActive(true);
    }

    void resultText2() // 공속
    {
        resultText[2].GetComponent<TextMesh>().text = (System.Math.Round((GameData.GetComponent<DataScript>().attackBlock) / GameData.GetComponent<DataScript>().playTime,1).ToString() + " blc/sec");
        resultText[2].SetActive(true);
    }

    void resultText3() // 정답률
    {
        resultText[3].GetComponent<TextMesh>().text = System.Math.Round(GameData.GetComponent<DataScript>().correctAnswer*100 / (GameData.GetComponent<DataScript>().correctAnswer + GameData.GetComponent<DataScript>().wrongAnswer), 1).ToString() + "%";
        resultText[3].SetActive(true);
    }

    void totalRank() // 랭크
    {
        float rankPoint= (GameData.GetComponent<DataScript>().removeBlock) / GameData.GetComponent<DataScript>().playTime 
            + (GameData.GetComponent<DataScript>().attackBlock) / (GameData.GetComponent<DataScript>().playTime)
            * GameData.GetComponent<DataScript>().correctAnswer / (GameData.GetComponent<DataScript>().correctAnswer + GameData.GetComponent<DataScript>().wrongAnswer);

        if (GameData.GetComponent<DataScript>().result == 2)
            rank[12].SetActive(true);
        else if (rankPoint >= 10.0f)
            rank[0].SetActive(true);
        else if(rankPoint>=7.0f)
            rank[1].SetActive(true);
        else if(rankPoint>=5.0f)
            rank[2].SetActive(true);
        else if(rankPoint>=4.0f)
            rank[3].SetActive(true);
        else if (rankPoint >= 3.0f)
            rank[4].SetActive(true);
        else if (rankPoint >= 2.7f)
            rank[5].SetActive(true);
        else if (rankPoint >= 2.4f)
            rank[6].SetActive(true);
        else if (rankPoint >= 2.0f)
            rank[7].SetActive(true);
        else if (rankPoint >= 1.6f)
            rank[8].SetActive(true);
        else if (rankPoint >= 1.2f)
            rank[9].SetActive(true);
        else if (rankPoint >= 0.6f)
            rank[10].SetActive(true);
        else
            rank[11].SetActive(true);

        goToTitle = true;
    }



}
