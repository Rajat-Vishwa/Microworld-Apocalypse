using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraBehaviour : MonoBehaviour
{

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public CinemachineVirtualCamera cam;
    public Rigidbody2D playerRB;
    public float minLens = 7f, maxLens = 11f;
    public float minSqrVel = 0f, maxSqrVel = 250f;
    public float lerpConstant = 10f;
    public float currentSize = 7f;

    void Start()
    {
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void LateUpdate()
    {
        float sqrVel = playerRB.velocity.sqrMagnitude;
        float targetSize = Map(sqrVel, minSqrVel, maxSqrVel, minLens, maxLens);

        targetSize = Mathf.Clamp(targetSize, minLens, maxLens);

        currentSize = Mathf.Lerp(currentSize, targetSize, lerpConstant* Time.deltaTime);
        cam.m_Lens.OrthographicSize = currentSize;
        
    }

}
