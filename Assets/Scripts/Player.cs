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

    bool dead = false;

    private Animator animator;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        if (dead)
        {
            return;
        }
        verticalInput = Input.GetAxis("Vertical");
        giroPendiente += Input.GetAxisRaw("Mouse X") * velocidadGiro;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (inGround)
            {
                animator.SetBool("jump", true);
                animator.SetBool("idle", false);
                animator.SetBool("run", false);

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

        if(verticalInput > 0)
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", true);
            Vector3 movement = transform.forward * verticalInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
        }
        

    }

    void OnCollisionEnter(Collision collision)
    {
        
        
        if(collision.gameObject.tag == "Deadly")
        {
            Debug.Log("auch");
            dead = true;
            animator.SetBool("die", true);  
        }
        if(collision.gameObject.tag == "Piso")
        {
            animator.SetBool("jump", false);   
            animator.SetBool("idle", true);   
            inGround = true;
        }
    }

}
