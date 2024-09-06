using System.Collections;
using UnityEngine;

public class BonusBall : MonoBehaviour
{
    [SerializeField] protected int platformLayer;
    [SerializeField] protected float speed;
    [SerializeField] protected BonusEffect bonusType;
    protected float lifetime = 20;
    protected Rigidbody2D rb;
    protected Player player;
    protected void Start()
    {
        player = Player.Instance;
        rb= GetComponent<Rigidbody2D>();
        Move();
    }
    protected virtual void Move()
    {
        rb.velocity=Vector2.down*speed;
    }
    protected virtual void OnHitPlatform()
    {
        player.StartCoroutine(player.BonusTimer(bonusType.activeTime, bonusType));
        Destroy(gameObject);
    }
    protected IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer==platformLayer)
        {
            OnHitPlatform();
        }
    }
}
