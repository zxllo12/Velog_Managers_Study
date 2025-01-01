using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI _healthText; // ü�� ǥ�ÿ�
    public Transform _target; // ���� ��� (�÷��̾�)

    private Vector3 _offset = new Vector3(0, 2f, 0); // �÷��̾� ���� �ణ �ø� ��ġ

    private void Update()
    {
        // ü�� �ؽ�Ʈ ������Ʈ
        _healthText.text = $"HP: 100 / {GameManager._instance._playerHealth}";

        // ü�� UI�� �÷��̾� ��ġ�� ����
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
    }
}
