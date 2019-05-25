using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public Text scoreText;
    public Text multiText;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public static GameManager instance;
    bool isEnd = false;
    public float totalNotes, normalHits, goodHits, perfectHits, missedHits;
    public GameObject resultsScreen;
    public Text percentHit, normalsText, goodsText, perfectsText, missedsText, rankText, finalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown) 
            {
                startPlaying = true;
                theBS.hasStarted = true;
                Invoke("StartMusic", 3);
            }
        } else 
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy && isEnd)
            {
                resultsScreen.SetActive(true);
                
                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missedsText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHits = (totalHit/totalNotes) * 100f;

                percentHit.text = percentHits.ToString("F1") + "%";

                string rankVal = "F";

                if(percentHits > 40)
                {
                    rankVal = "D";
                    if(percentHits > 55)
                    {
                        rankVal = "C";
                        if(percentHits > 70)
                        {
                            rankVal = "B";
                            if(percentHits > 85)
                            {
                                rankVal = "A";
                                if(percentHits > 95)
                                 {
                                  rankVal = "S";
                                 }
                            }
                        }
                    }
                }

                rankText.text = rankVal;
                finalScoreText.text = currentScore.ToString();

            }
        }
    }

    public void NoteHit()
    {
        

        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
        multiplierTracker++;
        if(multiplierThresholds[currentMultiplier-1] <= multiplierTracker)
        {
            multiplierTracker = 0;
            currentMultiplier++;
        }
        }

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        multiText.text = "Multiplier: x" + currentMultiplier;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;  
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier; 
        NoteHit();
        goodHits++;
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier; 
        NoteHit(); 
        perfectHits++;    
    }
    public void NoteMissed()
    {
       currentMultiplier = 1;
       multiplierTracker = 0;
       multiText.text = "Multiplier: x" + currentMultiplier;
       missedHits++;
    }

    void StartMusic()
    {
        theMusic.Play();
        isEnd = true;
    }
}
