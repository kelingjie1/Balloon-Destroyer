using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleField : MonoBehaviour 
{
    public static BattleField Instance;
    public List<Balloom> ballooms = new List<Balloom>();
    public float difficulty = 5;
    float restTime;
	void Awake () 
    {
        Instance = this;
		//CannonManger.GetInstance();

		//CannonItem nCannonItem = CannonItem.Create ();
		//this.gameObject.AddChild(nCannonItem.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        restTime = restTime - Time.deltaTime;
        if (restTime < 0)
        {
            restTime = 1;
            Balloom balloom = Balloom.Create();
            this.gameObject.AddChild(balloom.gameObject);
            ballooms.Add(balloom);
            balloom.speed = 0.5f;
            balloom.hp = Random.Range(1, difficulty);
            balloom.transform.localPosition = new Vector3(Screen.width / 2 + 50, Random.Range(-Screen.height / 2, Screen.height / 2), 0);
        }


	}

}
