using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
    float restTime;
    public void Shot()
    {
        Ammo ammo = Ammo.Create();
        BattleFeild.instance.gameObject.AddChild(ammo.gameObject);
        ammo.speed = (Input.mousePosition-new Vector3(Screen.width/2,Screen.height/2) - this.transform.localPosition).normalized;
        ammo.transform.localPosition = this.transform.localPosition;
    }
	void Update () 
    {
        restTime -= Time.deltaTime;
        if (restTime<0)
        {
            restTime = 0.5f;
            Shot();
        }
	}
}
