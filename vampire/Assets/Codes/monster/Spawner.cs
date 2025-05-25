using UnityEngine;

public class Spawner : MonoBehaviour
{
    int level;
    public Transform[] spawnPoints;
    public SpawnData[] spwanDatas;
    public float levelTime;
    float timer;
    
    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spwanDatas.Length;
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
        timer += Time.deltaTime;

        // level 값을 spawnPoints.Length와 spwanDatas.Length의 범위 내로 제한
        level = Mathf.Clamp(
            Mathf.FloorToInt(GameManager.instance.gameTime / levelTime),
            0,
            Mathf.Min(spawnPoints.Length - 1, spwanDatas.Length - 1)
        );

        if (timer > spwanDatas[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 1));
        if (enemy.CompareTag("Enemy"))
        {
            enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
            enemy.GetComponent<Enemy>().Init(spwanDatas[level]);
        }

    }
}

// 유니티 컴포넌트 프로퍼티에서 보일려면 직렬화를 해야한다. 20250131 topojs8
[System.Serializable]   
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;

}
