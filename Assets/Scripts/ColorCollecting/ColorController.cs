using System.Collections;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public GameObject fallColor;
	public GameObject fallBlack;public Material blackMat;
	public GameObject fallBlue;public Material blueMat;
	public GameObject fallCyan;public Material cyanMat;
	public GameObject fallGreen;public Material greenMat;
	public GameObject fallGrey;public Material greyMat;
	public GameObject fallMagenta;public Material magentaMat;
	public GameObject fallOrange;public Material orangeMat;
	public GameObject fallSky;public Material skyMat;
	public GameObject fallRed;public Material redMat;
	public GameObject fallWhite;public Material whiteMat;
	public GameObject fallYellow;public Material yellowMat;
    public GameObject player;
    public Vector3 spawnValues;
    public float spawnWait;
	public float waitBefore;
	public float con;
	public int index;
	private bool gameOver;
	// private ReadUDP readUDP;
	private Color col;
	//public double a,b;
	public double thresh;
    void Start()
    {
		gameOver=false;
        StartCoroutine(ColorWaves());
		// GameObject readUDPObject = GameObject.FindWithTag("ReadUDP");
		// if(readUDPObject!=null)
		// {
		// 	readUDP=readUDPObject.GetComponent<ReadUDP>();
		// }
		// if(readUDPObject==null)
		// {
		// 	print("ColorController cannot find 'ReadUDP' script");
		// }
    }
	// void Update()
	// {
	// 	a=readUDP.dataChanged;
	// 	if(a<=thresh&&a>=-thresh){
	// 	a=-(a-3.0)/20.0;
    //     col=blackMat.color;    col.a=(float)a;     blackMat.color=col;
	// 	col=blueMat.color;     col.a=(float)a;     blueMat.color=col;
	// 	col=cyanMat.color;     col.a=(float)a;     cyanMat.color=col;
	// 	col=greenMat.color;    col.a=(float)a;     greenMat.color=col;
	// 	col=greyMat.color;     col.a=(float)a;     greyMat.color=col;
	// 	col=magentaMat.color;  col.a=(float)a;     magentaMat.color=col;
	// 	col=orangeMat.color;   col.a=(float)a;     orangeMat.color=col;
	// 	col=skyMat.color;      col.a=(float)a;     skyMat.color=col;
	// 	col=redMat.color;      col.a=(float)a;     redMat.color=col;
	// 	col=whiteMat.color;    col.a=(float)a;     whiteMat.color=col;
	// 	col=yellowMat.color;   col.a=(float)a;     yellowMat.color=col;
	// 	}
	// }
	IEnumerator ColorWaves()
    {
		yield return new WaitForSeconds(waitBefore);
        while(true)
        {
			index=Random.Range(1,12);
			if(index==1)fallColor=fallBlack;
			else if(index==2)fallColor=fallBlue;
			else if(index==3)fallColor=fallCyan;
			else if(index==4)fallColor=fallGreen;
			else if(index==5)fallColor=fallGrey;
			else if(index==6)fallColor=fallMagenta;
	 		else if(index==7)fallColor=fallOrange;
			else if(index==8)fallColor=fallRed;
			else if(index==9)fallColor=fallSky;
			else if(index==10)fallColor=fallWhite;
			else if(index==11)fallColor=fallYellow;
			con=Random.Range(player.transform.position.x - 6, player.transform.position.x + 13);
            while(con<14||(con>80&&con<105)||(con>174&&con<213)||(con>286&&con<314)||con>386)
            {
                con = Random.Range(player.transform.position.x - 10, player.transform.position.x + 13);
            }
            Vector3 spawnPosition = new Vector3(con, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(fallColor, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
			if(gameOver)break;
        }
		print("ENDcolor");
    }
	public void GameOver()
	{
		gameOver=true;
	}
}
