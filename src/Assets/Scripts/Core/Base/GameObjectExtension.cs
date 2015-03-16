using UnityEngine;
using System.Collections;

public static class GameObjectExtension
{
    public static GameObject FindChild(this GameObject obj, string name)
    {
        return Util.FindChild(name, obj);
    }

    public static void AddChild(this GameObject obj,GameObject child)
    {
        Util.AddChild(child, obj);
    }

    //实例化
    public static GameObject Instantiate(this GameObject obj)
    {
        return GameObject.Instantiate(obj) as GameObject;
    }
}
