using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float upPower = 0f;
    Rigidbody rBody; //��������Ǹ������ͣ�����ΪRigibody Ϊǰ׺�������rBody��������������֡�
    AudioSource audioSource;

    [SerializeField]AudioClip MainRocketSound;


    [SerializeField] float RotatePower = 0f;
    void Start()
    {
       rBody =  GetComponent<Rigidbody>();//�Ѹ��帳ֵ��rBody.��ΪGetComponent<>���������һ�����������Ժ���Ҫ�����š�
       audioSource = GetComponent<AudioSource>();
    

    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }
    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            rBody.AddRelativeForce(Vector3.up * upPower * Time.deltaTime);
            if (!audioSource.isPlaying)//ע�⣬���ﲻ���ں����������Ϊ�����񷽷�һ�����á�
            {
                audioSource.PlayOneShot(MainRocketSound);
            }


        }
        else {
            audioSource.Stop();
        }
    }
    void ProcessRotation() {

        if (Input.GetKey(KeyCode.A))
        {
            GiveRotate(RotatePower);//ע�⣬��������Ĵ��ݣ�����ΪGiveRotate()�����()���潨����float rotatePush���ֵ�����Բ�������֮ǰ�ı�����
        
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GiveRotate(-RotatePower);    //���������൱�ڵõ�����RotatePower ��ֵ���ܹ�������㡣
          
        }

    }

    private void GiveRotate(float rotatePush)//����������ع��ģ�������������ظ��ģ��������ƵĴ��룬�������ع���
                                             //�����ȫѡ���ع��Ĵ���Σ�Ȼ���Ҽ���һ�����Ϳ����ع���
                                             //�ع��Ĵ����жϡ��ع����������������ֱ�ӽ����²������������������������е�����
                                             

    {
        rBody.freezeRotation = true;//������ת�����������ܹ������ֶ���ת�������Ҫ��Ϊ�˱�����ײ����BUG�����û��������ᣬ��
                                    //С���ײ��CUBEʱ�ᷢ��BUG��unity���޷��жϵ������ı���ת��
      
        transform.Rotate(Vector3.forward * rotatePush * Time.deltaTime);//ע�⿴���rotatePush����������GiveRotate.
        rBody.freezeRotation = false;//�ⶳ��ת����������ϵͳ�ӹܡ�
    }


}
