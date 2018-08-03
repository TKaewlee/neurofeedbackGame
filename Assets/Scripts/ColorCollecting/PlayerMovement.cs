using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 a, b;
    public float v,j;
    public float moveRate;
    private float nextMove;
    public int con = 0;
    private void OnCollisionEnter(Collision collision)
    {
        con = 0;
    }
    void Update ()
    {

        Physics.gravity = new Vector3(0, -15f, 0);
        a = rb.velocity;
        b = rb.angularVelocity;
        a.z = 0f;
        b.x = 0f;
        b.y = 0f;
        rb.velocity = a;
        rb.angularVelocity = b;
        if ((Input.GetKey("d")||Input.GetKey("right")) && con==0)
        {
            a.x = v;
            rb.velocity = a;
        }
        if ((Input.GetKey("a")||Input.GetKey("left")) && con==0)
        {
            a.x = -v;
            rb.velocity = a;
        }
        if ((Input.GetKey("w")||Input.GetKey("up")) && Time.time > nextMove)
        {
            con = 1;
            nextMove = Time.time + moveRate;
            a.y = j;
            rb.velocity = a;
        }
    }
}
