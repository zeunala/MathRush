using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {

    public GameObject BlockGenerator;
    public GameObject GameData;

    public float dropSpan;
    float dropDelta = 0;

    public float attackSpan;
    float attackDelta = 0;

    float gamePlayTime = 0;

    float Playerstatus = 0.0f; // 0.0f가 아니면 입력이 막힌다. 입력을 막아야 하는 상황에서 해당 초를 설정
    float CPUstatus = 0.0f; // CPU는 입력을 막아야할 상황이 게임 종료시뿐으로 이부분은 CPU span만 조절하면 되므로 불필요. 하지만 이후 1대1구현을 위해 변수만 선언

    public GameObject[,] playField = new GameObject[9, 5]; // [row, col], 8행은 게임오버 및 블록삭제 때의 행간이동시 코드의 용이함을 위한것이며 평시 null로 유지, 블록 삭제시 행간 이동 외에는 0~7행까지만 사용한다.
    public GameObject[,] CPUField = new GameObject[9, 5];

    public AudioClip lineclear;
    public AudioClip gameover;
    public AudioClip gamewin;
    AudioSource aud;

    public GameObject gameoverSign;
    public GameObject gamewinSign;

    public static double EvalExpression(char[] expr)
    {
        double result = 0;
        double[] temp1 = { -100, -100, -100, -100, -100, -100, -100, -100 }; // -100 : default
        int temp1Count = 0;
        char[] temp2 = { '?', '?','?', '?', '?', '?', '?' };
        int temp2Count = 0;

        int temp = 0;
        for(int i=0; i<expr.Length; i++)
        {
            if(expr[i]>='0' && expr[i]<='9')
            {
                temp *= 10;
                temp += (expr[i] - '0');
            }
            else if(expr[i]=='+' || expr[i]=='-' || expr[i]=='*' || expr[i]=='/')
            {
                temp1[temp1Count++] = temp;
                temp = 0;
                temp2[temp2Count++] = expr[i];
            }
        }
        temp1[temp1Count++] = temp;
        temp = 0;

        // 11.20 update
        for(int i=0; i<temp2Count; i++)
        {
            if(temp2[i]=='*')
            {
                temp1Count--;
                temp2Count--;

                temp1[i] *= temp1[i + 1];
                for (int j = i + 1; j < temp1Count; j++)
                {
                    temp1[j] = temp1[j + 1];
                }
                for (int j = i; j < temp2Count; j++)
                {
                    temp2[j] = temp2[j + 1];
                }
            }
            else if (temp2[i] == '/')
            {
                temp1Count--;
                temp2Count--;

                temp1[i] /= temp1[i + 1];
                for (int j = i + 1; j < temp1Count; j++)
                {
                    temp1[j] = temp1[j + 1];
                }
                for (int j = i; j < temp2Count; j++)
                {
                    temp2[j] = temp2[j + 1];
                }
            }
        }

        result = temp1[0];
        for(int i=0; i<temp2Count; i++)
        {
            if (temp2[i] == '+') result += temp1[i + 1];
            else if (temp2[i] == '-') result -= temp1[i + 1];
            //else if (temp2[i] == '*') result *= temp1[i + 1];
            //else if (temp2[i] == '/') result /= temp1[i + 1];
        }

        return Convert.ToDouble(result.ToString("F1"));
    }
    //Use ex. EvalExpression("2+5*2".ToCharArray()) 
    // ***********************
    string FieldToString(int player, int row_, int col_)
    {
        if (player == 1)
        {
            string temp = playField[row_, col_].tag;
            if (temp == "dummy") return ("*999999");
            if (temp == "x2" || temp == "x3" || temp == "x4" || temp == "x5" || temp == "x6" || temp == "x7" || temp == "x8" || temp == "x9")
                return ("*" + temp.Substring(1));
            else if (temp == "z2" || temp == "z3" || temp == "z4" || temp == "z5" || temp == "z6" || temp == "z7" || temp == "z8" || temp == "z9")
                return ("/" + temp.Substring(1));
            else
                return temp;
        }
        else
        {
            string temp = CPUField[row_, col_].tag;
            if (temp == "dummy") return ("*999999");
            if (temp == "x2" || temp == "x3" || temp == "x4" || temp == "x5" || temp == "x6" || temp == "x7" || temp == "x8" || temp == "x9")
                return ("*" + temp.Substring(1));
            else if (temp == "z2" || temp == "z3" || temp == "z4" || temp == "z5" || temp == "z6" || temp == "z7" || temp == "z8" || temp == "z9")
                return ("/" + temp.Substring(1));
            else
                return temp;
        }
    }
   
    public bool LineClear(int player, double answer)
    {
        if (player == 1)
        {
            if (Playerstatus != 0.0f) // 입력 가능 상태 아니면 막는다. CPU측에선 구현 불필요해 이 부분없음, 이후 1대1 구현시 삽입
                return false;

            int[,] playerFieldClear = new int[8, 5]; // 0: false, 1: true
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 5; j++)
                    playerFieldClear[i,j] = 0;

            int deleteCount = 0;
            for(int i=7; i>=0; i--) // row
            {
                for(int j=0; j<5; j++) // col
                {
                    for (int k1 = 2; k1 <= (5 - j); k1++) // k1 : number of blocks that checking to right
                    {
                        string command = "";
                        for (int k2=0; k2<k1; k2++)
                        {
                            if (playField[i, j + k2] == null) break;
                            else command += FieldToString(1, i, j + k2);

                            if(k2==(k1-1))
                            {
                                command = command.Substring(1,command.Length-1); //Debug.Log(command);
                                //Debug.Log(command+" "+EvalExpression(command.ToCharArray()));
                                if (answer == EvalExpression(command.ToCharArray()))
                                {
                                    for (int k3 = 0; k3 < k1; k3++)
                                    {
                                        playerFieldClear[i, j + k3] = 1;
                                        //Debug.Log("정답:"+playField[i,j+k3].tag);
                                    }
                                }
                            }
                        }
                    }
                    for(int k1=2; k1<=i+1; k1++) // k1 : number of blocks that checking to down
                    {
                        string command = "";
                        for (int k2=0; k2<k1; k2++)
                        {

                            if (playField[i - k2, j] == null) break;
                            else command += FieldToString(1, i - k2, j);

                            if(k2==(k1-1))
                            {
                                command = command.Substring(1, command.Length - 1);
                                if(answer==EvalExpression(command.ToCharArray()))
                                {
                                    for(int k3=0; k3<k1; k3++)
                                    {
                                        playerFieldClear[i - k3, j] = 1;
                                        //Debug.Log("정답:" + playField[i-k3, j].tag);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            
            for (int i=0; i<8; i++) // delete blocks
            {
                for(int j=0; j<5; j++)
                {
                    if (playerFieldClear[i, j] == 1)
                    {
                        if (playField[i, j] != null)
                        {
                            //Debug.Log("player제거:" + playField[i, j].tag);
                            Destroy(playField[i, j]); 
                            playField[i, j] = null;
                            
                            deleteCount++;
                            GameData.GetComponent<DataScript>().removeBlock++;
                        }
                    }
                }
            }

            for (int i = 0; i < 8; i++) // block down
            {
                for (int j = 0; j < 5; j++)
                {
                    if (playField[i, j] == null)
                    {
                        for (int k = i; k < 8; k++)
                        {
                            playField[k, j] = playField[k + 1, j];
                            if (playField[k, j] != null)
                                playField[k, j].GetComponent<BlockScript>().row = k;
                        }
                        // 해당 열의 위에 null이 아닌게 하나라도 있으면 j--를 통해 그자리의 null여부를 다시 검사한다.
                        for(int k2=i+1; k2<8; k2++)
                        {
                            if (playField[k2, j] != null)
                            {
                                j--;
                                break;
                            }
                        }
                    }
                }
            }

            if(deleteCount==0)
            {
                // penalty something
                Playerstatus += 1.5f; // 오답시 1.5초간 입력을 막는다.
                GameData.GetComponent<DataScript>().wrongAnswer++;
                return false;
            }
            else
            {
                aud.PlayOneShot(lineclear);
                GameData.GetComponent<DataScript>().correctAnswer++;
                if (deleteCount>=3)
                {
                    if (deleteCount == 3)
                    {
                        BlockGenerator.GetComponent<BlockGenerator>().CPUattacked += 1;
                        GameData.GetComponent<DataScript>().attackBlock += 1;
                    }
                    else if (deleteCount == 4)
                    {
                        BlockGenerator.GetComponent<BlockGenerator>().CPUattacked += 3;
                        GameData.GetComponent<DataScript>().attackBlock += 3;
                    }
                    else if (deleteCount >= 5 && deleteCount <= 7)
                    {
                        BlockGenerator.GetComponent<BlockGenerator>().CPUattacked += 5;
                        GameData.GetComponent<DataScript>().attackBlock += 5;
                    }
                    else if (deleteCount >= 8 && deleteCount <= 10)
                    {
                        BlockGenerator.GetComponent<BlockGenerator>().CPUattacked += 7;
                        GameData.GetComponent<DataScript>().attackBlock += 7;
                    }
                    else
                    {
                        BlockGenerator.GetComponent<BlockGenerator>().CPUattacked += 9;
                        GameData.GetComponent<DataScript>().attackBlock += 9;
                    }
                }
                return true;
            }

        }

        if (player == 2)
        {
            int[,] CPUFieldClear = new int[8, 5]; // 0: false, 1: true
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 5; j++)
                    CPUFieldClear[i, j] = 0;

            int deleteCount = 0;
            for (int i = 7; i >= 0; i--) // row
            {
                for (int j = 0; j < 5; j++) // col
                {
                    for (int k1 = 2; k1 <= (5 - j); k1++) // k1 : number of blocks that checking to right
                    {
                        string command = "";
                        for (int k2 = 0; k2 < k1; k2++)
                        {
                            if (CPUField[i, j + k2] == null) break;
                            else command += FieldToString(2, i, j + k2);

                            if (k2 == (k1 - 1))
                            {
                                command = command.Substring(1, command.Length - 1); //Debug.Log(command);
                                //Debug.Log(command + " " + EvalExpression(command.ToCharArray()));
                                if (answer == EvalExpression(command.ToCharArray()))
                                {
                                    for (int k3 = 0; k3 < k1; k3++)
                                    {
                                        CPUFieldClear[i, j + k3] = 1;
                                        //Debug.Log("정답");
                                    }
                                }
                            }
                        }
                    }
                    for (int k1 = 2; k1 <= i + 1; k1++) // k1 : number of blocks that checking to down
                    {
                        string command = "";
                        for (int k2 = 0; k2 < k1; k2++)
                        {

                            if (CPUField[i - k2, j] == null) break;
                            else command += FieldToString(2, i - k2, j);

                            if (k2 == (k1 - 1))
                            {
                                command = command.Substring(1, command.Length - 1);
                                if (answer == EvalExpression(command.ToCharArray()))
                                {
                                    for (int k3 = 0; k3 < k1; k3++)
                                    {
                                        CPUFieldClear[i - k3, j] = 1;
                                    }
                                }
                            }
                        }
                    }

                }
            }

            for (int i = 0; i < 8; i++) // delete blocks
            {
                for (int j = 0; j < 5; j++)
                {
                    if (CPUFieldClear[i, j] == 1)
                    {
                        if (CPUField[i, j] != null)
                        {
                            //Debug.Log("CPU제거:" + playField[i, j].tag);
                            Destroy(CPUField[i, j]); 
                            CPUField[i, j] = null;
                            deleteCount++;
                        }
                    }
                }
            }

            for (int i = 0; i < 8; i++) // block down
            {
                for (int j = 0; j < 5; j++)
                {
                    if (CPUField[i, j] == null)
                    {
                        for (int k = i; k < 8; k++)
                        {
                            CPUField[k, j] = CPUField[k + 1, j];
                            if (CPUField[k, j] != null)
                                CPUField[k, j].GetComponent<BlockScript>().row = k;
                        }
                        // 해당 열의 위에 null이 아닌게 하나라도 있으면 j--를 통해 그자리의 null여부를 다시 검사한다.
                        for (int k2 = i + 1; k2 < 8; k2++)
                        {
                            if (CPUField[k2, j] != null)
                            {
                                j--;
                                break;
                            }
                        }

                    }
                }
            }

            if (deleteCount == 0)
            {
                // penalty something
                return false;
            }
            else
            {
                aud.PlayOneShot(lineclear);
                if (deleteCount >= 3)
                {
                    if (deleteCount == 3)
                        BlockGenerator.GetComponent<BlockGenerator>().Playerattacked += 1;
                    else if(deleteCount==4)
                        BlockGenerator.GetComponent<BlockGenerator>().Playerattacked += 3;
                    else if (deleteCount>=5 && deleteCount<=7)
                        BlockGenerator.GetComponent<BlockGenerator>().Playerattacked += 5;
                    else if (deleteCount >= 8 && deleteCount <= 10)
                        BlockGenerator.GetComponent<BlockGenerator>().Playerattacked += 7;
                    else
                        BlockGenerator.GetComponent<BlockGenerator>().Playerattacked += 9;
                }
                return true;
            }

        }
        return false;
    } // (변수).Substring(1)로 첫번째 문자 제거 가능

    public int NextEmptyRow(int player_, int col_) // 해당 플레이어필드의 특정 열에서 0행부터 시작하여 처음으로 빈 행(0~7)을 int로 리턴, 없으면 게임종료 후 8리턴
    {
        if (player_ == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                if (playField[i, col_] == null)
                    return i;
            }
            GameOver();//플레이어 필드에 떨어뜨릴 공간이 없음
            return 8;
        }
        else
        {
            for(int i=0; i<8; i++)
            {
                if (CPUField[i, col_] == null)
                    return i;
            }
            GameWin();//CPU 필드에 떨어뜨릴 공간이 없음
            return 8;
        }
    }

    // Use this for initialization
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        GameData = GameObject.Find("GameData");

        Debug.Log("Start!"+ GameData.GetComponent<DataScript>().gameLevel);

        switch(GameData.GetComponent<DataScript>().gameLevel)
        {
            case 1:
                dropSpan = 6.0f;
                attackSpan = 4.5f;
                Debug.Log("1");
                break;
            case 2:
                dropSpan = 4.0f;
                attackSpan = 2.4f;
                Debug.Log("2");
                break;
            case 3:
                dropSpan = 3.0f;
                attackSpan = 1.5f;
                Debug.Log("3");
                break;
            case 4:
                dropSpan = 1.0f;
                attackSpan = 0.3f;
                Debug.Log("4");
                break;
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                playField[i, j] = null;
                CPUField[i, j] = null;
            }
        }
        gameoverSign.SetActive(false);
        gamewinSign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        dropDelta += Time.deltaTime;
        if (dropDelta > dropSpan)
        {
            dropDelta = 0;
            switch (GameData.GetComponent<DataScript>().gameLevel)
            {
                case 1:
                    break;
                case 2:
                    dropSpan *= 0.95f;
                    break;
                case 3:
                    dropSpan *= 0.9f;
                    attackSpan *= 0.95f;
                    break;
                case 4:
                    dropSpan *= 0.8f;
                    attackSpan *= 0.8f;
                    break;
            }
            BlockGenerator.GetComponent<BlockGenerator>().BlockDropAll();
        }

        attackDelta += Time.deltaTime;
        if (attackDelta > attackSpan)
        {
            attackDelta = 0;
            for (int i = 30; i > -31; i--)
            {
                if (LineClear(2, i) == true) break;
            }
        }

        gamePlayTime += Time.deltaTime;

        if(Playerstatus!=0.0f)
        {
            Playerstatus -= Time.deltaTime;
            if (Playerstatus < 0)
                Playerstatus = 0.0f;
        }
    }

    public void GameWin()
    {
        if(dropSpan!=100000) aud.PlayOneShot(gamewin);
        dropSpan = 100000;
        attackSpan = 100000;
        Playerstatus = 100000.0f;
        CPUstatus = 100000.0f;
        BlockGenerator.GetComponent<BlockGenerator>().Playerattacked = 0;
        BlockGenerator.GetComponent<BlockGenerator>().CPUattacked = 0;
        gamewinSign.SetActive(true);
        GameData.GetComponent<DataScript>().playTime = gamePlayTime;
        GameData.GetComponent<DataScript>().result = 1;

        Invoke("GoToResult", 3);
    }

    public void GameOver()
    {
        if(dropSpan!=100000) aud.PlayOneShot(gameover);
        dropSpan = 100000;
        attackSpan = 100000;
        Playerstatus = 100000.0f;
        CPUstatus = 100000.0f;
        gameoverSign.SetActive(true);
        GameData.GetComponent<DataScript>().playTime = gamePlayTime;
        GameData.GetComponent<DataScript>().result = 2;

        Invoke("GoToResult", 3);
    }

    void GoToResult()
    {
        //Debug.Log("평속:"+(GameData.GetComponent<DataScript>().removeBlock)/gamePlayTime);
        //Debug.Log("공속:" + (GameData.GetComponent<DataScript>().attackBlock) / gamePlayTime);
        //Debug.Log("정답률:" + (GameData.GetComponent<DataScript>().correctAnswer) / (GameData.GetComponent<DataScript>().correctAnswer+GameData.GetComponent<DataScript>().wrongAnswer));
        //Debug.Log("총점:" + ((GameData.GetComponent<DataScript>().removeBlock) / gamePlayTime + (GameData.GetComponent<DataScript>().attackBlock) / gamePlayTime) * (GameData.GetComponent<DataScript>().correctAnswer) / (GameData.GetComponent<DataScript>().correctAnswer + GameData.GetComponent<DataScript>().wrongAnswer));
        SceneManager.LoadScene("Result");
    }
}
