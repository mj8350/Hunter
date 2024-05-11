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

    [Header("#sfx")] //ȿ����
    public AudioClip[] sfxClip; // ȿ������ ���������� �迭��
    public float sfxVolume;
    public int channels; // ���� �Ҹ��� ���� ������ ä�η� ����
    AudioSource[] sfxPlayers;
    int channel_Index; // ���� ������� �Ҹ��� ���° �ε�������


    public enum Sfx { Attack=0, Fire=1, Teleport=2, Jump=3}  

    void Awake()
    {
        instance = this; // �ڱ��ڽ��̴�
        Init();
    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>(); // ������� ����ϴ� �ڽ� ������Ʈ
        bgmPlayer.playOnAwake = false; // ������ ���� ��ư�� ������ ���۵ǰ�
        bgmPlayer.loop = true; // ��� ���
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // ����� �ҽ��� ���� �迭
        
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

    public void PlaySfx(Sfx sfx) //ȿ���� ��� �Լ�
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channel_Index) & sfxPlayers.Length; // ä�� ������ŭ ��ȸ�ϵ���,  & ä���� �Ѿ���ʰ�

            if (sfxPlayers[loopIndex].isPlaying)
                continue; //��Ƽ���� �ݺ��� ���߿� ���� ������ �ǳʶٴ� Ű����

            channel_Index = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayers[loopIndex].Play(); // ������ҽ��� Ŭ���� �����ϰ� Play �Լ� ȣ��
            break; //ȿ���� ����� �� ���Ŀ��� �ݺ��� ����
        }
       
    }
}
