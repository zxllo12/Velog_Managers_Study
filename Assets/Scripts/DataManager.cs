using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager _instance { get; private set; }

    private string _saveFilePath; // ���� ���� ��ġ

    [System.Serializable]
    public class GameData
    {
        public int _highScore;
        public int _playerHealth;
        public float _masterVolume;
    }

    public GameData _gameData;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _saveFilePath = Path.Combine(Application.persistentDataPath, "GameData.json");

            LoadGameData(); // ���� ���� �� ������ �ε�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void SaveGameData()
    {
        // ���� �����͸� ����
        string json = JsonUtility.ToJson(_gameData, true); // JSON �������� ��ȯ
        File.WriteAllText(_saveFilePath, json); // ���Ͽ� ����
    }

    
    public void LoadGameData()
    {
        // ���� �����͸� �ε�
        if (File.Exists(_saveFilePath))
        {
            string json = File.ReadAllText(_saveFilePath); // ���Ͽ��� �б�
            _gameData = JsonUtility.FromJson<GameData>(json); // JSON �����͸� ��ü�� ��ȯ
        }
        else
        {
            _gameData = new GameData
            {
                _highScore = 0,
                _playerHealth = 100,
                _masterVolume = 0.5f
            };
        }
    }

    
    public void ResetGameData()
    {
        // �����͸� �ʱ�ȭ
        _gameData = new GameData
        {
            _highScore = 0,
            _playerHealth = 100,
            _masterVolume = 0.5f
        };

        SaveGameData();
    }
}
