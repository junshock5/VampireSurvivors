using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 12강 26분22초시간 컨트롤

    [Header("# game control")]
    public bool isLive;
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
    public LevelUp levelUp;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        health = maxHealth;

        // 임시
        levelUp.Select(0);
    }

    void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    
    public void GetExp(){
        exp++;
        if(exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)]){
            level++;
            exp = 0;
            levelUp.Show();
        }
    }

        public void Stop(){
            isLive = false;
            Time.timeScale = 0;
        }

        public void Resume(){
            isLive = true;
            Time.timeScale = 1;
        }
}
