using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_animator : MonoBehaviour
{
    // 目標遊戲物件
    public GameObject target;
    
    // 導航網格代理 (負責移動)
    NavMeshAgent nav;
    
    // 動畫控制器
    Animator ani;
    
    // 血量控制器
    health hp;

    // 最大加速度
    public float maxAccer = 5f;

    // 記錄最後更新時間 (用於巡邏)
    float lastTime;

    // 初始化函式 (在遊戲開始時執行)
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        hp = GetComponent<health>();
        lastTime = Time.time - 4;
    }

    // 清理角色的協程 (在角色死亡後延遲刪除)
    IEnumerator clearCharacter()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    // 每幀更新函式
    void Update()
    {
        // 檢查角色是否死亡
        /*if (hp.healthValue <= 0)
        {
            ani.SetBool("death", true);
            nav.enabled = false;
            this.enabled = false;
            StartCoroutine(clearCharacter());
        }*/

        // 計算當前速度與目標距離
        float v = nav.velocity.magnitude;
        float dist = Vector3.Distance(target.transform.position, transform.position);

        // 如果距離目標超出停止距離，並且未處於 "underAttacked" 動畫狀態
        if (dist > nav.stoppingDistance && !ani.GetCurrentAnimatorStateInfo(0).IsName("underAttacked"))
        {
            // 如果距離超過 10，開始巡邏
            if (dist > 10)
            {
                partol();
            }
            else
            {
                // 否則設置目標位置為導航目的地
                nav.SetDestination(target.transform.position);
            }
        }
        else
        {
            // 停止移動
            nav.velocity = Vector3.zero;
        }

        // 計算導航目標的相對方向
        Vector3 d = Quaternion.Inverse(transform.rotation) * nav.desiredVelocity;
        float m = Mathf.Atan2(d.x, d.z) / Mathf.PI;

        // 設置動畫參數
        ani.SetFloat("vertical", v);
        ani.SetFloat("horizontal", m);

        // 如果距離小於 2.0f，進行攻擊
        if (dist < 2.0f)
        {
            transform.LookAt(target.transform);
            BroadcastMessage("startAttacking");
        }
        else
        {
            ani.SetBool("attack", false);
            BroadcastMessage("endAttacking");
        }
    }

    // 被攻擊時的處理函式
    public void Hit()
    {
        // 預留函式 (目前尚未實現具體邏輯)
    }

    // 碰撞事件觸發函式
    /*private void OnCollisionEnter(Collision collision)
    {
        // 計算碰撞點的相對方向
        Vector3 relativePoint = collision.GetContact(0).point - transform.position;
        float angular = Mathf.Atan2(relativePoint.x, relativePoint.z) / Mathf.PI * 180;

        // 設置動畫觸發器與方向參數
        ani.SetTrigger("underAttacked");
        ani.SetFloat("angular", angular);
    }*/

    // 巡邏邏輯函式
    void partol()
    {
        // 每隔 5 秒更新一次隨機目標位置
        if (lastTime + 5 < Time.time)
        {
            float newX = transform.position.x + Random.Range(-10, 10);
            float newY = transform.position.y;
            float newZ = transform.position.z + Random.Range(-10, 10);

            Vector3 newPosition = new Vector3(newX, newY, newZ);
            nav.SetDestination(newPosition);

            lastTime = Time.time;
        }
    }
}
