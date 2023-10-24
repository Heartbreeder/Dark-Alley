using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScore : MonoBehaviour {
    public string leftName, rightName;
    public GameObject leftAlley, rightAlley;
    private GameObject lgs, lrs, lfs, rgs, rrs, rfs;//Scoreboards
    // Use this for initialization
    void Start () {
        lgs = transform.GetChild(0).gameObject;
        lrs = transform.GetChild(1).gameObject;
        lfs = transform.GetChild(2).gameObject;
        rgs = transform.GetChild(3).gameObject;
        rrs = transform.GetChild(4).gameObject;
        rfs = transform.GetChild(5).gameObject;
        updateScoreboard();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void updateScoreboard()
    {
        //Player names
        transform.GetChild(6).gameObject.GetComponent<TextMesh>().text = leftName;
        transform.GetChild(7).gameObject.GetComponent<TextMesh>().text = rightName;
         
        //Left Game Score
        TextMesh lgstext= lgs.GetComponent<TextMesh>();
        AlleyLogic Logic = leftAlley.GetComponent<AlleyLogic>();

        //Translate numbers to score symbols, 10 is X or / the rest are normal
        string  [] scores= new string[21];
        for (int i=0; i<21; i++)
        {
            if (Logic.gameScore[i] == 10 && i % 2 == 0 && i < 19)
            {//Strike
                scores[i] = "X";
            }else if (i % 2 == 1 && Logic.gameScore[i] + Logic.gameScore[i - 1] == 10  && i < 19)
            {//Spare
                if (Logic.gameScore[i - 1] != 10)
                {
                    scores[i] = "/";
                }
                else
                {
                    scores[i] = " ";
                }
            }
            else if ((i == 19 || i == 20) && Logic.gameScore[i] == 10)
            {
                if (Logic.gameScore[i-1] == 10 && Logic.gameScore[i] == 10)
                {//Strike
                    scores[i] = "X";
                }
                else//it is a 2nd-shot
                {//Spare
                    scores[i] = "/";
                }
            }
            else
            {
                if(Logic.gameScore[i] == 0 && Logic.gameShotCount-1<i)
                {
                    scores[i] = " ";
                }
                else
                {
                    scores[i] = Logic.gameScore[i].ToString();
                }
                
            }
        }
        lgstext.text =  scores[0] +" "+scores[1]+"    "+
                        scores[2] + " " + scores[3] + "    " +
                        scores[4] + " " + scores[5] + "    " +
                        scores[6] + " " + scores[7] + "    " +
                        scores[8] + " " + scores[9] + "    " +
                        scores[10] + " " + scores[11] + "    " +
                        scores[12] + " " + scores[13] + "    " +
                        scores[14] + " " + scores[15] + "    " +
                        scores[16] + " " + scores[17] + "   " +
                        scores[18] + " " + scores[19] + " " + scores[20];

        //Left Round Score
        TextMesh lrstext = lrs.GetComponent<TextMesh>();

        string text = "";
        for (int i=0; i < 10; i++)
        {
            if (i < Logic.totalShotCount)
            {
                for (int j = 0; j < 3 - Logic.totalScore[i].ToString().Length; j++)
                {
                   text += "_";

                }
                text += Logic.totalScore[i].ToString();
            }
            else
            {
                text += "___";
            }

            text += "   ";
        }
        lrstext.text = text;


        //Left Final Score
        TextMesh lfstext = lfs.GetComponent<TextMesh>();
        lfstext.text = Logic.playerScore.ToString();

        //Right Game Score
        TextMesh rgstext = rgs.GetComponent<TextMesh>();
        Logic = rightAlley.GetComponent<AlleyLogic>();

        //Translate numbers to score symbols, 10 is X or / the rest are normal
        for (int i = 0; i < 21; i++)
        {
            if (Logic.gameScore[i] == 10 && i % 2 == 0 && i < 19)
            {//Strike
                scores[i] = "X";
            }
            else if (i % 2 == 1 && Logic.gameScore[i] + Logic.gameScore[i - 1] == 10 && i < 19)
            {//Spare
                if (Logic.gameScore[i - 1] != 10)
                {
                    scores[i] = "/";
                }
                else
                {
                    scores[i] = " ";
                } 
            }
            else if ((i == 19 || i == 20) && Logic.gameScore[i] == 10)
            {
                if (Logic.gameScore[i - 1] == 10 && Logic.gameScore[i] == 10)
                {//Strike
                    scores[i] = "X";
                }
                else//it is a 2nd-shot
                {//Spare
                    scores[i] = "/";
                }
            }
            else
            {
                if (Logic.gameScore[i] == 0 && Logic.gameShotCount - 1 < i)
                {
                    scores[i] = " ";
                }
                else
                {
                    scores[i] = Logic.gameScore[i].ToString();
                }

            }
        }
        rgstext.text =  scores[0] + " " + scores[1] + "    " +
                        scores[2] + " " + scores[3] + "    " +
                        scores[4] + " " + scores[5] + "    " +
                        scores[6] + " " + scores[7] + "    " +
                        scores[8] + " " + scores[9] + "    " +
                        scores[10] + " " + scores[11] + "    " +
                        scores[12] + " " + scores[13] + "    " +
                        scores[14] + " " + scores[15] + "    " +
                        scores[16] + " " + scores[17] + "   " +
                        scores[18] + " " + scores[19] + " " + scores[20];

        //Right Round Score
        TextMesh rrstext = rrs.GetComponent<TextMesh>();

        text = "";
        for (int i = 0; i < 10; i++)
        {
            if (i < Logic.totalShotCount)
            {
                for (int j = 0; j < 3 - Logic.totalScore[i].ToString().Length; j++)
                {
                    text += "_";

                }
                text += Logic.totalScore[i].ToString();
            }
            else
            {
                text += "___";
            }

            text += "   ";
        }
        rrstext.text = text;

        //Right Final Score
        TextMesh rfstext = rfs.GetComponent<TextMesh>();
        rfstext.text = Logic.playerScore.ToString();


    }



}
