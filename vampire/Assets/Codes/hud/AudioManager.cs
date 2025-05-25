using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgcVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;
    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;


    public enum SfxType
    {
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee,
        Range = 7,
        Select,
        Win
    }

    void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        // 배경음
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.SetParent(transform);
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgcVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // 효과음
        GameObject sfxObj = new GameObject("SfxPlayer");
        sfxObj.transform.SetParent(transform);
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].bypassListenerEffects = true;
            sfxPlayers[i].volume = sfxVolume;
        }
    }
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(SfxType sfxType)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            if (!sfxPlayers[i].isPlaying)
            {
                int loopIndex = channelIndex++ % sfxPlayers.Length;

                if (sfxPlayers[loopIndex].isPlaying)
                    continue;

                int randomIndex = 0;
                if (sfxType == SfxType.Hit || sfxType == SfxType.Melee)
                {
                    randomIndex = Random.Range(0, 2);
                }

                channelIndex = loopIndex;
                sfxPlayers[loopIndex].clip = sfxClips[(int)sfxType + randomIndex];
                sfxPlayers[loopIndex].Play();
                break;
            }
        }
    }
}
