using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour 
{
	public float countValue;
	public float currentTime;
	public Text timeText;
	public GameController gameController;
	public ColorController colorController;
	public Text timeUpText;
	void Start()
	{
		timeUpText.text="";
		StartCoroutine(CountDown());
	}
	IEnumerator CountDown()
	{
		currentTime=countValue;
		while(currentTime>=0)
		{
			timeText.text=currentTime.ToString();
			yield return new WaitForSeconds(1.0f);
			currentTime--;
		}
		timeUpText.text="TIME's UP!";
		gameController.GameOver();
		colorController.GameOver();
	}
}
