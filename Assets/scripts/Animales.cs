using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animales : MonoBehaviour
{
  public Vector3 teleportPosition = new Vector3(15, 2, 59);

    // Este método se llama cuando ocurre una colisión
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisionó tiene la etiqueta "Coche"
        if (collision.gameObject.CompareTag("Animal"))
        {
            // Teletransporta al jugador a la posición especificada
            transform.position = teleportPosition;
            
        }
    }
}
