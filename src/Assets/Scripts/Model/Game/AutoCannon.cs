using UnityEngine;
using System.Collections;


public class AutoCannon : Cannon
{

    public override void Awake()
    {
        base.Awake();
        damage = 1;
        shotSpeed = 0.5f;
        speed = 1;
    }
    public override Balloom FindTarget()
    {
        if (BattleFeild.Instance.ballooms.Count>0)
        {
            target = BattleFeild.Instance.ballooms[0];
        }
        else
        {
            target = null;
        }
        return target;
    }

    public override Ammo CreateAmmo()
    {
        return FollowAmmo.Create();
    }
}
