using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 100f; // 每秒旋转的角度
    void Start()
    {
        
    }

    void Update()
    {
        // 沿自身 Y 轴（Local Y）旋转
        // 使用 Time.deltaTime 确保在不同帧率下旋转速度一致
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
