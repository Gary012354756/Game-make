using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject ob; // 要追蹤的物體
    public Vector3 offset = new Vector3(0, -0.1f, 0); // 調整後的攝影機偏移量

    // Start is called before the first frame update
    void Start()
    {
        // 初始化攝影機位置，應用偏移量 
        transform.position = ob.transform.position + offset;
        transform.rotation = ob.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // 目標位置和旋轉角度
        Vector3 targetPosition = ob.transform.position + offset;
        Quaternion targetRotation = ob.transform.rotation;

        // 線性插值移動攝影機
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 4);

        // 使用 Quaternion.Lerp 平滑旋轉攝影機
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 4);
    }
}
