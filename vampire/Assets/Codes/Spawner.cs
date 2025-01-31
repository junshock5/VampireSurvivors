using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    float timer;
    
    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.8f)
        {
            GameManager.instance.pool.Get(1);
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;

    }
}
