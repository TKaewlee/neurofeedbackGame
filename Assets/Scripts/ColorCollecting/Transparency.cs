using System.Collections;
using UnityEngine;

public class Transparency : MonoBehaviour 
{
	public Renderer render;
	public Color col;
	public float trans;
	void Start()
	{
		render=GetComponent<Renderer>();
	}
	void Update()
	{
		col=render.material.color;
		col.a=trans;
		render.material.color=col;
	}
	public void UpdateTrans(float tran)
	{
		trans=tran;
	}
}
