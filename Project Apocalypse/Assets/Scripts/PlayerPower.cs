using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerPower : MonoBehaviour
{
    public float maxMaxPower = 500f, minMaxPower = 100f;
    public float MaxPower = 100;
    public float Power;
    public float powerIncreaseRate = 10f;
    public bool Regenerate = true;
    public bool canShoot = true;
    private bool prevCanShoot = true;
    public float powerShootThreshold = 10f;
    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    void Start()
    {
        MaxPower = Mathf.Clamp(MaxPower, 0, maxMaxPower);
        Power = MaxPower;
    }
    void Update()
    {
        Power = Mathf.Clamp(Power, 0, MaxPower);
        if(Regenerate) Power += powerIncreaseRate * Time.deltaTime;

        if(Power > powerShootThreshold){
            canShoot = true;
            prevCanShoot = true;
        }
        else{
            if(prevCanShoot){
                canShoot = true;
            }else canShoot = false;
        }

        if(Power < 1f) prevCanShoot = false;
    }
}
