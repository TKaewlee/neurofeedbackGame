using UnityEngine;

public class ChangeColourContact : MonoBehaviour
{
    public Renderer rend;
    public AudioSource ad;
    public int scoreValue;
    private GameController gameController;
    void Start()
    {
        rend = GetComponent<Renderer>();
        ad = GetComponent<AudioSource>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController==null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blue")
        {
            rend.material.color=new Color32(41,78,221,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Red")
        {
            rend.material.color=new Color32(255,63,59,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Green")
        {
            rend.material.color=new Color32(70,229,80,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "White")
        {
            rend.material.SetColor("_Color", Color.white);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Black")
        {
            rend.material.color=new Color32(51,51,51,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Yellow")
        {
            rend.material.color=new Color32(248,248,41,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Grey")
        {
            rend.material.color=new Color32(152,152,152,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Cyan")
        {
            rend.material.color=new Color32(73,236,165,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Magenta")
        {
            rend.material.color=new Color32(255,100,237,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Orange")
        {
            rend.material.color=new Color32(255,122,0,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Sky")
        {
            rend.material.color=new Color32(32,233,255,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
        if (other.tag == "Orange")
        {
            rend.material.color=new Color32(255,122,0,255);
            Destroy(other.gameObject);
            ad.Play();
            gameController.AddScore(scoreValue);
        }
    }

}
