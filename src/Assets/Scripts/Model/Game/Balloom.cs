using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Balloom : MonoBehaviour 
{

    public float hp;
    public float hardness;
    public float armor;
    public float speed;
    public List<Ammo> aimAmmos = new List<Ammo>();
    public List<Cannon> aimCannons = new List<Cannon>();
    public static Balloom Create()
    {
        return ResourceManager.LoadGameObject("Prefab/Game/Balloom").GetComponent<Balloom>();
    }
    public void Hit(float damage, float puncture)
    {
        if (puncture >= hardness)
        {
            if (damage>=armor)
            {
                hp -= damage - armor;
                if (hp < 0)
                {
                    GameObject.Destroy(this.gameObject);
                }
            }
            
        }
        
    }

	void Update () 
    {
        this.transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
        if (this.transform.localPosition.x > Screen.width || this.transform.localPosition.x < -Screen.width || this.transform.localPosition.y > Screen.height || this.transform.localPosition.y < -Screen.height)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
