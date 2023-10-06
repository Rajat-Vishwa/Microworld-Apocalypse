using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ZombieHealth : MonoBehaviour
{
    public float DieTime = 10f;
    public float MaxHealth = 100f;
    public float Health;
    public float scoreTransferFactor;
    public float healthTransferFactor = 0.5f;
    public float scaleThreshold = 0.3f;
    public GameObject healthLight;
    private Light2D spriteLight;
    public GameManager gameManager;
    
    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    void Awake()
    {
        Health = MaxHealth;
        spriteLight = gameObject.GetComponentInChildren<Light2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreTransferFactor = 0.3f;
        scaleThreshold = 0.3f;
    }

    void Update()
    {
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        spriteLight.intensity = Map(Health, 0, MaxHealth, 0.5f, 0.75f);
        if(Health <= 0){
            Die();
        }        
    }

    private void Die(){
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, DieTime * Time.deltaTime);
        if(transform.localScale.sqrMagnitude <= scaleThreshold){
            GameObject hLight = Instantiate(healthLight, transform.position, transform.rotation);
            hLight.GetComponent<HealthLight>().power = MaxHealth * healthTransferFactor;
            gameManager.CurrentScore += (int) (MaxHealth * scoreTransferFactor);
            gameManager.ZombiesAlive.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
