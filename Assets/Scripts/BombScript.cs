using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BladeScript blade = GetComponent<BladeScript>();
        if (blade)
        {
            return;
        }

        FindObjectOfType<GameManager>().OnBombHit();
    }
}
