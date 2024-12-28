using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_animator : MonoBehaviour
{
    Animator cc;
    public GameObject spe, pos;
    GameObject QQ;
    public AudioClip music;
    public GameObject ball;


    // 初始化
    void Start()
    {
        cc = GetComponent<Animator>();
    }

    // 每一幀更新一次
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2; // 按住Shift鍵加速
        }

        // 攻擊動畫
        /*if (Input.GetMouseButton(0))
        {
            cc.SetBool("attack", true); // 設定攻擊動畫
        }
        else
        {
            cc.SetBool("attack", false);
        }*/

        float horizontal = Input.GetAxis("Horizontal");
        cc.SetFloat("vertical", vertical);
        cc.SetFloat("horizontal", horizontal);

    }

    /*public void Hit()
    {
        QQ = Instantiate(spe, pos.transform); // 生成特效
        AudioSource.PlayClipAtPoint(music, pos.transform.position, 1);
        GameObject PP = Instantiate(ball, pos.transform.position, Quaternion.Euler(0, 0, 0));
        PP.GetComponent<Rigidbody>().AddForce(transform.forward*500);
    }

    public void HitOver()
    {
        Destroy(QQ); // 銷毀特效
    }*/


}
