using UnityEngine;
using System.Collections;

public class GameCale
{
    public static float DamageReduction(float armor)
    {
        return armor / (armor + 100);
    }
    public static float Damage(float attack,float armor,float hardness)
    {
        return attack * (1 - DamageReduction(armor)) - hardness;
    }

    public static float NeedAttack(float hp,float armor,float hardness)
    {
        return (hp + hardness) / (1 - DamageReduction(armor));
    }
}
