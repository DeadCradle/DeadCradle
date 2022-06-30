using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ForwardColldier : MonoBehaviour
{
    [SerializeField] AudioClip ExploreSound;
    [SerializeField] AudioClip FinSound;
    [SerializeField] ParticleSystem SuccessP;
    [SerializeField] ParticleSystem FailP;


    AudioSource audioSource;
    ParticleSystem particleS;
    bool collisionDisable = false;
    bool IsTransitioning = false;
    [SerializeField] float delayTime = 1f;
    private void OnCollisionEnter(Collision other)
    {
        if (IsTransitioning||collisionDisable) { return; }//���IsTransitioningΪ�棬�򷵻ء�������ִ�к�������ݡ���һ��collisonDisable���
        switch (other.gameObject.tag)//�������Ӵ�����TAG��ʲô���Զ���⡣
        {
            case "Friendly":
                break;
            case "Enemy":
                RocketCrash();
                break;
            case "Finish":
                GoNextLevel();
                break;
            default:
                break;

        }
    }
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        particleS = GetComponent<ParticleSystem>();        //ǧ��Ҫ������һ����������������һ������һ������Ҫ��Ҫ������ڵ����ݷ�������ֵ��
                                                           //���asourse.֮ǰ������ֻ��֤�����������𣬵������ĸ��ط�����������û�и���������
                                                           //��������Ҫ��һ�Ρ�
    }
    void Update()
    {
        Cheater();
        LostCollider();

    }

    void RocketCrash()
    {
        RocketExploreSound();
        FailP.Play();                                          //��ʽ�ǣ� particalSystem.Play(),��Ϊǰ���Ѿ�������FailP��ParticalSystem������ֱ��ʹ���������ּ��ɡ�
        GetComponent<Movement>().enabled = false;           //�������˼�������object���ҵ�Movement��������Ȼ��������е�enable���ԣ������������Ա�Ϊfalse��
        Invoke("ReLoadLevel", delayTime);                   //Invoke������1f�����ӳ�1�롣Invoke����Ҫ���롰���ӷ��������֣�����Ǹ�ǿ���ã�����ά����    

    }

    void ReLoadLevel()
    {

        int CurrentSceneNumber = SceneManager.GetActiveScene().buildIndex;//����ǳ���Ҫ��ʹ�ó�����������
                                                                          //��ȡ��ǰ������indexֵ�������indexֵ����Ϊ��ǰ����CS��
        SceneManager.LoadScene(CurrentSceneNumber);                         //��ȡ���CS�ı�����
    }

    void LostCollider()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;//�л�����һ�¾�Ϊ�෴״̬��
        } 
    }
        void Cheater()
        {
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log("cheater");
                LoadNextLevel();
            }
        }

        void LoadNextLevel()
        {
            finsoundStart();

            int NextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (NextLevel == SceneManager.sceneCountInBuildSettings)
            {
                NextLevel = 0;
            }
            SceneManager.LoadScene(NextLevel);
        }
        void GoNextLevel()
        {
            SuccessP.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", delayTime);

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

