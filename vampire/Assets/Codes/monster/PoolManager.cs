using UnityEngine;
using System.Collections.Generic; // 추가된 네임스페이스

public class PoolManager : MonoBehaviour
{
    // 프리맵 보관 변수
    public GameObject[] prefabs; // 프리맵 리스트

    // 품 담당 리스트
    List<GameObject>[] pools;    
    
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 프리맵 리스트의 길이만큼 풀 리스트 생성
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>(); // 풀 리스트 초기화
        }
    }
    public GameObject Get(int index)
    {
        GameObject select = null;
        // 풀의 게임 오브젝트 접근
        foreach (GameObject obj in pools[index])
        {
            if(!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        // 못찾음
        if(select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }

}
