using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover2 : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerTileChecker tileChecker; 
    public Vector3 newCameraPosition;  
    public GameObject wallObject; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tileChecker.isOrderCorrect)
        {
            Debug.Log("Jugador pasó el trigger.");
            
            tileChecker.ResetPlayerOrder(); 
            MoveCamera();
            HideWall();
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

