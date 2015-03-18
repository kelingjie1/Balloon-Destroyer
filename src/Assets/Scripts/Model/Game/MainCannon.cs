using UnityEngine;
using System.Collections;

public class MainCannon : Cannon 
{
    public override void Awake()
    {
 	    base.Awake();
        damage = 1;
        shotSpeed = 2;
        speed = 1;
    }

    public override Ammo Shot()
    {
        Ammo ammo = base.Shot();
        ammo.direction = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) - this.transform.localPosition).normalized;
        return ammo;
    }
    
}
