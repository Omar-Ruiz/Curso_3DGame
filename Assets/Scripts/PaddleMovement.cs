using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    public float pingPongSpeed = 1f;
    [SerializeField] Vector2 DragDistance = new Vector2(0f, 0f);

    private Vector3 center;

    void Awake()
    {
        center = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DragDistance == Vector2.zero || pingPongSpeed == 0f)
        {
            return;
        }

        if(DragDistance.x != 0f && DragDistance.y == 0f)
        {
            Movement();
        }

        if(DragDistance.x == 0f && DragDistance.y != 0f)
        {
            Movement(DragDistance.y);
        }
        if(DragDistance.x != 0f && DragDistance.y != 0f)
        {
            Movement(DragDistance.x, DragDistance.y);
        }

    
    }

    //Movimiento base en el eje X, con la función PingPong de Unity para hacer un movimiento oscilante 
    void Movement()
    {
       transform.position = new Vector3(Mathf.PingPong(Time.time*pingPongSpeed, DragDistance.x)+center.x, transform.position.y, transform.position.z);
    }

    //Movimiento base en el eje Y, con la función PingPong de Unity para hacer un movimiento oscilante
    void Movement(float DragDistanceY)
    {
       transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time*pingPongSpeed, DragDistanceY)+center.y, transform.position.z);
    }

    //Movimiento en ambos ejes, con la función PingPong de Unity para hacer un movimiento oscilante
    void Movement(float DragDistanceX, float DragDistanceY)
    {
       transform.position = new Vector3(Mathf.PingPong(Time.time*pingPongSpeed, DragDistanceX)+center.x, Mathf.PingPong(Time.time*pingPongSpeed, DragDistanceY)+center.y, transform.position.z);
    }

    
    /*
    private Vector3 startMarker;
    private Vector3 endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    void Awake() {
        
        startMarker = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endMarker = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }
    void Start() {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }
    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }*/
}
