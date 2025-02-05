using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CameraTrigger : MonoBehaviour
{
    public PlayerTileChecker tileChecker;
    public Vector3 newCameraPosition;
    public GameObject wallObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tileChecker.isOrderCorrect)
        {
            Debug.Log("Jugador pasó el trigger.");

            // Cambiar el enunciado al siguiente
            ChangeEnunciado();

            // Resetear el orden del jugador
            tileChecker.ResetPlayerOrder();

            // Mover la cámara y ocultar el muro
            MoveCamera();
            HideWall();
        }
    }

    private void ChangeEnunciado()
    {
        // Cambiar al siguiente enunciado usando la propiedad pública
        if (tileChecker.CurrentEnunciado == "Enlace")
        {
            tileChecker.SetOrderForEnunciado("Titulo");
        }
        else if (tileChecker.CurrentEnunciado == "Titulo")
        {
            tileChecker.SetOrderForEnunciado("Parrafo");
        }
        else if (tileChecker.CurrentEnunciado == "Parrafo")
        {
            Debug.Log("Todos los enunciados completados.");
            // Aquí puedes agregar lógica adicional si es necesario
        }
    }

    private void MoveCamera()
    {
        Camera.main.transform.position = newCameraPosition;
    }

    private void HideWall()
    {
        if (wallObject != null)
        {
            wallObject.SetActive(false);
        }
    }
}




