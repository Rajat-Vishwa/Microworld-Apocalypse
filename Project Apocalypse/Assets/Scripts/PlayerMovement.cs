using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    public float forceScale = 1000f;
    public Transform PlayerTransform;
    public Rigidbody2D rb;
    private Vector2 lookDir;
    private Vector3 inputRaw, force;
    private Vector3 old_mouse_position = Vector3.zero;
    public float coolDownTime = 1f, CoolDownTimer = 0;
    public float lerpTime = 10f;
    public ShootLaser laserScript;
    void Update()
    {  
        inputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        force = inputRaw * forceScale * Time.deltaTime;
        rb.AddForce(force);

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.mousePosition == old_mouse_position && !Input.GetMouseButton(0)){
            if(CoolDownTimer == 0) lookDir = Vector2.Lerp(lookDir, rb.velocity.normalized, lerpTime * Time.deltaTime);
            else lookDir = Vector2.Lerp(lookDir, (mouseWorldPosition - PlayerTransform.position).normalized, lerpTime * Time.deltaTime);
        }else{
            lookDir = Vector2.Lerp(lookDir, (mouseWorldPosition - PlayerTransform.position).normalized, lerpTime * Time.deltaTime);
            CoolDownTimer = coolDownTime;
        }
        
        PlayerTransform.Rotate(new Vector3(0, 0, Vector3.SignedAngle(PlayerTransform.up,lookDir, new Vector3(0,0,1))));
        if(Input.GetMouseButton(0)){
            laserScript.shoot = true;
            laserScript.lookDir = lookDir;
        }else{
            laserScript.shoot = false;
        }

        old_mouse_position = Input.mousePosition;
        if(CoolDownTimer > 0) CoolDownTimer -= Time.deltaTime;
        else CoolDownTimer = 0;
    }

}
