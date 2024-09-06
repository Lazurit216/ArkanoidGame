using UnityEngine;

[CreateAssetMenu(fileName = "BonusWidePlat", menuName = "Custom/Bonuses/WidePlat")]
public class BonusWidePlat : BonusEffect
{
    protected SpriteRenderer platformSprite;
    protected CapsuleCollider2D platformCol;
    [SerializeField] protected float scaleFactor;
    protected Vector2 startSize;
    public override void ApplyEffect()
    {
        platformSprite=Player.Instance.GetComponent<SpriteRenderer>();
        platformCol=Player.Instance.GetComponent<CapsuleCollider2D>();
        startSize = platformSprite.size;
        float newSize = startSize.x * scaleFactor;
        platformSprite.size=new Vector2 (newSize, platformSprite.size.y);
        platformCol.size = new Vector2(newSize, platformCol.size.y);

    }
    public override void DisableEffect()
    {
        platformSprite = Player.Instance.GetComponent<SpriteRenderer>();
        platformCol = Player.Instance.GetComponent<CapsuleCollider2D>();
        platformSprite.size = startSize;
        platformCol.size = startSize;
    }
}
