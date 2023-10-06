using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class ShootLaser : MonoBehaviour
{
    public VolumetricLineBehavior laser1, laser2;
    public Transform laserTransform1, laserTransform2;
    public float MaxDist = 100f;
    public bool shoot = false;
    public float damage = 1f;
    public Vector2 lookDir;

    public PlayerPower powerScript;
    public float powerDeclineRate = 10f;

    void Start(){
        laserTransform1 = laser1.gameObject.GetComponent<Transform>();
        laserTransform2 = laser2.gameObject.GetComponent<Transform>();
        powerScript = gameObject.GetComponentInParent<PlayerPower>();
    }
    void FixedUpdate()
    {  
        if(shoot && powerScript.canShoot){
            laser1.gameObject.SetActive(true);
            laser2.gameObject.SetActive(true);
            Shoot();
            powerScript.Power -= powerDeclineRate * Time.deltaTime;
            powerScript.Regenerate = false;
            }else{
            laser1.gameObject.SetActive(false);
            laser2.gameObject.SetActive(false);
            powerScript.Regenerate = true;
        }
    }

    public void Shoot(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir, MaxDist);
        
        if(hit.collider != null){
            Vector3 targetPosition = hit.point;
            laser1.StartPos = Vector3.zero;
            laser2.StartPos = Vector3.zero;
            laser1.EndPos = laserTransform1.InverseTransformPoint(targetPosition);
            laser2.EndPos = laserTransform2.InverseTransformPoint(targetPosition);

            if(hit.collider.tag == "Zombie"){
                ZombieHealth zombieHealthScript = hit.collider.gameObject.GetComponent<ZombieHealth>();
                zombieHealthScript.Health -= damage * Time.fixedDeltaTime;
            }
        }else{
            laser1.EndPos = laserTransform1.InverseTransformPoint(transform.up * MaxDist);
            laser2.EndPos = laserTransform2.InverseTransformPoint(transform.up *MaxDist);
        }
    }
}
