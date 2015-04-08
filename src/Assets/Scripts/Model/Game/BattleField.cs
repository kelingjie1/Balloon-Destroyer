using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleField : BasePage 
{
    public static BattleField Instance;
    public List<Balloom> ballooms = new List<Balloom>();
    public float difficulty = 5;


    public float restBalloomTime;
    public float restPhaseTime;
    public float balloomGenerateRate;
    public float restBalloomGroupTime;


	void Awake () 
    {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () 
    {
        BalloomGenerate();
	}

    void BalloomGenerate()
    {
        restBalloomTime = restBalloomTime - Time.deltaTime;
        restPhaseTime = restPhaseTime - Time.deltaTime;
        restBalloomGroupTime = restBalloomGroupTime - Time.deltaTime;
        if (restBalloomGroupTime<0)
        {
            restBalloomGroupTime = Random.Range(10, 50);
            CreateBalloomGroup();
        }
        if (restPhaseTime<0)
        {
            CreateBalloom();
            restPhaseTime = 10;
            balloomGenerateRate = Random.Range(0.2f, 2);
        }

        if (restBalloomTime < 0)
        {
            restBalloomTime = 1 / balloomGenerateRate;
            CreateBalloom();
        }
    }

    void CreateBalloom()
    {
        Balloom balloom = Balloom.Create();
        this.gameObject.AddChild(balloom.gameObject);
        ballooms.Add(balloom);
        balloom.speed = 0.5f;
        balloom.hp = Random.Range(1, difficulty / 5 / balloomGenerateRate);
        balloom.transform.localPosition = new Vector3(Screen.width / 2 + 50, Random.Range(-Screen.height / 2, Screen.height / 2), 0);
    }

    void CreateBalloomGroup()
    {
        int id = Random.Range(0, 2); 
        GameObject balloomGroup = ResourceManager.LoadGameObject("Prefab/Game/BalloomGroup/BalloomGroup" + id);
        this.gameObject.AddChild(balloomGroup.gameObject);
        balloomGroup.transform.localPosition = new Vector3(Screen.width / 2 + 50, Random.Range(-Screen.height / 4, Screen.height / 4), 0);
        IEnumerator enumerator = balloomGroup.transform.GetEnumerator();

        while (balloomGroup.transform.childCount>0)
        {
            
            Balloom balloom = balloomGroup.transform.GetChild(0).GetComponent<Balloom>();
            balloom.transform.parent = this.transform;

            balloom.speed = 0.5f;
            balloom.hp = 2;
        }
    }
}
