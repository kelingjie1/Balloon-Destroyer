using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Cannon : MonoBehaviour
{
    public BasicCannonData data;
    public float attack;        //攻击力
    public float speed;         //子弹飞行速度
    public int puncture;        //穿透
    public float armorBreak;    //破甲
    public float shotSpeed;     //攻击速度
    public float investigation; //侦查力
    public Balloom target;      //瞄准目标
    public Vector3 direction = new Vector3(1, 0, 0);
    
    public float restTime;

	public bool   m_bOnFight; //是否在出战状态
	public bool   m_bUnLock;  //是否解锁

    public virtual void Awake()
    {
        EventManager.Instance.RegisterEvent(EventDefine.BalloomAppear, BalloomAppear);
        EventManager.Instance.RegisterEvent(EventDefine.BalloomDisappear, BalloomDesappear);
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
        ammo.attack = attack;
        ammo.puncture = puncture;
        ammo.target = target;
        ammo.speed = speed;
        BattleField.Instance.gameObject.AddChild(ammo.gameObject);
        ammo.direction = this.direction;
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
        if (target)
        {
            this.direction = (target.transform.localPosition - this.transform.localPosition).normalized;
        }
        int dir = 1;
        if (this.direction.y<0)
        {
            dir = -1;
        }
        this.transform.localEulerAngles = new Vector3(0, 0, dir * Vector3.Angle(new Vector3(1, 0, 0), this.direction));
	}
}
