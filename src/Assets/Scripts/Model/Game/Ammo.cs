using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour 
{

    public Vector3 speed;
    public GameObject target;
    public float damage;
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
            balloom.Attack(1);
            GameObject.Destroy(this.gameObject);
        }
        if(this.transform.localPosition.x>Screen.width||this.transform.localPosition.x<-Screen.width||this.transform.localPosition.y>Screen.height||this.transform.localPosition.y<-Screen.height)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
