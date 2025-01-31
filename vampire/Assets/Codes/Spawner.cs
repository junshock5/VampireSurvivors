using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    float timer;
    
    void Awake()
    {
        spawnPoints = GETComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.2f)
        {
            GameManager.instance.pool.Get(1);
            timer = 0;
            Spawn();
        }
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     GameManager.instance.pool.Get(0);   
        // }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

    }
}
