using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    public float speed;         //子弹飞行速度
    public Balloom target;      //瞄准目标
    public float attack;        //攻击力
    public int puncture;        //穿透
    public float ArmorBreak;    //破甲
    public float shotSpeed;     //攻击速度
    public float investigation; //侦查力
        public Vector3 direction;
    public static Ammo Create()
    {
        return ResourceManager.LoadGameObject("prefab/Game/Ammo").AddComponent<Ammo>();
    }
    public virtual void Awake()
    {
        EventManager.Instance.RegisterEvent(EventDefine.BalloomAppear, BalloomAppear);
        EventManager.Instance.RegisterEvent(EventDefine.BalloomDisappear, BalloomDesappear);
    }
    public virtual void BalloomAppear(EventDefine define, object param1, object param2, object param3, object param4)
    {
        if (target == null)
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
    public virtual void Update()
    {
        if (target)
        {
            direction = (target.transform.localPosition - this.transform.localPosition).normalized;
        }
        this.transform.Translate(Time.deltaTime * speed * direction);
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        Balloom balloom = collision.gameObject.GetComponent<Balloom>();
        if (balloom)
        {
            float damage = GameCale.Damage(this.attack, balloom.armor, balloom.hardness);
            if (puncture>0)
            {
                float needAttack = GameCale.NeedAttack(balloom.hp, balloom.armor, balloom.hardness);
                attack-=needAttack;
                if (attack>0)
                {
                    puncture--;
                }
                else
                {
                    GameObject.Destroy(this.gameObject);
                }
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
            balloom.Hurt(damage);

        }
        if(this.transform.localPosition.x>Screen.width||this.transform.localPosition.x<-Screen.width||this.transform.localPosition.y>Screen.height||this.transform.localPosition.y<-Screen.height)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
