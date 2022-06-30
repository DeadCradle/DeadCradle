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
        if (IsTransitioning||collisionDisable) { return; }//如果IsTransitioning为真，则返回。否则则执行后面的内容。加一个collisonDisable免得
        switch (other.gameObject.tag)//如果物体接触到的TAG是什么，自动检测。
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
        particleS = GetComponent<ParticleSystem>();        //千万不要忘记这一步。我总是忘记这一步。这一步很重要，要把组件内的内容反过来赋值给
                                                           //这个asourse.之前的声明只是证明他是这个类别，但是是哪个地方调用这个类别没有给出，所以
                                                           //必须这里要给一次。
    }
    void Update()
    {
        Cheater();
        LostCollider();

    }

    void RocketCrash()
    {
        RocketExploreSound();
        FailP.Play();                                          //格式是： particalSystem.Play(),因为前面已经定义了FailP是ParticalSystem，所以直接使用粒子名字即可。
        GetComponent<Movement>().enabled = false;           //这里的意思是在这个object中找到Movement这个组件，然后调用其中的enable属性，最后让这个属性变为false。
        Invoke("ReLoadLevel", delayTime);                   //Invoke，后面1f代表延迟1秒。Invoke后面要输入“”加方法的名字，这个是个强引用，不好维护。    

    }

    void ReLoadLevel()
    {

        int CurrentSceneNumber = SceneManager.GetActiveScene().buildIndex;//这个非常重要，使用场景管理器，
                                                                          //读取当前场景的index值。把这个index值设置为当前变量CS。
        SceneManager.LoadScene(CurrentSceneNumber);                         //读取这个CS的变量。
    }

    void LostCollider()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;//切换。按一下就为相反状态。
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

