using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float healthValue = 100;
    public characterType badGuy;

    private void OnCollisionEnter(Collision collided)
    {
        deducted(collided.collider.gameObject.GetComponent<undermine>());
    }
    private void OnTriggerEnter(Collider collided)
    {
        deducted(collided.gameObject.GetComponent<undermine>());
    }
    void deducted(undermine und)
    {
        if (und != null && und.chara == badGuy && und.attacking)
        {
            healthValue -= und.undermineValue;
            print(healthValue);
        }
    }
}
