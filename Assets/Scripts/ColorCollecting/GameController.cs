using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject fallObject;
    public GameObject player;
    public Vector3 spawnValues;
    public float spawnWait;
    public float waitBefore;
    public float con;
    public int score;
    public Vector3 tmp;
    public int changeStage;
    public int state;
    public Text scoreText;
    private bool gameOver;
    private ReadUDP readUDP;
    private BaselineTBR baselineTBR;
    // private TimeController timeControll;
    public Material fallObjectMat;
    // public double a,thresh;
    private Color col;
    public double i;
    public float time, limitTime;
    public Dropdown environmentDropdown;
    public int environmentIndex;
    //public static string environmentName;

    void Start()
    {
        i=1f;
        gameOver=false;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        environmentDropdown.onValueChanged.AddListener(delegate {
            actionDropdownValueChanged(environmentDropdown);
        });

        // GameObject readUDPObject = GameObject.FindWithTag("ReadUDP");
        // if(readUDPObject != null)
        // {
        //     readUDP = readUDPObject.GetComponent<ReadUDP>();
        // }
        // if(readUDPObject==null)
        // {
        //     Debug.Log("GameController cannot find 'ReadUDP' script");
        // }

        // GameObject calibrationObject = GameObject.FindWithTag("Calibration");
        // if(calibrationObject != null)
        // {
        //     baselineTBR = calibrationObject.GetComponent<BaselineTBR>();
        // }
        // if(calibrationObject==null)
        // {
        //     Debug.Log("GameController cannot find 'Calibration' script");
        // }
        
        // GameObject timeControllerObject = GameObject.FindWithTag("TimeController");
        // if(timeControllerObject != null)
        // {
        //     timeControll = timeControllerObject.GetComponent<TimeController>();
        //limitTime=timeController.timeSet;
        // }
        // if(timeControllerObject==null)
        // {
        //     Debug.Log("GameController cannot find 'TimeController' script");
        // }
    }
    private void actionDropdownValueChanged(Dropdown actionTarget)
    {
        environmentIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }
    void Update()
    {
        limitTime=timeController.timeSet;
        /*if(score==changeStage && state==1)
        {
            tmp=player.transform.position;
            tmp.x=110;
            player.transform.position=tmp;
            state++;
        }
        if(score==2*changeStage && state==2)
        {
            tmp=player.transform.position;
            tmp.x=220;
            player.transform.position=tmp;
            state++;
        }
        if(score==3*changeStage && state==3)
        {
            tmp=player.transform.position;
            tmp.x=325;
            tmp.y=3;
            player.transform.position=tmp;
            state++;
        }*/
        time = Time.time - timeController.timeStart;
        if(time==0){state=0; score=0; UpdateScore();}
        if(environmentIndex==0)
        {
            if(time>=0*limitTime && state==0)
            {
                tmp=player.transform.position;
                tmp.x=16;
                player.transform.position=tmp;
                state++;
            }
            if(time>=0.25*limitTime && state==1)
            {
                tmp=player.transform.position;
                tmp.x=110;
                player.transform.position=tmp;
                state++;
            }
            if(time>=0.5*limitTime && state==2)
            {
                tmp=player.transform.position;
                tmp.x=220; tmp.y=3;
                player.transform.position=tmp;
                state++;
            }
            if(time>=0.75*limitTime && state==3)
            {
                tmp=player.transform.position;
                tmp.x=325; tmp.y=3;
                player.transform.position=tmp;
                state++;
            }
        }
        else if(environmentIndex==1)
        {
            if(time==0)
            {
                tmp=player.transform.position;
                tmp.x=16;
                player.transform.position=tmp;
            }
        }
        else if(environmentIndex==2)
        {
            if(time==0)
            {
                tmp=player.transform.position;
                tmp.x=110;
                player.transform.position=tmp;
            }
        }
        else if(environmentIndex==3)
        {
            if(time==0)
            {
                tmp=player.transform.position;
                tmp.x=220; tmp.y=3;
                player.transform.position=tmp;
            }
        }
        else if(environmentIndex==4)
        {
            if(time==0)
            {
                tmp=player.transform.position;
                tmp.x=325; tmp.y=3;
                player.transform.position=tmp;
            }
        }

        //DOMESTIC COLOR CHANGING

        //Falled object color changing
        // a=read2UDP.dataTempChanged;
        //Calculate with thresh
        // thresh=baselineTBR.min;
        // a=(2*baselineTBR.baseline-thresh-a)/(2*(baselineTBR.baseline-thresh));
        // if(baselineTBR.baseline!=0f)
        // {
        //     if(a>=i)
        //     {
        //         while(a>i)
        //         {
        //             i+=0.01;
        //             col=fallObjectMat.color;
        //             col.a=(float)i;
        //             fallObjectMat.color=col;
        //         }
        //         print("Intensity UP");
        //     }
        //     else if(a<i)
        //     {
        //         while(a<i)
        //         {
        //             i-=0.01;
        //             col=fallObjectMat.color;
        //             col.a=(float)i;
        //             fallObjectMat.color=col;
        //         }
        //         print("Intensity DOWN");
        //     }
        // }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(waitBefore);
        while(true)
        {
            con=Random.Range(player.transform.position.x - 13, player.transform.position.x + 13);
            while(con<14||(con>80&&con<105)||(con>174&&con<213)||(con>286&&con<314)||con>386)
            {
                con = Random.Range(player.transform.position.x - 13, player.transform.position.x + 13);
            }
            Vector3 spawnPosition = new Vector3(con, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(fallObject, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
            if(gameOver)break;
        }
        print("ENDgame");
    }
    public void AddScore(int newScoreValue)
    {
        score+=newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text="Score: "+score.ToString();
    }
    public void GameOver()
    {
        gameOver=true;
    }
}