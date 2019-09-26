using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour 
{
    void OnTriggerExit(Collider other) {
        Destroy(other.gameObject);
        //print("Detroy object: " + Time.time);
        if (other.tag == "Asteroid")
        {
            PlayerController.isMissAsteroid = true;
        }
    }
}
