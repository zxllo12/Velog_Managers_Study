using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource _effectsSource; // ȿ������ AudioSource
    public AudioSource _bgmSource; // ������ǿ� AudioSource

    [Header("Audio Clips")]
    public AudioClip _backgroundMusic; // �������
    public AudioClip _scoreSound; // ���� ���� ȿ����
    public AudioClip _damageSound; // ü�� ���� ȿ����
    public AudioClip _restartSound; // ���� ����� ȿ����

    [Range(0f, 1f)] public float _masterVolume = 0.5f; // ��ü ���� (0 ~ 1)

    private void Awake()
    {
        // �̱��� ����
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� GameManager ����
        }
    }

    private void Start()
    {
        PlayMusic(_backgroundMusic); // ���� ���� �� ����� ���
        UpdateAudioSourcesVolume(); // AudioSource ���� �ʱ�ȭ
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
        _masterVolume = Mathf.Clamp01(volume); // 0 ~ 1�� ����
        UpdateAudioSourcesVolume();
    }

    private void UpdateAudioSourcesVolume()
    {
        _effectsSource.volume = _masterVolume;
        _bgmSource.volume = _masterVolume;
    }
}
