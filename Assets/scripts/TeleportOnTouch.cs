using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTouch : MonoBehaviour
{
    public Transform teleportDestination; // Asigna en el inspector la posición de destino

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            other.transform.position = teleportDestination.position;
            Debug.Log("Jugador teletransportado a: " + teleportDestination.position);
        }
    }
}

