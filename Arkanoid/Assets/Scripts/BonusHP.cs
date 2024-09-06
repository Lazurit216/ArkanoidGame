using UnityEngine;
[CreateAssetMenu(fileName = "BonusHP", menuName = "Custom/Bonuses/HP")]
public class BonusHP : BonusEffect
{
    [SerializeField] private int hpAmount;
    public override void ApplyEffect()
    {
        Player.Instance.ChangeHP(hpAmount);
    }
    public override void DisableEffect()
    {
        throw new System.NotImplementedException();
    }

}
