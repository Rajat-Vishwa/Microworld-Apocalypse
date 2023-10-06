using UnityEngine;
using Pathfinding;
public class ZombieBehaviour : MonoBehaviour
{
    public Transform playerTransform, spriteTransform;
    public ZombieDash dashScript;
    public Rigidbody2D rb;
    public AIPath AIScript;
    public AIDestinationSetter destinationScript;
    public float DistanceThreshold = 100f;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dashScript = gameObject.GetComponent<ZombieDash>();
        AIScript = gameObject.GetComponent<AIPath>();
        destinationScript = gameObject.GetComponent<AIDestinationSetter>();
        AIScript.endReachedDistance = DistanceThreshold;
        destinationScript.target = playerTransform;
    }

    void Update()
    {
        Vector3 target = playerTransform.position - transform.position;
        //Debug.Log(target);
        float sqrDist = Vector3.SqrMagnitude(target);
        //spriteTransform.Rotate(new Vector3(0, 0, Vector3.SignedAngle(spriteTransform.up, target, new Vector3(0,0,1))));
        spriteTransform.up = target.normalized;

        if(sqrDist <= Mathf.Pow(DistanceThreshold,2)){

            Vector3 vel = AIScript.velocity;
            AIScript.enabled = false;
            if(!dashScript.enabled) rb.velocity = vel;
            dashScript.enabled = true;

        }else if(sqrDist >= Mathf.Pow(DistanceThreshold,2) * 1.20f){
            dashScript.enabled = false;
            if(rb.velocity.sqrMagnitude <= Mathf.Pow(AIScript.maxSpeed,2)){
                AIScript.enabled = true;
            }else{
                rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity.normalized * AIScript.maxSpeed, 0.1f*Time.deltaTime);
            }
        }else{
            
        }

    }
}
