using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    Vector3 startingPosition;               //开始位置，这个变量不需要动，所以不需要序列化，serialzeField;
    [SerializeField] Vector3 movementVector;                 //运动向量
    [SerializeField][Range(0,1)] float movmentFactor;              //运动因子（我不太理解这个意思。往后看才能理解。）后来老师表示自己写错了，运动因子应该是一个浮点数。确实。有一个基础的VECTOR3和一个结束时候的vector3是够了的。
    [SerializeField]float period = 2f;//这个周期的值
    void Start()
    {
        startingPosition = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {

        //这里很重要的一点，老师给这个0加了一个F，代表他是浮点数，因为period是浮点数。两个浮点数之间会有微妙的差异。所以要用浮点数进行比较，否则会出现一些额外的未知问题。   
        //if (period == 0f) { return; }



        //最终优化，使用了这个Mathf。Epsilon。这个代表unity中最小最小的数。所以他是可以比0更精确的。
        if (period == Mathf.Epsilon) { return; }

        //其实给周期加个range也可以解决这个问题。如果周期为0则直接返回。也可以解决这个问题。返回则不执行之后的命令了。
        float cycles = Time.time / period;  //时间除以period为一个循环周期。
        const float tau = Mathf.PI * 2;//锁定这个值。
        float rawSinWave = Mathf.Sin(cycles * tau);   //核心公式，这个公式可以让物件不断循环来来回回。

        movmentFactor = (rawSinWave + 1f)/2f; //这个是为了保持值一直是正数。因为factor的值不能为负数。而公式产生的值是会有负数的。
        Vector3 offset = movementVector * movmentFactor;
        transform.position = startingPosition + offset;
         
    }
}
