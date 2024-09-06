using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private Vector2 startPos;
    private Camera m_Camera;
    private Rigidbody2D m_Rigidbody;
    private Map map;

    [field: SerializeField] public float minForce { get; private set; }
    [field: SerializeField] public float maxForce { get; private set; }
    [SerializeField] private float forceCoof = 3;

    private bool canMove;
    public bool isStarted {  get; private set; }

    private bool isLerp;
    [SerializeField] private float lerpTime = 0.2f;
    [SerializeField] private float lerpSpeed = 10;


    private float angle;
    private float force;

    private Player player;

    public event BallKicked onBallKicked;
    public delegate void BallKicked(float angle, float force);

    public event MousePressed onMousePressed;
    public delegate void MousePressed(float angle, float force);

    public static PlayerController Instance;
    private void Awake()
    {
        Instance=this;
    }

    private void Start()
    {
        m_Camera = Camera.main;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        map=Map.Instance;
        
        angle = 0;
        force = 5;

        player=Player.Instance;
        player.onDed += SetStartOpt;

        SetStartOpt();
    }
    private void SetStartOpt()
    {
        canMove = true;
        isStarted = false;
        isLerp = false;

        transform.position = startPos;
    }

    private void Update()
    {
        StartClick();
        if(isLerp) transform.position = Vector2.Lerp(transform.position, GetMouseCoords(), Time.deltaTime * lerpSpeed);
    }
    private void FixedUpdate()
    {
        Moving(GetMouseCoords());
    }

    private void Moving(Vector2 newPos)
    {
        if (canMove)
        {
            m_Rigidbody.MovePosition(newPos);
        }
    }
    private void StartClick()
    {

        Vector3 newMousePos;
        Vector3 WNewMousePos;

        if (!isStarted)
        {
            if (Input.GetMouseButton(1))
            {
                canMove = false;
                newMousePos = Input.mousePosition;
                WNewMousePos = m_Camera.ScreenToWorldPoint(newMousePos);
                Vector2 direction = WNewMousePos - transform.position;
                angle = Vector2.Angle(Vector2.right, direction)-90;
                if (transform.position.y > WNewMousePos.y) angle *= -1;

                force = Mathf.Abs(direction.y - transform.position.y) / forceCoof;
                if (force>maxForce) force=maxForce;
                if (force<minForce) force=minForce;
                //Debug.Log(force);


                onMousePressed?.Invoke(angle, force);

            }
            if (Input.GetMouseButtonUp(1))
            {
                isStarted = true;
                StartCoroutine(MoveToCursor());

                onBallKicked?.Invoke(angle, force);
            }
        }
    }
    private IEnumerator MoveToCursor()
    {
        isLerp = true;
        yield return new WaitForSeconds(lerpTime);
        canMove = true;
        isLerp = false;
    }

    private Vector2 GetMouseCoords()
    {
        Vector2 screenMousePosition = Input.mousePosition;
        Vector2 worldMousePosition = m_Camera.ScreenToWorldPoint(screenMousePosition);
        Vector2 platformPosition = new Vector2(worldMousePosition.x * sensitivity, startPos.y);
        if ((platformPosition.x) < map.minX)
        {
            float newX = map.minX ;
            platformPosition = new Vector2(newX, startPos.y);
        }
        if ((platformPosition.x) > map.maxX)
        {
            float newX = map.maxX;
            platformPosition = new Vector2(newX, startPos.y);
        }

        return platformPosition;
    }
    }
