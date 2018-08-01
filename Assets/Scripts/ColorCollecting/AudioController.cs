using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	public GameObject player;
	private AudioSource ad;
	public AudioClip forestAudio;
	public AudioClip winterAudio;
	public AudioClip desertAudio;
	public AudioClip oceanAudio;
	public int state,con;
	void Start()
	{
		ad=gameObject.GetComponent<AudioSource>();
		state=1;con=0;
	}
	void Update()
	{
		if(state==1&&player.transform.position.x>=14&&player.transform.position.x<=80)
		{
			ad.clip=forestAudio;
			ad.Play();print("play1");
			state=2;
		}
		if(state==2&&player.transform.position.x>=105&&player.transform.position.x<=174)
		{
			ad.clip=winterAudio;
			ad.Play();print("play2");
			state=3;
		}
		if(state==3&&player.transform.position.x>=213&&player.transform.position.x<=286)
		{
			ad.clip=desertAudio;
			ad.Play();print("play3");
			state=4;
		}
		if(state==4&&player.transform.position.x>=314&&player.transform.position.x<=386)
		{
			ad.clip=oceanAudio;
			ad.Play();print("play4");
			state=5;
		}
	}
}
