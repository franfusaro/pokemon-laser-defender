using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    List<string> excludedTags = new List<string>() { "Enemy", "Boss" };

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (!excludedTags.Contains(gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
