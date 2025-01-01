using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager _instance { get; private set; }

    private string _saveFilePath; // 파일 저장 위치

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

            LoadGameData(); // 게임 시작 시 데이터 로드
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void SaveGameData()
    {
        // 게임 데이터를 저장
        string json = JsonUtility.ToJson(_gameData, true); // JSON 형식으로 변환
        File.WriteAllText(_saveFilePath, json); // 파일에 저장
    }

    
    public void LoadGameData()
    {
        // 게임 데이터를 로드
        if (File.Exists(_saveFilePath))
        {
            string json = File.ReadAllText(_saveFilePath); // 파일에서 읽기
            _gameData = JsonUtility.FromJson<GameData>(json); // JSON 데이터를 객체로 변환
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
        // 데이터를 초기화
        _gameData = new GameData
        {
            _highScore = 0,
            _playerHealth = 100,
            _masterVolume = 0.5f
        };

        SaveGameData();
    }
}
