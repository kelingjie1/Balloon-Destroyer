using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    public float speed;         //子弹飞行速度
    public Balloom target;      //瞄准目标
    public float damage;        //攻击力
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
        EventManager.Instance.RegisterEvent(EventDefine.BalloomDesappear, BalloomDesappear);
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
            balloom.Hit(damage, puncture);
            GameObject.Destroy(this.gameObject);
        }
        if(this.transform.localPosition.x>Screen.width||this.transform.localPosition.x<-Screen.width||this.transform.localPosition.y>Screen.height||this.transform.localPosition.y<-Screen.height)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
