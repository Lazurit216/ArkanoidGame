using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block
{
    [SerializeField] private List<BonusBall> bonusList;
    protected override void OnDeath()
    {
        int index = Random.RandomRange(0, bonusList.Count);
        Instantiate(bonusList[index], transform.position, Quaternion.identity);
        base.OnDeath();
    }
}
