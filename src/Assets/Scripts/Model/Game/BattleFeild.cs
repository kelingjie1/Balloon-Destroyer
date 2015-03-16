using UnityEngine;
using System.Collections;

public class BattleFeild : MonoBehaviour 
{
    public static BattleFeild instance;
    float restTime;
	void Awake () 
    {
        instance = this;
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
            balloom.speed = 0.5f;
            balloom.transform.localPosition = new Vector3(Screen.width / 2 + 50, Random.Range(-Screen.height / 2, Screen.height / 2), 0);
        }


	}
}
