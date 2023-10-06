using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDash : MonoBehaviour
{
    public float forceScale = 800f;
    public float maxVel;
    public Transform player;
    public Rigidbody2D rb;
    private Vector3 force;
    public float Damage = 20f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {  
        if(rb.velocity.sqrMagnitude < Mathf.Pow(maxVel,2)){
            Vector3 targetDir = player.position - transform.position;
            force = targetDir * forceScale * Time.deltaTime;
            rb.AddForce(force);
        }
    }

    void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            PlayerHealth healthScript = col.gameObject.GetComponent<PlayerHealth>();
            healthScript.Health -= Damage * Time.deltaTime;
        }
    }

}
