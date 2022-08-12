using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHitScorePoint = 2;
    [SerializeField] int enemyKilledSP = 100;
    [SerializeField] GameObject EnemyParticles;
    [SerializeField] GameObject EnemyBeHitParticles;
    GameObject SpawnPlace;
    ScoreBoard scoreBoard;

    [SerializeField] int enemyHp = 5;
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        if(GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
        SpawnPlace = GameObject.FindWithTag("SpawnAtRunTime");
        
    }

    void OnParticleCollision(GameObject other)
    {
        EnemyBeHit();
        if (enemyHp < 1)
        {
            EnemyKill();
        }
    }

    private void EnemyBeHit()
    {
        scoreBoard.IncreaseScore(enemyKilledSP);
        GameObject EnemyBetHitVFX = Instantiate(EnemyBeHitParticles, transform.position, Quaternion.identity);
        EnemyBetHitVFX.transform.parent = SpawnPlace.transform;

        enemyHp = enemyHp - 1;//�һ���ϰ������д�Ƚ�ֱ�ۣ���ʦ��д���ǣ�enemyHP --.�Լ�����1��
    }
    void EnemyKill()
    {
        scoreBoard.IncreaseScore(enemyKilledSP);
        GameObject EnemyVFX = Instantiate(EnemyParticles, transform.position, Quaternion.identity);
        EnemyVFX.transform.parent = SpawnPlace.transform;
        Destroy(gameObject);
    }
    
}
