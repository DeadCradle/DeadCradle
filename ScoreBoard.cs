using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score";
    }
    public void IncreaseScore(int amountToIncrease)//这个amountToIncrease则是传入的信号。
                                                   //传入的信号，要在别的代码里写了，不能在
                                                   //这些了。因为是外部传入。
    {
        score += amountToIncrease;
        Debug.Log(score);
        scoreText.text = score.ToString();
    }
  }

