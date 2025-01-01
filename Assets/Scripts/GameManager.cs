using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager _instance { get; private set; }

    // ������ ���¸� �����ϱ� ���� ����
    public int _playerScore = 0;  // �÷��̾� ����
    public int _playerHealth = 100; // �÷��̾� ü��
    public bool _isGameOver = false; // ���� ���� ����

    // �ð� ���� ����
    public float _gameTime = 0f; // ���� ���� �ð� (�� ����)

    public GameObject _player; // �÷��̾�

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

    private void Update()
    {
        if (!_isGameOver)
        {
            // ���� �ð� ������Ʈ
            _gameTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // ���� ����
        HandleVolumeInput();
    }

    private void HandleVolumeInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ���� ��
            SoundManager._instance.SetMasterVolume(SoundManager._instance._masterVolume + 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // ���� �ٿ�
            SoundManager._instance.SetMasterVolume(SoundManager._instance._masterVolume - 0.1f);
        }
    }

    // ���� �߰� �޼���
    public void AddScore(int amount)
    {
        if (!_isGameOver)
        {
            _playerScore += amount;
            SoundManager._instance.PlaySoundEffect(SoundManager._instance._scoreSound);
        }
    }

    // ü�� ���� �޼���
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

    // ���� ���� ó��
    private void GameOver()
    {
        _isGameOver = true;

        if (_player != null)
        {
            _player.gameObject.SetActive(false);
        }
    }

    // ���� ����� �޼���
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

    // ���� ���� �ð� ������ (��:�� ����)
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

    // ���� ������ �ε�
    public void LoadGameData()
    {
        DataManager._instance.LoadGameData();
        _playerScore = DataManager._instance._gameData._highScore;
        _playerHealth = DataManager._instance._gameData._playerHealth;
        SoundManager._instance.SetMasterVolume(DataManager._instance._gameData._masterVolume);
    }
}
