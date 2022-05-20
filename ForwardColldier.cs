using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ForwardColldier : MonoBehaviour
{
    [SerializeField] AudioClip ExploreSound;
    [SerializeField] AudioClip FinSound;
    AudioSource audioSource;

    bool IsTransitioning = false;
    [SerializeField]float delayTime = 1f;
    private void OnCollisionEnter(Collision other)
    {
        if (IsTransitioning) { return; }//���IsTransitioningΪ�棬�򷵻ء�������ִ�к�������ݡ�
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("123");
                break;
            case "Enemy":
                RocketCrash();
                

                Debug.Log("enemy here");
                
                break;
            case "Finish":
                GoNextLevel();
                Debug.Log("you done");
                break;
            default:
                Debug.Log("no");                                                                                                                                                                                                             
                break;

        }
    }
     void Start()
    {
        audioSource = GetComponent<AudioSource>();//ǧ��Ҫ������һ����������������һ������һ������Ҫ��Ҫ������ڵ����ݷ�������ֵ��
                                                  //���as.֮ǰ������ֻ��֤�����������𣬵������ĸ��ط�����������û�и���������
                                                  //��������Ҫ��һ�Ρ�
    }

    void RocketCrash()
    {
        RocketExploreSound();
      
        GetComponent<Movement>().enabled = false;
        //�������˼�������object���ҵ�Movement��������Ȼ��������е�enable���ԣ������������Ա�Ϊfalse��
        Invoke("ReLoadLevel", delayTime);//Invoke������1f�����ӳ�1�롣Invoke����Ҫ���롰���ӷ��������֣�����Ǹ�ǿ���ã�����ά����    
       
    }
    void ReLoadLevel()
    {
    
        int CurrentSceneNumber = SceneManager.GetActiveScene().buildIndex;//����ǳ���Ҫ��ʹ�ó�����������
                                                                          //��ȡ��ǰ������indexֵ�������indexֵ����Ϊ��ǰ����CS��
        SceneManager.LoadScene(CurrentSceneNumber) ;//��ȡ���CS�ı�����
    }

    void LoadNextLevel()
    {   
        finsoundStart();
   
        int NextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(NextLevel == SceneManager.sceneCountInBuildSettings)
        {
            NextLevel = 0;
        }
        SceneManager.LoadScene(NextLevel);
    }
    void GoNextLevel()
    {   
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",delayTime);
    }
    void finsoundStart()
    {
        IsTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(FinSound);
    }
    void RocketExploreSound()
    {
        IsTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(ExploreSound);
    }
}
