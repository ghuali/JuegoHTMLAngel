using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerColision : MonoBehaviour
{
    public Vector3 teleportPosition = new Vector3(0, 2, -54);

    // Este método se llama cuando ocurre una colisión
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisionó tiene la etiqueta "Coche"
        if (collision.gameObject.CompareTag("Coche"))
        {
            // Teletransporta al jugador a la posición especificada
            transform.position = teleportPosition;
            
        }
    }
}
