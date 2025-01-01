using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI _scoreText; // 점수 표시 텍스트
    public TextMeshProUGUI _timeText; // 시간 표시 텍스트
    public TextMeshProUGUI _gameOverText; // 게임 오버 텍스트

    public Button _saveButton; // 저장 버튼
    public Button _loadButton; // 불러오기 버튼
    public Button _resetButton; // 리셋 버튼

    public Slider volumeSlider; // 볼륨 슬라이더

    private void Start()
    {
        // Null 체크를 통해 버튼 오브젝트가 설정되지 않았을 경우 발생할 수 있는 런타임 오류를 방지합니다.
        if (_saveButton != null)
        {
            _saveButton.onClick.AddListener(GameManager._instance.SaveGameData);
        }

        if (_loadButton != null)
        {
            _loadButton.onClick.AddListener(GameManager._instance.LoadGameData);
        }

        if (_resetButton != null)
        {
            _resetButton.onClick.AddListener(DataManager._instance.ResetGameData);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = SoundManager._instance._masterVolume;
        }
    }

    private void Update()
    {
        // 점수 업데이트
        _scoreText.text = $"Score: {GameManager._instance._playerScore}";

        // 게임 시간 업데이트
        _timeText.text = $"Time: {GameManager._instance.GetFormattedGameTime()}";

        // 볼륨 슬라이더 업데이트
        volumeSlider.value = SoundManager._instance._masterVolume;

        if (GameManager._instance._isGameOver)
        {
            _gameOverText.text = "Game Over!";
            _gameOverText.gameObject.SetActive(true);
        }
        else
        {
            _gameOverText.gameObject.SetActive(false);
        }
    }
}
