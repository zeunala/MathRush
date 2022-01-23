using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

    public GameObject[] block = new GameObject[57];
    public GameObject wait1;
    public GameObject wait6;
    private GameObject[] playerWait = new GameObject[5];
    private GameObject[] CPUWait = new GameObject[5];
    public AudioClip smallattack;
    public AudioClip bigattack;
    public AudioClip span;
    AudioSource aud;
    public GameObject GameDirector;

    // 11.20 update
    public int Playerattacked=0;
    public float Playerspan = 3.0f; // 이게 0이되면 대기중인 블록이 모두 떨어짐
    public int CPUattacked=0;
    public float CPUspan = 3.0f;


    // Use this for initialization
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        BlockDropAll();
        BlockDropAll();

        for(int i=0; i<5; i++)
        {
            playerWait[i] = null;
            CPUWait[i] = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerWait[i] != null)
                Destroy(playerWait[i]);
            if (CPUWait[i] != null)
                Destroy(CPUWait[i]);
        }

        if (Playerattacked != 0)
        {
            Playerspan -= Time.deltaTime; // 주기가 끝나면 떨어짐
            if (Playerspan <= 0)
            {
                BlockPlayerDrop(Playerattacked);
                Playerattacked = 0;
                Playerspan = 3.0f;
            }

            if(CPUattacked!=0) // 상대방도 대기 블록이 있으면 상쇄
            {
                if(Playerattacked>CPUattacked)
                {
                    Playerattacked -= CPUattacked;
                    CPUattacked = 0;
                }
                else
                {
                    CPUattacked -= Playerattacked;
                    Playerattacked = 0;
                }
            }

            if(Playerattacked>=6) // 대기블록 표시
            {
                playerWait[0] = Instantiate(wait6) as GameObject;
                playerWait[0].transform.position = new Vector3(-5.8f, 3.8f, 0);
            }
            else
            {
                playerWait[0] = Instantiate(wait1) as GameObject;
                playerWait[0].transform.position = new Vector3(-5.8f, 3.8f, 0);
            }

            if (Playerattacked >= 12)
            {
                playerWait[1] = Instantiate(wait6) as GameObject;
                playerWait[1].transform.position = new Vector3(-5.8f, 2.6f, 0);
            }
            else if (Playerattacked >= 2)
            {
                playerWait[1] = Instantiate(wait1) as GameObject;
                playerWait[1].transform.position = new Vector3(-5.8f, 2.6f, 0);
            }
            else
            {
                if (playerWait[1] != null)
                    Destroy(playerWait[1]);
            }

            if (Playerattacked >= 18)
            {
                playerWait[2] = Instantiate(wait6) as GameObject;
                playerWait[2].transform.position = new Vector3(-5.8f, 1.4f, 0);
            }
            else if (Playerattacked >= 3)
            {
                playerWait[2] = Instantiate(wait1) as GameObject;
                playerWait[2].transform.position = new Vector3(-5.8f, 1.4f, 0);
            }
            else
            {
                if (playerWait[2] != null)
                    Destroy(playerWait[2]);
            }

            if (Playerattacked >= 24)
            {
                playerWait[3] = Instantiate(wait6) as GameObject;
                playerWait[3].transform.position = new Vector3(-5.8f, 0.2f, 0);
            }
            else if (Playerattacked >= 4)
            {
                playerWait[3] = Instantiate(wait1) as GameObject;
                playerWait[3].transform.position = new Vector3(-5.8f, 0.2f, 0);
            }
            else
            {
                if (playerWait[3] != null)
                    Destroy(playerWait[3]);
            }

            if (Playerattacked >= 30)
            {
                playerWait[4] = Instantiate(wait6) as GameObject;
                playerWait[4].transform.position = new Vector3(-5.8f, -1.0f, 0);
            }
            else if (Playerattacked >= 5)
            {
                playerWait[4] = Instantiate(wait1) as GameObject;
                playerWait[4].transform.position = new Vector3(-5.8f, -1.0f, 0);
            }
            else
            {
                if (playerWait[4] != null)
                    Destroy(playerWait[4]);
            }
        }
        else
        {
            Playerspan = 3.0f;
        }

        if (CPUattacked != 0)
        {
            CPUspan -= Time.deltaTime; // 주기가 끝나면 떨어짐
            if (CPUspan <= 0)
            {
                BlockCPUDrop(CPUattacked);
                CPUattacked = 0;
                CPUspan = 3.0f;
            }

            if (Playerattacked != 0) // 상대방도 대기 블록이 있으면 상쇄
            {
                if (CPUattacked > Playerattacked)
                {
                    CPUattacked -= Playerattacked;
                    Playerattacked = 0;
                }
                else
                {
                    Playerattacked -= CPUattacked;
                    CPUattacked = 0;
                }
            }

            if (CPUattacked >= 6) // 대기블록 표시
            {
                CPUWait[0] = Instantiate(wait6) as GameObject;
                CPUWait[0].transform.position = new Vector3(5.8f, 3.8f, 0);
            }
            else
            {
                CPUWait[0] = Instantiate(wait1) as GameObject;
                CPUWait[0].transform.position = new Vector3(5.8f, 3.8f, 0);
            }

            if (CPUattacked >= 12)
            {
                CPUWait[1] = Instantiate(wait6) as GameObject;
                CPUWait[1].transform.position = new Vector3(5.8f, 2.6f, 0);
            }
            else if (CPUattacked >= 2)
            {
                CPUWait[1] = Instantiate(wait1) as GameObject;
                CPUWait[1].transform.position = new Vector3(5.8f, 2.6f, 0);
            }
            else
            {
                if (CPUWait[1] != null)
                    Destroy(CPUWait[1]);
            }

            if (CPUattacked >= 18)
            {
                CPUWait[2] = Instantiate(wait6) as GameObject;
                CPUWait[2].transform.position = new Vector3(5.8f, 1.4f, 0);
            }
            else if (CPUattacked >= 3)
            {
                CPUWait[2] = Instantiate(wait1) as GameObject;
                CPUWait[2].transform.position = new Vector3(5.8f, 1.4f, 0);
            }
            else
            {
                if (CPUWait[2] != null)
                    Destroy(CPUWait[2]);
            }

            if (CPUattacked >= 24)
            {
                CPUWait[3] = Instantiate(wait6) as GameObject;
                CPUWait[3].transform.position = new Vector3(5.8f, 0.2f, 0);
            }
            else if (CPUattacked >= 4)
            {
                CPUWait[3] = Instantiate(wait1) as GameObject;
                CPUWait[3].transform.position = new Vector3(5.8f, 0.2f, 0);
            }
            else
            {
                if (CPUWait[3] != null)
                    Destroy(CPUWait[3]);
            }

            if (CPUattacked >= 30)
            {
                CPUWait[4] = Instantiate(wait6) as GameObject;
                CPUWait[4].transform.position = new Vector3(5.8f, -1.0f, 0);
            }
            else if (CPUattacked >= 5)
            {
                CPUWait[4] = Instantiate(wait1) as GameObject;
                CPUWait[4].transform.position = new Vector3(5.8f, -1.0f, 0);
            }
            else
            {
                if (CPUWait[4] != null)
                    Destroy(CPUWait[4]);
            }
        }
        else
        {
            CPUspan = 3.0f;
        }
    }

    public void BlockDropAll()
    {
        int[] rand = new int[10];
        for(int i=0; i<10; i++) { rand[i] = Random.Range(1, 57);  }
        for(int i=0; i<10; i++)
        {
            GameObject temp = Instantiate(block[rand[i]-1]) as GameObject;
            int tempRow;
            if (i>=0&&i<=4) // 플레이어 필드
            {
                temp.transform.position = new Vector3(-4.6f+(0.8f*i), 4.48f, 0);
                tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(1, i);
                GameDirector.GetComponent<GameDirector>().playField[tempRow, i] = temp;
                temp.GetComponent<BlockScript>().row = tempRow;
            }
            else if(i>=5&&i<=9) // 상대 필드
            {
                temp.transform.position = new Vector3(-2.6f + (0.8f * i), 4.48f, 0);
                tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(2, i-5);
                GameDirector.GetComponent<GameDirector>().CPUField[tempRow, i - 5] = temp;
                temp.GetComponent<BlockScript>().row = tempRow;
            }
        }
        this.aud.PlayOneShot(this.span);
    }

    public void Block5DropPlayer() // 5개 이상 떨궈야할때 균형있게 떨어뜨리기 위함
    {
        int[] rand = new int[5];
        for (int i = 0; i < 5; i++) { rand[i] = Random.Range(1, 57); }
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(block[rand[i] - 1]) as GameObject;
            int tempRow;

            temp.transform.position = new Vector3(-4.6f + (0.8f * i), 4.48f, 0);
            tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(1, i);
            GameDirector.GetComponent<GameDirector>().playField[tempRow, i] = temp;
            temp.GetComponent<BlockScript>().row = tempRow;

        }
    }

    public void Block5DropCPU()
    {
        int[] rand = new int[10];
        for (int i = 5; i < 10; i++) { rand[i] = Random.Range(1, 57); }
        for (int i = 5; i < 10; i++)
        {
            GameObject temp = Instantiate(block[rand[i] - 1]) as GameObject;
            int tempRow;

            temp.transform.position = new Vector3(-2.6f + (0.8f * i), 4.48f, 0);
            tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(2, i - 5);
            GameDirector.GetComponent<GameDirector>().CPUField[tempRow, i - 5] = temp;
            temp.GetComponent<BlockScript>().row = tempRow;
        }
    }

    public void BlockPlayerDrop(int n)
    {
        int total = n;

        while (n >= 5) // 떨굴 일반 블록이 5개 이상일때, 5개를 균형있게 떨어뜨리는 함수를 호출하고 떨굴 블록 -5
        {
            Block5DropPlayer();
            n -= 5;
        }
        for (int dropCount = 0; dropCount < n; dropCount++) // normal block
        {
            int rand = Random.Range(1, 57);
            int position = Random.Range(0, 5);
            GameObject temp = Instantiate(block[rand-1]) as GameObject;
            int tempRow;

            temp.transform.position = new Vector3(-4.6f + (0.8f * position), 4.48f, 0);
            tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(1, position);
            GameDirector.GetComponent<GameDirector>().playField[tempRow, position] = temp;
            temp.GetComponent<BlockScript>().row = tempRow;

        }
        for(int i = 0; i < (total/5); i++) // dummy block
        {
            int position = Random.Range(0, 5);
            GameObject temp = Instantiate(block[56]) as GameObject;
            int tempRow;

            temp.transform.position = new Vector3(-4.6f + (0.8f * position), 4.48f, 0);
            tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(1, position);
            GameDirector.GetComponent<GameDirector>().playField[tempRow, position] = temp;
            temp.GetComponent<BlockScript>().row = tempRow;
        }
        if (total<=5) { this.aud.PlayOneShot(this.smallattack); } // sound effects
        else { this.aud.PlayOneShot(this.bigattack); }

    }

    public void BlockCPUDrop(int n)
    {
        int total = n;

        while (n >= 5)
        {
            Block5DropCPU();
            n -= 5;
        }
        for (int dropCount = 0; dropCount < n; dropCount++) // normal block
        {
            int rand = Random.Range(1, 57);
            int position = Random.Range(0, 5);
            GameObject temp = Instantiate(block[rand - 1]) as GameObject;
            int tempRow;

            temp.transform.position = new Vector3(1.4f + (0.8f * position), 4.48f, 0);
            tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(2, position);
            GameDirector.GetComponent<GameDirector>().CPUField[tempRow, position] = temp;
            temp.GetComponent<BlockScript>().row = tempRow;
        }
        for (int i = 0; i < (total/5); i++) // dummy block
        {
            int position = Random.Range(0, 5);
            GameObject temp = Instantiate(block[56]) as GameObject;
            int tempRow;

            temp.transform.position = new Vector3(1.4f + (0.8f * position), 4.48f, 0);
            tempRow = GameDirector.GetComponent<GameDirector>().NextEmptyRow(2, position);
            GameDirector.GetComponent<GameDirector>().CPUField[tempRow, position] = temp;
            temp.GetComponent<BlockScript>().row = tempRow;
        }
        if (total<=5) { this.aud.PlayOneShot(this.smallattack); } // sound effects
        else { this.aud.PlayOneShot(this.bigattack); }

    }




    
}
