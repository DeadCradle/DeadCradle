using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    Vector3 startingPosition;               //��ʼλ�ã������������Ҫ�������Բ���Ҫ���л���serialzeField;
    [SerializeField] Vector3 movementVector;                 //�˶�����
    [SerializeField][Range(0,1)] float movmentFactor;              //�˶����ӣ��Ҳ�̫��������˼�����󿴲�����⡣��������ʦ��ʾ�Լ�д���ˣ��˶�����Ӧ����һ����������ȷʵ����һ��������VECTOR3��һ������ʱ���vector3�ǹ��˵ġ�
    [SerializeField]float period = 2f;//������ڵ�ֵ
    void Start()
    {
        startingPosition = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {

        //�������Ҫ��һ�㣬��ʦ�����0����һ��F���������Ǹ���������Ϊperiod�Ǹ�����������������֮�����΢��Ĳ��졣����Ҫ�ø��������бȽϣ���������һЩ�����δ֪���⡣   
        //if (period == 0f) { return; }



        //�����Ż���ʹ�������Mathf��Epsilon���������unity����С��С�������������ǿ��Ա�0����ȷ�ġ�
        if (period == Mathf.Epsilon) { return; }

        //��ʵ�����ڼӸ�rangeҲ���Խ��������⡣�������Ϊ0��ֱ�ӷ��ء�Ҳ���Խ��������⡣������ִ��֮��������ˡ�
        float cycles = Time.time / period;  //ʱ�����periodΪһ��ѭ�����ڡ�
        const float tau = Mathf.PI * 2;//�������ֵ��
        float rawSinWave = Mathf.Sin(cycles * tau);   //���Ĺ�ʽ�������ʽ�������������ѭ�������ػء�

        movmentFactor = (rawSinWave + 1f)/2f; //�����Ϊ�˱���ֵһֱ����������Ϊfactor��ֵ����Ϊ����������ʽ������ֵ�ǻ��и����ġ�
        Vector3 offset = movementVector * movmentFactor;
        transform.position = startingPosition + offset;
         
    }
}
