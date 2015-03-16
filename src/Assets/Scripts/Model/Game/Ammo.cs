using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{

    public Vector3 speed;
    Balloom m_target;
    public Balloom target
    {
        get
        {
            return m_target;
        }
        set
        {
            if (m_target)
            {
                m_target.aimAmmos.Remove(this);
            }
            m_target = value;
            m_target.aimAmmos.Add(this);
        }
    }
    public float damage;
    public float puncture;
    public static Ammo Create()
    {
        return ResourceManager.LoadGameObject("prefab/Game/Ammo").GetComponent<Ammo>();
    }
    void Update()
    {
        this.transform.Translate(Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
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
