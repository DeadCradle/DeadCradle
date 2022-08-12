using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float XMove, YMove;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    [Header("���ڷɴ�������")] 
    [Tooltip("�ɴ��ٶ�")]
    [SerializeField] float ShipSpeed = 1f;
    [Tooltip("X����Լ��")]
    [SerializeField] float ClampXRange = 9f;
    [Tooltip("Y����Լ��")]
    [SerializeField] float ClampYRange = 4f;
    [Tooltip("Y����ƫ��ֵ")]

    [SerializeField] float positionPitchYFactor = 0f;
    [Tooltip("X��λ��ƫ��ֵ")]
    [SerializeField] float positionPitchXFactor = 0f;
    [Tooltip("Y�����ֵ")]

    [Header("�ɴ�����ֵ")]
    [SerializeField] float YmoveControllerFactor = 0f;
    [Tooltip("X�����ֵ")]
    [SerializeField] float XmoveControllerFactor = 0f;
    [Tooltip("������������")]
    [SerializeField] GameObject[] Lasers;
    // Update is called once per frame
 
    void Update()
    {
        ProcessTranslate();

        ProcessRotate();

        ProcessFire();
    }

    void ProcessRotate()
    {
        float DuetoPositionY = transform.localPosition.y * positionPitchYFactor;
        float DuetoPositionX = transform.localPosition.x * positionPitchXFactor;
        float DuetoControllerY = YMove * YmoveControllerFactor;
        float DuetoControllerX = XMove * XmoveControllerFactor;
        float pitch = DuetoPositionY + DuetoControllerY;//��һ���֣����ƶ�λ�ú���ת�й�ϵ���ڶ����֣��������������localposition�й�ϵ��
        float yaw = DuetoPositionX;
        float roll = DuetoControllerX;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslate()
    {
        XMove = Input.GetAxis("Horizontal");

        YMove = Input.GetAxis("Vertical");

        float TouchMovingX = XMove * Time.deltaTime * ShipSpeed;

        float newPosX = transform.localPosition.x + TouchMovingX;

        float TouchMovingY = YMove * Time.deltaTime * ShipSpeed;

        float clampedX = Mathf.Clamp(newPosX, -ClampXRange, ClampXRange);//Լ��X
        float newPosY = transform.localPosition.y + TouchMovingY;

        float clampedY = Mathf.Clamp(newPosY, -ClampYRange, ClampYRange);//Լ��Y�����忴unity�ĵ���

        transform.localPosition = new Vector3(
        clampedX,
        clampedY,
        transform.localPosition.z
        );
    }

    void ProcessFire()
    {

        if (Input.GetButton("Fire1"))
        {
            LaserShoot(true);
        }
        else
        {
            LaserShoot(false);
        }
    }

    void LaserShoot(bool IsShoot)
    {
        foreach (GameObject laserInF in Lasers)
        {
            var laserInFPartical = laserInF.GetComponent<ParticleSystem>().emission;
            laserInFPartical.enabled = IsShoot;


        }
    }





}

/*   void ProcessFire()
   {
       float ShipFire = Input.GetAxis("Fire1");
       if (ShipFire == 1)
       {
           Debug.Log("shoot");
       }
       else
       {
           Debug.Log("noShoot");
       }
   } 
}*/
