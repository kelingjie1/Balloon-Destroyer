using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Balloom : MonoBehaviour 
{
    public float m_hp;
    public float hp            //生命
    {
        get
        {
            return m_hp;
        }
        set
        {
            m_hp = value;
            if (value>0)
            {
                float color = 1 - this.hp / BattleField.Instance.difficulty;
                this.GetComponent<UISprite>().color = new Color(color, color, color);
            }
        }
    }
    public float hardness;      //硬度
    public float armor;         //护甲
    public float speed;         //速度
    public float cover;         //隐蔽
    public static Balloom Create()
    {
        return ResourceManager.LoadGameObject("Prefab/Game/Balloom").GetComponent<Balloom>();
    }
    public virtual void Awake()
    {
        EventManager.Instance.SendEvent(EventDefine.BalloomAppear, -1, this);
    }
    public virtual void Hurt(float damage)
    {
        this.hp -= damage;
        if (this.hp < 0)
        {
            
            BattleField.Instance.ballooms.Remove(this);
            EventManager.Instance.SendEvent(EventDefine.BalloomDisappear, -1, this);
            GameObject.Destroy(this.gameObject);
        }
    }
	public virtual void Update() 
    {
        this.transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
        if (this.transform.localPosition.x > Screen.width || this.transform.localPosition.x < -Screen.width/2 || this.transform.localPosition.y > Screen.height || this.transform.localPosition.y < -Screen.height)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
