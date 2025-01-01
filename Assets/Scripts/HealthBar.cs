using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI _healthText; // 체력 표시용
    public Transform _target; // 따라갈 대상 (플레이어)

    private Vector3 _offset = new Vector3(0, 2f, 0); // 플레이어 위로 약간 올린 위치

    private void Update()
    {
        // 체력 텍스트 업데이트
        _healthText.text = $"HP: 100 / {GameManager._instance._playerHealth}";

        // 체력 UI를 플레이어 위치에 고정
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
    }
}
