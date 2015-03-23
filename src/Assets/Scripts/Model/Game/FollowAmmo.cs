using UnityEngine;
using System.Collections;

public class FollowAmmo : Ammo 
{
    public static FollowAmmo Create()
    {
        return ResourceManager.LoadGameObject("prefab/Game/Ammo").AddComponent<FollowAmmo>();
    }
    public override Balloom FindTarget()
    {
        if (BattleField.Instance.ballooms.Count > 0)
        {
            target = BattleField.Instance.ballooms[0];
        }
        else
        {
            target = null;
        }
        return target;
    }
}
