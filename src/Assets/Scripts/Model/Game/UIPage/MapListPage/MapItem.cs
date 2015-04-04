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

    public void OnClickForBattleField(GameObject CannonItem)
    {
        
        //PageManager.Instance.ShowPage(BattleField.Instance);

    }
    void Awake()
    {
        UIEventListener.Get(this.gameObject).onClick = OnClickForBattleField;

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