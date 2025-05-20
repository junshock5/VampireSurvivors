using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // 12강 26분22초시간 컨트롤

    [Header("# game control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# player info")]
    public int playerId;
    public float maxHealth = 100;
    public float health;
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
    public Result uiResult;
    
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;
    }
    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        levelUp.Select(playerId % 2); // 플레이어 아이템 선택
        Resume();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();

        Stop();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(1);
        //GameOver();
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if(isLive == false)
            return;
        exp++;
        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            levelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
