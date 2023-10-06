using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{

    public float maxMaxHealth = 500f, minMaxHealth = 100f;
    public float MaxHealth = 100;
    public float minIntensity = 0.8f, maxIntensity = 1.55f;
    public float Health;
    private Light2D playerLight;
    public PauseManager managerScript;

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    void Start()
    {
        MaxHealth = Mathf.Clamp(MaxHealth, 0, maxMaxHealth);
        Health = MaxHealth;
        playerLight = gameObject.GetComponentInChildren<Light2D>();
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseManager>();
    }
    void Update()
    {
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        playerLight.intensity = Map(Health, 0, MaxHealth, minIntensity, maxIntensity);

        if(Health <= 0){
            managerScript.GameOver();
        }
    }
}
