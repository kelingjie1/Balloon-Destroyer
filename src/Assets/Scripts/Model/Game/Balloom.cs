using UnityEngine;
using System.Collections;

public class Balloom : MonoBehaviour 
{

    public float hp;
    public float speed;
    public static Balloom Create()
    {
        return ResourceManager.LoadGameObject("Prefab/Game/Balloom").GetComponent<Balloom>();
    }
	public void Attack(float damage)
    {
        hp -= damage;
        if (hp<0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
        if (this.transform.localPosition.x > Screen.width || this.transform.localPosition.x < -Screen.width || this.transform.localPosition.y > Screen.height || this.transform.localPosition.y < -Screen.height)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
