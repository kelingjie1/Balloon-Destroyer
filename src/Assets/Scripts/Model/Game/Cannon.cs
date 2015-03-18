using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public float speed;
    public Balloom target;
    public float damage;
    public float puncture;

    public float shotSpeed;
    public float restTime;
    public virtual void Awake()
    {
        EventManager.Instance.RegisterEvent(EventDefine.BalloomAppear, BalloomAppear);
        EventManager.Instance.RegisterEvent(EventDefine.BalloomDesappear, BalloomDesappear);
    }
    public virtual void BalloomAppear(EventDefine define, object param1, object param2, object param3, object param4)
    {
        if (target==null)
        {
            FindTarget();
        }
    }
    public virtual void BalloomDesappear(EventDefine define, object param1, object param2, object param3, object param4)
    {
        FindTarget();
    }
    public virtual Balloom FindTarget()
    {
        return null;
    }
    public virtual Ammo CreateAmmo()
    {
        return Ammo.Create();
    }
    public virtual Ammo Shot()
    {
        Ammo ammo = CreateAmmo();
        ammo.damage = damage;
        ammo.puncture = puncture;
        ammo.target = target;
        ammo.speed = speed;
        BattleFeild.Instance.gameObject.AddChild(ammo.gameObject);
        if (target)
        {
            ammo.direction = (target.transform.localPosition - this.transform.localPosition).normalized;
        }
        else
        {
            ammo.direction = new Vector3(1, 0, 0);
        }
        ammo.transform.localPosition = this.transform.localPosition;
        return ammo;
    }
    public virtual void Update() 
    {
        restTime -= Time.deltaTime;
        if (restTime < 0)
        {
            restTime = 1/shotSpeed;
            Shot();
        }
	}
}
