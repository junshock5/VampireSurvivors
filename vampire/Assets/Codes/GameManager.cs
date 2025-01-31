using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public static GameManager instance;
    //public Player player2;
    public PoolManager pool;

    void Awake()
    {
        instance = this;
    }
}
