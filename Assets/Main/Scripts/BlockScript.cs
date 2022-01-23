using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockScript : MonoBehaviour {
    
    //int player = 100; // 100: default, 1: player, 2:CPU
    //int col = 100; // 0~4
    public int row = 100; // 0~7
    float velocity = 0.0f;
    
    public GameObject GameDirector;

    public void Destructor() { Destroy(gameObject); }

    void Update()
    {
        //BlockYMove
        if(this.transform.position.y>YPosition()) // 블록의 row에 따른 위치보다 실제 위치가 더 높은 경우
        {
            this.velocity += Physics.gravity.y * Time.deltaTime * 0.5f; // 0.5f는 속도계수
            this.transform.Translate(0, velocity, 0);
        }
        if (this.transform.position.y < YPosition() || (this.transform.position.y == YPosition() && velocity != 0))
        {
            this.transform.position = new Vector3(transform.position.x, YPosition(), 0);
            this.velocity = 0;
        }
    }

    private float YPosition()
    {
        float position=100.0f;
        switch(row)
        {
            case 0:
                position = -1.92f;
                break;
            case 1:
                position = -1.12f;
                break;
            case 2:
                position = -0.32f;
                break;
            case 3:
                position = 0.48f;
                break;
            case 4:
                position = 1.28f;
                break;
            case 5:
                position = 2.08f;
                break;
            case 6:
                position = 2.88f;
                break;
            case 7:
                position = 3.68f;
                break;
            case 8: // 게임 오버라인
                position = 4.48f;
                break;
        }

        if (this.tag == "+10" || this.tag == "+11" || this.tag == "+12" || this.tag == "+13" || this.tag == "+14" || this.tag == "+15" || this.tag == "+16" || this.tag == "+17" || this.tag == "+18" || this.tag == "+19" || this.tag == "+20" || this.tag == "-10" || this.tag == "-11" || this.tag == "-12" || this.tag == "-13" || this.tag == "-14" || this.tag == "-15" || this.tag == "-16" || this.tag == "-17" || this.tag == "-18" || this.tag == "-19" || this.tag == "-20")
            position += 0.03f;
        else if (this.tag == "dummy")
            position -= 0.03f;

        return position;
    }
}
