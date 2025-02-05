using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float gameTime;
    public float maxGameTime =20f;
    public static GameManager instance;
    //public Player player2;
    public PoolManager pool;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
