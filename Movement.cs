using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float upPower = 0f;
    [SerializeField] float RotatePower = 0f;
    Rigidbody rBody; //这个变量是刚体类型，所以为Rigibody 为前缀，后面的rBody是这个变量的名字。
    AudioSource audioSource;

    [SerializeField]AudioClip MainRocketSound;
    [SerializeField] ParticleSystem boostL;
    [SerializeField] ParticleSystem boostM;
    [SerializeField] ParticleSystem boostR;
    void Start()
    {  rBody =  GetComponent<Rigidbody>();//把刚体赋值给rBody.因为GetComponent<>这里调用了一个方法，所以后面要加括号。
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();//赋予火箭推力
        ProcessRotation();//赋予旋转力
    }
    private void OnApplicationQuit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
        }
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            SpaceAddForce();
        }
        else
        {
            SpaceAddForceStop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else { StopRotating(); }//else if 后面竟然还可以接着写个else.无敌，无敌。
    }
    void SpaceAddForceStop()
    {
        boostM.Stop();
        audioSource.Stop();
    }
   
    void StopRotating()
    {
        boostR.Stop(); boostL.Stop();
    }
    void SpaceAddForce()
    {
        rBody.AddRelativeForce(Vector3.up * upPower * Time.deltaTime);
        if (!audioSource.isPlaying)//注意，这里不能在后面加括号因为不能像方法一样调用。
        {
            audioSource.PlayOneShot(MainRocketSound);
        }
        if (!boostM.isPlaying)
        { boostM.Play(); }//这句反向判断很重要，中间粒子再没有播放的时候，播放。强制执行space的命令。
    }



    void RotateRight()
    {
        if (!boostL.isPlaying)
        { boostL.Play(); }
        GiveRotate(-RotatePower);    //所以这里相当于得到的是RotatePower 的值，能够参与计算。
    }

    void RotateLeft()
    {
        if (!boostR.isPlaying)
        { boostR.Play(); }
        GiveRotate(RotatePower);//注意，这个参数的传递，是因为GiveRotate()方法里，()里面建立了float rotatePush这个值，所以才能引用之前的变量。
    }

    void GiveRotate(float rotatePush)//这个代码是重构的，如果发现了有重复的，命令相似的代码，可以用重构，
                                             //用鼠标全选想重构的代码段，然后右键第一个，就可以重构。
                                             //重构的代码判断。重构方法的括号里可以直接建立新参数，并且针对这个参数，进行调整。
    {
        rBody.freezeRotation = true;//冻结旋转，所以我们能够进行手动旋转。这个主要是为了避免碰撞发生BUG。如果没有这个冻结，则
                                    //小火箭撞到CUBE时会发生BUG，unity会无法判断到底往哪边旋转。
        transform.Rotate(Vector3.forward * rotatePush * Time.deltaTime);//注意看这里，rotatePush这里，就是针对GiveRotate.
        rBody.freezeRotation = false;//解冻旋转，所以物理系统接管。
    }
}
//老师吧代码弄的非常干净，整洁，哪怕只有几行字，他也要把他抽象出来，因为这样的话，好多年以后，再看这个代码，都能很快明白其中的意思。不会重新一段段的去读。
//一定不要怕麻烦