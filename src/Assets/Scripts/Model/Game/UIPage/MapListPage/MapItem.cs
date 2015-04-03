using UnityEngine;
using System.Collections;

public class MapItem : MonoBehaviour
{
    public CannonData m_stCannonData; //one 塔数据
    public static MapItem Create()
    {
        Debug.Log("Create MapItem");
        return ResourceManager.LoadGameObject("prefab/Game/MapListPage/MapItem").AddComponent<MapItem>();
    }

    public void OnClickForShowCannonDetail(GameObject CannonItem)
    {
      
    }
    void Awake()
    {
        //UIEventListener.Get(gameObject.FindChild("CannonItemButton")).onClick = OnClickForShowCannonDetail;

    }

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}