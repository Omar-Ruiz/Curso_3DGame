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

    private Vector3 respawnPos = Vector3.zero;

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
               

                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                inGround = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (dead)
        {
            
            return;
        }

        if(rb.velocity.y >= 1)
        {
            inGround = false;
            animator.SetBool("jump", true);
        }




        float suavizado = 1f - Mathf.Exp(-suavizadoGiro * Time.fixedDeltaTime);
        float giroAplicado = Mathf.Clamp(giroPendiente * suavizado, -maxGiroPorFrameFisico, maxGiroPorFrameFisico);

        Quaternion turn = Quaternion.Euler(0f, giroAplicado, 0f);
        rb.MoveRotation(rb.rotation * turn);
        giroPendiente -= giroAplicado;

        if(verticalInput > 0)
        {
            
            animator.SetBool("run", true);
            Vector3 movement = transform.forward * verticalInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            animator.SetBool("run", false);
        }
        

    }

    void OnCollisionEnter(Collision collision)
    {
        
        
      
        if(collision.gameObject.tag == "Piso")
        {
            animator.SetBool("jump", false);   
            
            inGround = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        
        
        if(collider.gameObject.tag == "Deadly")
        {
            Debug.Log("auch");
            dead = true;
            
            animator.SetBool("die", true);  

            Respawn();
        }

        if(collider.gameObject.tag == "Respawn")
        {
            Debug.Log("Toque un respawn");

            respawnPos = transform.position;
            
        }



        if(collider.gameObject.tag == "Finish")
        {
            Debug.Log("yupiiiii");
            GameManager gm =  FindObjectOfType<GameManager>();
            gm.victory = true;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            animator.SetTrigger("victory");  
        }
        
    }


    void Respawn(){
        animator.SetBool("die", false);  
        dead = false;
        rb.velocity = Vector3.zero;
        transform.position= respawnPos;
    }

}
