using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType{fist, weapon};
public enum characterType{protogonist, enemy};

public class undermine : MonoBehaviour
{
    public weaponType weap;
    public characterType chara;
    public float undermineValue;
    public bool attacking = false;
    GameObject temp;
    Vector3 point;
    public ParticleSystem ps;

    public void startAttacking(){
        attacking = true;
    }
    public void endAttacking(){
        attacking = false;
    }


    private void OnCollisionEnter(Collision collided){
        temp = collided.gameObject;
        point = collided.contacts[0].point;
        if(attacking){
            playParticle();
        }
    }
    private void OnTriggerEnter(Collider collided){
        temp = collided.gameObject;
        point = collided.bounds.ClosestPoint(transform.position);
        if(attacking){
            playParticle();
        }

    }

    void playParticle(){
        Instantiate(ps, point, Quaternion.Euler(Vector3.zero), temp.transform);
    }
}
