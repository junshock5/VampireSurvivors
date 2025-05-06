using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("# game control")]
    public float gameTime;
    public float maxGameTime =20f;

    [Header("# player info")]
    public int maxHealth = 100;
    public int health;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

    [Header("# game object")]

    public static GameManager instance;
    public Player player;
    //public Player player2;
    public PoolManager pool;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    
    public void GetExp(){
        exp++;
        if(exp >= nextExp[level]){
            level++;
            exp = 0;
        }
    }
}
