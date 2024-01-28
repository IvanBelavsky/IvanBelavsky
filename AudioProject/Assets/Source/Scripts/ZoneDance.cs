using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDance : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.Dance();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
                 player.StpoDance();
        }
    }
}