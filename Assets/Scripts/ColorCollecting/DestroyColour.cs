using UnityEngine;

public class DestroyColour : MonoBehaviour
{
    public GameObject explosion;
    
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}