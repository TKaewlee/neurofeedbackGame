using UnityEngine;
using System.Collections;

public class Revive : MonoBehaviour
{
    public Renderer rn;
    public Rigidbody rb;
    public Color a;
    public GameObject playerExplosion;
    public float reviveWait;
    void Start()
    {
        rn = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="FallObject")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            StartCoroutine(HideAndShow(2.0f));
        }
    }
    IEnumerator HideAndShow(float delay)
    {
        a=rn.material.color;
        rn.material.color=new Color(0,0,0,0);
        rb.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(delay);
        rb.constraints = RigidbodyConstraints.None;
        rn.material.color=a;
        print("Revive");
    }
}
