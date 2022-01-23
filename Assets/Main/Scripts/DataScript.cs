using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataScript : MonoBehaviour {
    public int gameLevel; // Normal:1, Hard:2, Extreme:3, Impossible:4

    public int removeBlock=0; // 제거한 블록 수, 총 시간으로 나누면 평속
    public int attackBlock=0; // 공격한 블록 수, 총 시간으로 나누면 공속
    public float correctAnswer = 0; // 정답개수
    public float wrongAnswer = 0; // 오답개수
    public float playTime = 0;

    public int result; // Win:1, Lose:2

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
