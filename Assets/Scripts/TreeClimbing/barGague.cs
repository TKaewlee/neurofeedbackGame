using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class barGague : MonoBehaviour {
	public Transform gaugeBar;
	public Slider gaugeFill;
	public float currentGauge;
	public float maxGauge;
	// Update is called once per frame
	void Update () {
		controlGauge();
	}

	public void ChangeGauge(int amount){
		currentGauge += amount;
		currentGauge = Mathf.Clamp(currentGauge, 0, maxGauge);

		gaugeFill.value = currentGauge / maxGauge;
	}
	private void controlGauge(){
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		ChangeGauge((int)controlThrow);
	}
}
