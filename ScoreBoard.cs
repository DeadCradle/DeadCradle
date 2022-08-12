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
    public void IncreaseScore(int amountToIncrease)//���amountToIncrease���Ǵ�����źš�
                                                   //������źţ�Ҫ�ڱ�Ĵ�����д�ˣ�������
                                                   //��Щ�ˡ���Ϊ���ⲿ���롣
    {
        score += amountToIncrease;
        Debug.Log(score);
        scoreText.text = score.ToString();
    }
  }

