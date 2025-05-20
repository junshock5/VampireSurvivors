using UnityEngine;
using System;
using System.Collections;
public class AchiveManager : MonoBehaviour
{
    // 19분 12초 14+
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject[] uiNotice;

    enum Achive { UnlockPotato, UnlockBean }
    Achive[] achives;
    WaitForSecondsRealtime wait;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        if (!PlayerPrefs.HasKey("MyData")){
            Init();
        }
        wait = new WaitForSecondsRealtime(5);
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }

    }
    void Start()
    {
        UnlockCharacters();
    }

    void UnlockCharacters()
    {

        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach ( Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }
    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            // uiNotice 배열의 각 요소에 대해 활성화/비활성화 처리
            for (int i = 0; i < uiNotice.Length; i++)
            {
                bool isActive = i == i; // index와 i를 비교
                uiNotice[i].SetActive(isActive); // 배열의 특정 요소에 접근
            }
            StartCoroutine(NoticRoutine());
        }

    }

    IEnumerator NoticRoutine()
    {
        foreach (GameObject notice in uiNotice)
        {
            notice.SetActive(true); // 배열의 각 요소에 대해 SetActive 호출
        }

        yield return wait;

        foreach (GameObject notice in uiNotice)
        {
            notice.SetActive(false); // 배열의 각 요소에 대해 SetActive 호출
        }
    }

}
