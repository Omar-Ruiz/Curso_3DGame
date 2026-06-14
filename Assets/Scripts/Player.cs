using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f, velocidadGiro = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float suavizadoGiro = 18f;
    [SerializeField] private float maxGiroPorFrameFisico = 8f;

    private Rigidbody rb;
    private float verticalInput;
    private float giroPendiente;
    private bool inGround = true;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        giroPendiente += Input.GetAxisRaw("Mouse X") * velocidadGiro;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (inGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                inGround = false;
            }
        }
    }

    void FixedUpdate()
    {
        float suavizado = 1f - Mathf.Exp(-suavizadoGiro * Time.fixedDeltaTime);
        float giroAplicado = Mathf.Clamp(giroPendiente * suavizado, -maxGiroPorFrameFisico, maxGiroPorFrameFisico);

        Quaternion turn = Quaternion.Euler(0f, giroAplicado, 0f);
        rb.MoveRotation(rb.rotation * turn);
        giroPendiente -= giroAplicado;

        Vector3 movement = transform.forward * verticalInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

    }

    void OnCollisionEnter(Collision collision)
    {
        inGround = true;
    }

}
