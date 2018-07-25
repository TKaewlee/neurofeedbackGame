using UnityEngine;

public class FallObjectMovement : MonoBehaviour
{
    public Rigidbody fallObject;
    public Vector3 a;
    void Update()
    {
        a=fallObject.velocity;
        a.y = -3f;
        fallObject.velocity = a;
    }
}
