using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class toggleText : MonoBehaviour {
	
	public Toggle toggle;
	public Text label;
	public string ison;
	public string isoff;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (toggle.isOn) {
			label.text = ison;
		}
		else {
			label.text = isoff;
		}
	}
}
