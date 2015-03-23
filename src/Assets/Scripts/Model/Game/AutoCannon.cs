using UnityEngine;
using System.Collections;


public class AutoCannon : Cannon
{

    public override void Awake()
    {
        base.Awake();
        attack = 1;
        shotSpeed = 0.5f;
        speed = 1;
    }
    public override Balloom FindTarget()
    {
        if (BattleField.Instance.ballooms.Count>0)
        {
            target = BattleField.Instance.ballooms[0];
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
