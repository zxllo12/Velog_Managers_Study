using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI _scoreText; // ���� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI _timeText; // �ð� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI _gameOverText; // ���� ���� �ؽ�Ʈ

    public Button _saveButton; // ���� ��ư
    public Button _loadButton; // �ҷ����� ��ư
    public Button _resetButton; // ���� ��ư

    public Slider volumeSlider; // ���� �����̴�

    private void Start()
    {
        // Null üũ�� ���� ��ư ������Ʈ�� �������� �ʾ��� ��� �߻��� �� �ִ� ��Ÿ�� ������ �����մϴ�.
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
        // ���� ������Ʈ
        _scoreText.text = $"Score: {GameManager._instance._playerScore}";

        // ���� �ð� ������Ʈ
        _timeText.text = $"Time: {GameManager._instance.GetFormattedGameTime()}";

        // ���� �����̴� ������Ʈ
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
