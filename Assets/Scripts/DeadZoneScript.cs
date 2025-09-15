using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthScript>().PlayerDeath();
        }
    }
}
