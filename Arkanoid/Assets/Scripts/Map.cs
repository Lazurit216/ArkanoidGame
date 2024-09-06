using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private float rightOffset=0.5f;
    [SerializeField] private float leftOffset = 0.5f;
    private Camera m_Camera;
    [HideInInspector] public float maxX { get; protected set; }
    public float minX { get; protected set; }

    public static Map Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_Camera = Camera.main;

        Vector2 max = m_Camera.ViewportToWorldPoint(new Vector2(1, 1)); // top-right corner
        maxX = max.x - rightOffset;
        Vector2 min = m_Camera.ViewportToWorldPoint(new Vector2(0, 0)); 
        minX = min.x + leftOffset;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(minX, -10), new Vector2(minX, 10));
        Gizmos.DrawLine(new Vector2(maxX, -10), new Vector2(maxX, 10));
    }*/
}
