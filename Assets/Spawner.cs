using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private GameObject coinPrefab;
    public float obstacleSpawnTime= 2f;
    [Range(0,1)] public float obstacleSpawnTimeFactor= 0.4f;
    public float obstacleSpeed = 3f;
    [Range(0,1)] public float obstacleSpeedFactor= 0.5f;



    private float _obstacleSpawnTime;
    private float _obstacleSpeed;
    
    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private float timeSinceLastCoinSpawn;
    public float coinSpawnInterval = 60f; 



    private void Start(){
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }

     private void Update()
    {
    if (GameManager.Instance.isPlaying)
    {
        timeAlive += Time.deltaTime;
        timeSinceLastCoinSpawn += Time.deltaTime;
        CalculateFactors();
        SpawnLoop();
    }
    }

    private void SpawnLoop(){
        timeUntilObstacleSpawn+= Time.deltaTime;
        if(timeUntilObstacleSpawn>= _obstacleSpawnTime){
            Spawn();
            timeUntilObstacleSpawn=0f;
        }
    }


    private void ClearObstacles(){
        foreach(Transform child in obstacleParent){
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactors(){
         _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
         _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
   
    }

    private void ResetFactors(){
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed= obstacleSpeed;
    }

   private void Spawn()
    {
    
    GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
    spawnedObstacle.transform.parent = obstacleParent;

    Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
    obstacleRB.velocity = Vector2.left * _obstacleSpeed;

    
    if (timeSinceLastCoinSpawn >= coinSpawnInterval)
    {
        
        Vector3 coinPosition = transform.position;
        coinPosition.y += Random.Range(-1f, 1f); 

        
        GameObject spawnedCoin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        spawnedCoin.transform.parent = obstacleParent;

        Rigidbody2D coinRB = spawnedCoin.GetComponent<Rigidbody2D>();
        coinRB.velocity = Vector2.left * _obstacleSpeed;

        
        timeSinceLastCoinSpawn = 0f;
    }
    }



}
