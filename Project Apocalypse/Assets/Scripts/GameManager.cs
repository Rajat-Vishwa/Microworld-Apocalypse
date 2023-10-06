using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    public int CurrentScore;
    public Transform[] spawnPoints;
    private int NumSpawnPoint;
    private int zombieCount = 4;
    private int lastScore = 0;
    public int healthBonus = 20;
    public float addThreshold = 50;
    public float addThresholdIncrementIndex = 0.25f;
    public int maxZombieCount = 50;
    public float maxCoolDownTime = 10;
    private float coolDownTime = 0;
    [SerializeField]
    private int currentZombies = 0;
    public GameObject[] ZombiePrefabs;
    public List<GameObject> ZombiesAlive = new List<GameObject>();

    public TMP_Text scoreText;

    void Start()
    {
        Time.timeScale = 1;
        Player = GameObject.FindGameObjectWithTag("Player"); 
        NumSpawnPoint = spawnPoints.Length;
        CurrentScore = 0;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedZombie = Instantiate(ZombiePrefabs[Random.Range(0,ZombiePrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        ZombiesAlive.Add(spawnedZombie);
        coolDownTime = maxCoolDownTime;
    }

    void FixedUpdate()
    {  
        Mathf.Clamp(zombieCount, 0, maxZombieCount);
        if(CurrentScore - lastScore > addThreshold){
            zombieCount++;
            lastScore = CurrentScore;
            addThreshold += addThresholdIncrementIndex * addThreshold;
            Player.GetComponent<PlayerHealth>().MaxHealth += healthBonus;
            Player.GetComponent<PlayerPower>().MaxPower += healthBonus;
        }

        currentZombies = ZombiesAlive.Count;

        if(currentZombies < zombieCount){
            for(int i=0; i< zombieCount - currentZombies; i++){
                if(coolDownTime <= 0){
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    GameObject spawnedZombie = Instantiate(ZombiePrefabs[Random.Range(0,ZombiePrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
                    ZombiesAlive.Add(spawnedZombie);
                    coolDownTime = maxCoolDownTime;
                }
            }
        }

        scoreText.text = "Score : " + CurrentScore.ToString();

        if(coolDownTime > 0) coolDownTime -= Time.fixedDeltaTime;
    }
}
