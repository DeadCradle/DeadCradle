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
    [Header("关于飞船的属性")] 
    [Tooltip("飞船速度")]
    [SerializeField] float ShipSpeed = 1f;
    [Tooltip("X距离约束")]
    [SerializeField] float ClampXRange = 9f;
    [Tooltip("Y距离约束")]
    [SerializeField] float ClampYRange = 4f;
    [Tooltip("Y轴置偏移值")]

    [SerializeField] float positionPitchYFactor = 0f;
    [Tooltip("X轴位置偏移值")]
    [SerializeField] float positionPitchXFactor = 0f;
    [Tooltip("Y轴控制值")]

    [Header("飞船控制值")]
    [SerializeField] float YmoveControllerFactor = 0f;
    [Tooltip("X轴控制值")]
    [SerializeField] float XmoveControllerFactor = 0f;
    [Tooltip("激光序列数组")]
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
        float pitch = DuetoPositionY + DuetoControllerY;//第一部分，跟移动位置和旋转有关系，第二部分，跟操作的物体的localposition有关系。
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

        float clampedX = Mathf.Clamp(newPosX, -ClampXRange, ClampXRange);//约束X
        float newPosY = transform.localPosition.y + TouchMovingY;

        float clampedY = Mathf.Clamp(newPosY, -ClampYRange, ClampYRange);//约束Y。具体看unity文档。

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
