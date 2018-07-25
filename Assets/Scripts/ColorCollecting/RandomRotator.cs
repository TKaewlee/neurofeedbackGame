using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public Rigidbody rb;
    public float tumble;
    private void Start()
    {
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
