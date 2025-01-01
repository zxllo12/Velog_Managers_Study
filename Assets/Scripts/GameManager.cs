using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager _instance { get; private set; }

    // 게임의 상태를 관리하기 위한 변수
    public int _playerScore = 0;  // 플레이어 점수
    public int _playerHealth = 100; // 플레이어 체력
    public bool _isGameOver = false; // 게임 오버 상태

    // 시간 관리 변수
    public float _gameTime = 0f; // 게임 진행 시간 (초 단위)

    public GameObject _player; // 플레이어

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

    private void Update()
    {
        if (!_isGameOver)
        {
            // 게임 시간 업데이트
            _gameTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // 볼륨 조절
        HandleVolumeInput();
    }

    private void HandleVolumeInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 볼륨 업
            SoundManager._instance.SetMasterVolume(SoundManager._instance._masterVolume + 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 볼륨 다운
            SoundManager._instance.SetMasterVolume(SoundManager._instance._masterVolume - 0.1f);
        }
    }

    // 점수 추가 메서드
    public void AddScore(int amount)
    {
        if (!_isGameOver)
        {
            _playerScore += amount;
            SoundManager._instance.PlaySoundEffect(SoundManager._instance._scoreSound);
        }
    }

    // 체력 감소 메서드
    public void TakeDamage(int damage)
    {
        if (!_isGameOver)
        {
            _playerHealth -= damage;
            SoundManager._instance.PlaySoundEffect(SoundManager._instance._damageSound);

            if (_playerHealth <= 0)
            {
                GameOver();
            }
        }
    }

    // 게임 오버 처리
    private void GameOver()
    {
        _isGameOver = true;

        if (_player != null)
        {
            _player.gameObject.SetActive(false);
        }
    }

    // 게임 재시작 메서드
    public void RestartGame()
    {
        if (_isGameOver == true)
        {
            _player.gameObject.SetActive(true);
        }

        _playerScore = 0;
        _playerHealth = 100;
        _gameTime = 0f;
        _isGameOver = false;

        SoundManager._instance.PlaySoundEffect(SoundManager._instance._restartSound);
    }

    // 현재 게임 시간 포맷팅 (분:초 형태)
    public string GetFormattedGameTime()
    {
        int minutes = Mathf.FloorToInt(_gameTime / 60f);
        int seconds = Mathf.FloorToInt(_gameTime % 60f);
        return $"{minutes:D2}:{seconds:D2}";
    }

    public void SaveGameData()
    {
        DataManager._instance._gameData._highScore = _playerScore;
        DataManager._instance._gameData._playerHealth = _playerHealth;
        DataManager._instance._gameData._masterVolume = SoundManager._instance._masterVolume;
        DataManager._instance.SaveGameData();
    }

    // 게임 데이터 로드
    public void LoadGameData()
    {
        DataManager._instance.LoadGameData();
        _playerScore = DataManager._instance._gameData._highScore;
        _playerHealth = DataManager._instance._gameData._playerHealth;
        SoundManager._instance.SetMasterVolume(DataManager._instance._gameData._masterVolume);
    }
}
