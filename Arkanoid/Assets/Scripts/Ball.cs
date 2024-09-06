using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Transform parentTr;
    private float myforce;
    [SerializeField] private float minAxisAngle=0.07f;
    [SerializeField] private int wallLayer;
    [SerializeField] private int platformLayer;
    [SerializeField] private int blockLayer;
    [SerializeField] private int dedLayer;
    private PlayerController playerController;
    private Player player;
    private Rigidbody2D m_Rigidbody;
    private Vector2 lastVelocity;
    private float lowSpeedTryesLimit = 100;
    private float lowSpeedTryes;
    private float minSpeed = 1;

    private SoundManager soundManager;
    [SerializeField] private AudioClip wallReflectSound;
    private void Start()
    {
        soundManager=SoundManager.Instance;

        playerController = PlayerController.Instance;
        playerController.onBallKicked += Unhook;

        player=Player.Instance;
        player.onDed += SetStartOpt;

        m_Rigidbody = GetComponent<Rigidbody2D>();

        lowSpeedTryes = 0;

        SetStartOpt();
    }
    private void SetStartOpt()
    {
        m_Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = parentTr;
        transform.localPosition = Vector2.zero;
    }
    private void FixedUpdate()
    {
        lastVelocity = m_Rigidbody.velocity;
    }
    private void Update()
    {
        if (playerController.isStarted && m_Rigidbody.velocity.magnitude < minSpeed)
        {
            lowSpeedTryes += 1;
            if (lowSpeedTryes > lowSpeedTryesLimit)
            {
                lowSpeedTryes = 0;
                player.OnDeath(true);
            }
        }
    }
    private void Unhook(float angle, float force)
    {
        myforce=force;
        transform.parent=null;
        m_Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Vector2 forceV = new Vector2(0, 1);
        Vector2 rotatedVector = Quaternion.AngleAxis(angle, Vector3.forward) * forceV;
        m_Rigidbody.velocity = rotatedVector * myforce;
    }
    private void PlatformKick()
    {
        float angle= player.GetAngle(transform.position);
        Vector2 forceV = new Vector2(0, 1);
        Vector2 rotatedVector = Quaternion.AngleAxis(angle, Vector3.forward) * forceV;
        m_Rigidbody.velocity = rotatedVector *myforce;
        
    }

    private void WallBounce(Collision2D collision)
    {

        Vector2 direction = Vector2.Reflect(lastVelocity.normalized, collision.GetContact(0).normal);
        Debug.Log(direction);
        if (direction.x==0) direction=new Vector2(minAxisAngle, direction.y);
        if (direction.y == 0) direction = new Vector2(direction.x, minAxisAngle);
        Debug.Log(direction);

        m_Rigidbody.velocity = direction*myforce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==wallLayer)
        {
            WallBounce(collision);
            soundManager.PlaySound(wallReflectSound);
        }
        else if (collision.gameObject.layer == platformLayer)
        {
            PlatformKick();
            soundManager.PlaySound(wallReflectSound);
        }
        else if (collision.gameObject.layer == blockLayer)
        {
            WallBounce(collision);
            collision.gameObject.GetComponent<Block>().OnHit();
        }
        else if (collision.gameObject.layer == dedLayer)
        {
            player.OnDeath();
        }
    }
}
