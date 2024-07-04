using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#sfx")] //효과음
    public AudioClip[] sfxClip; // 효과음은 여러개여서 배열로
    public float sfxVolume;
    public int channels; // 여러 소리를 내기 때문에 채널로 관리
    AudioSource[] sfxPlayers;
    int channel_Index; // 지금 재생중인 소리가 몇번째 인덱스인지


    public enum Sfx { Attack=0, Fire=1, Teleport=2, Jump=3}  

    void Awake()
    {
        instance = this; // 자기자신이다
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>(); // 배경음을 담당하는 자식 오브젝트
        bgmPlayer.playOnAwake = false; // 게임이 시작 버튼이 눌리면 시작되게
        bgmPlayer.loop = true; // 계속 재생
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // 오디오 소스를 담을 배열
        
        for(int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;

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

    public void PlaySfx(Sfx sfx) //효과음 재생 함수
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channel_Index) & sfxPlayers.Length; // 채널 개수만큼 순회하도록,  & 채널이 넘어가지않게

            if (sfxPlayers[loopIndex].isPlaying)
                continue; //컨티뉴는 반복문 도중에 다음 루프로 건너뛰는 키워드

            channel_Index = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayers[loopIndex].Play(); // 오디오소스의 클립을 변경하고 Play 함수 호출
            break; //효과음 재생이 된 이후에는 반복문 종료
        }
       
    }
}
