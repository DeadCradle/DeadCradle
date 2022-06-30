using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float upPower = 0f;
    [SerializeField] float RotatePower = 0f;
    Rigidbody rBody; //��������Ǹ������ͣ�����ΪRigibody Ϊǰ׺�������rBody��������������֡�
    AudioSource audioSource;

    [SerializeField]AudioClip MainRocketSound;
    [SerializeField] ParticleSystem boostL;
    [SerializeField] ParticleSystem boostM;
    [SerializeField] ParticleSystem boostR;
    void Start()
    {  rBody =  GetComponent<Rigidbody>();//�Ѹ��帳ֵ��rBody.��ΪGetComponent<>���������һ�����������Ժ���Ҫ�����š�
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();//����������
        ProcessRotation();//������ת��
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
        else { StopRotating(); }//else if ���澹Ȼ�����Խ���д��else.�޵У��޵С�
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
        if (!audioSource.isPlaying)//ע�⣬���ﲻ���ں����������Ϊ�����񷽷�һ�����á�
        {
            audioSource.PlayOneShot(MainRocketSound);
        }
        if (!boostM.isPlaying)
        { boostM.Play(); }//��䷴���жϺ���Ҫ���м�������û�в��ŵ�ʱ�򣬲��š�ǿ��ִ��space�����
    }



    void RotateRight()
    {
        if (!boostL.isPlaying)
        { boostL.Play(); }
        GiveRotate(-RotatePower);    //���������൱�ڵõ�����RotatePower ��ֵ���ܹ�������㡣
    }

    void RotateLeft()
    {
        if (!boostR.isPlaying)
        { boostR.Play(); }
        GiveRotate(RotatePower);//ע�⣬��������Ĵ��ݣ�����ΪGiveRotate()�����()���潨����float rotatePush���ֵ�����Բ�������֮ǰ�ı�����
    }

    void GiveRotate(float rotatePush)//����������ع��ģ�������������ظ��ģ��������ƵĴ��룬�������ع���
                                             //�����ȫѡ���ع��Ĵ���Σ�Ȼ���Ҽ���һ�����Ϳ����ع���
                                             //�ع��Ĵ����жϡ��ع����������������ֱ�ӽ����²������������������������е�����
    {
        rBody.freezeRotation = true;//������ת�����������ܹ������ֶ���ת�������Ҫ��Ϊ�˱�����ײ����BUG�����û��������ᣬ��
                                    //С���ײ��CUBEʱ�ᷢ��BUG��unity���޷��жϵ������ı���ת��
        transform.Rotate(Vector3.forward * rotatePush * Time.deltaTime);//ע�⿴���rotatePush����������GiveRotate.
        rBody.freezeRotation = false;//�ⶳ��ת����������ϵͳ�ӹܡ�
    }
}
//��ʦ�ɴ���Ū�ķǳ��ɾ������࣬����ֻ�м����֣���ҲҪ���������������Ϊ�����Ļ����ö����Ժ��ٿ�������룬���ܺܿ��������е���˼����������һ�ζε�ȥ����
//һ����Ҫ���鷳