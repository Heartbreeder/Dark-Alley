using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyLogic : MonoBehaviour {
	public bool isRight;
    public GameObject spawnTo;
    public GameObject[] pins;
    public GameObject scoreboard;

    private GameObject curBall;
    private Vector3 ballSpawn;


    public int gameShotCount = 0;
    public int totalShotCount = 0;
    public int[] gameScore = new int[21];
    public int[] totalScore = new int[10];
    public int playerScore = 0;

    // Use this for initialization
    void Start () {
        resetScore();
        ballSpawn = spawnTo.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball"))
        {
            curBall = other.gameObject;
            //curBall.transform.SetParent( gameObject.transform);
            StartCoroutine("BowlingSequence");
        }
		//Destroy(other.gameObject);
	}

    IEnumerator BowlingSequence()
    {
        yield return new WaitForSeconds(5f);

        //Calculate this shot's score

        //Check how many pins fell
        int roundScore = 0;
        for (int i = 0; i < 10; i++)
        {
            PinLogic pin = pins[i].GetComponent<PinLogic>();
            if (!pin.isStanding)
            {
                roundScore++;
            }
        }

        //It is the first shot of a pair
        if (gameShotCount % 2 == 0 && gameShotCount < 18)
        {
            gameScore[gameShotCount] = roundScore;
            gameShotCount++;
            if (roundScore == 10)//it is a strike; so we skip a shot
            {
                gameShotCount++;
                resetPins();
                //TODO play something cheerful here?
            }
        }//It is the second shot of a pair
        else if (gameShotCount % 2 == 1 && gameShotCount < 18)
        {
            gameScore[gameShotCount] = roundScore - gameScore[gameShotCount-1];
            gameShotCount++;
            resetPins();
        }//final 3 shots have special conditions
        else if (gameShotCount == 18)
        {
            gameScore[gameShotCount] = roundScore;
            gameShotCount++;
            if (roundScore == 10)
            {
                resetPins();
                //TODO play something cheerful here?
            }
        }
        else if (gameShotCount == 19)
        {
            if (gameScore[gameShotCount - 1] == 10)//it is a 1st-shot round
            {
                gameScore[gameShotCount] = roundScore;
                gameShotCount++;
                if (roundScore == 10)
                {
                    resetPins();
                }
            }
            else//it is a 2nd-shot
            {
                gameScore[gameShotCount] = roundScore - gameScore[gameShotCount - 1];
                gameShotCount++;
                resetPins();
                if (gameScore[gameShotCount] != 10)//Here we don't have a strike or spare so skip the last shot
                {
                    gameShotCount++;
                }
            }
        }
        else if(gameShotCount == 20)
        {
            if (gameScore[gameShotCount - 1] == 10)//it is a 1st-shot round
            {
                gameScore[gameShotCount] = roundScore;
                gameShotCount++;
            }
            else//it is a 2nd-shot
            {
                gameScore[gameShotCount] = roundScore - gameScore[gameShotCount - 1];
                gameShotCount++;
            }
            resetPins();
        }

        //Re-calculate all round scores, the last one is our player's score
        int count = 0;
        for (int i = 0; i < 9; i++)
        {//8 first rounds the same, 9th round sligtly different
            int curscore = gameScore[2 * i] + gameScore[2 * i + 1];
            if (gameScore[2 * i] == 10)
            {//Strike, add the next 2 shots
                if (i != 8 && gameScore[2 * i + 2] != 10)
                {
                    curscore += gameScore[2 * i + 2] + gameScore[2 * i + 3];
                }
                else
                {
                    curscore += gameScore[2 * i + 2];
                }
                if(i != 8 && gameScore[2 * i + 2] == 10)
                {//Second strike add another 1 shot
                        curscore += gameScore[2 * i + 4];

                }
                else if (i == 8 && gameScore[2 * i + 2] == 10)
                {
                    curscore += gameScore[2 * i + 3];
                }
            }else if (gameScore[2 * i] != 10 && gameScore[2 * i] + gameScore[2 * i + 1] == 10)
            {//Spare , add 1 more shot
                curscore += gameScore[2 * i + 2];
            }
            count += curscore;
            totalScore[i] = count;
        }
        //10th round is just a sum
        totalScore[9] = count + gameScore[18] + gameScore[19] + gameScore[20];

        totalShotCount = gameShotCount / 2;
        if (totalShotCount > 9) { totalShotCount = 9; }

         playerScore = totalScore[totalShotCount];

        if (gameShotCount > 20)
        {
            //End game? reset score?
            //resetScore();
        }

        //Re-draw the scoreboard
        scoreboard.SendMessage("updateScoreboard");
        //Reset the ball
        if (curBall != null)
        {
            curBall.transform.position = ballSpawn;
            //curBall.transform.SetParent(null);
            curBall = null;
        }

    }
    void resetPins()
    {
        for (int i = 0; i < 10; i++)
        {
            pins[i].SendMessage("ResetPos");
        }
        
    }

    void resetScore()
    {
        for (int i = 0; i < 21; i++)
        {
            gameScore[i] = 0;
        }
        for (int i = 0; i < 10; i++)
        {
            totalScore[i] = 0;
        }
        gameShotCount = 0;
        totalShotCount = 0;
    }

    //Called by the FoulLogic's collider
    void Foul()
    {
        StartCoroutine("BowlingSequence");
    }
}
