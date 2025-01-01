using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource _effectsSource; // 효과음용 AudioSource
    public AudioSource _bgmSource; // 배경음악용 AudioSource

    [Header("Audio Clips")]
    public AudioClip _backgroundMusic; // 배경음악
    public AudioClip _scoreSound; // 점수 증가 효과음
    public AudioClip _damageSound; // 체력 감소 효과음
    public AudioClip _restartSound; // 게임 재시작 효과음

    [Range(0f, 1f)] public float _masterVolume = 0.5f; // 전체 볼륨 (0 ~ 1)

    private void Awake()
    {
        // 싱글톤 설정
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복 GameManager 제거
        }
    }

    private void Start()
    {
        PlayMusic(_backgroundMusic); // 게임 시작 시 배경음 재생
        UpdateAudioSourcesVolume(); // AudioSource 볼륨 초기화
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            _effectsSource.PlayOneShot(clip);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_bgmSource.isPlaying)
        {
            _bgmSource.Stop();
        }

        _bgmSource.clip = clip;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = Mathf.Clamp01(volume); // 0 ~ 1로 제한
        UpdateAudioSourcesVolume();
    }

    private void UpdateAudioSourcesVolume()
    {
        _effectsSource.volume = _masterVolume;
        _bgmSource.volume = _masterVolume;
    }
}
