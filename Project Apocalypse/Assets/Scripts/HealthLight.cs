using UnityEngine;

public class HealthLight : MonoBehaviour
{
    public float power;
    public float powerFactor;
    public float healthFactor;
    public float despawnTime = 10f;

    void Awake(){
        Destroy(gameObject, despawnTime);
        powerFactor = 0.5f;
        healthFactor = 0.3f;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<PlayerHealth>().Health += power * healthFactor;
            col.gameObject.GetComponent<PlayerPower>().Power += power * powerFactor;
            Destroy(gameObject);
        }
    }

}
