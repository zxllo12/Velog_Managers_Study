using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager._instance.AddScore(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager._instance.TakeDamage(20);
        }
    }
}
