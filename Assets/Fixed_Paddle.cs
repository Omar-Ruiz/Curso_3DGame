using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fixed_Paddle : MonoBehaviour
{
    [SerializeField] private float velocidadGiro = 90f; // grados por segundo
    [SerializeField] private float toleranciaRotacion = 0.5f;

    bool needsFix = false;

    private Rigidbody rb;
    private Quaternion rotacionInicial;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotacionInicial = rb.rotation;
    }

    void FixedUpdate()
    {
        if (!needsFix)
        {
            return;
        }

        if (Quaternion.Angle(rb.rotation, rotacionInicial) <= toleranciaRotacion)
        {
            rb.angularVelocity = Vector3.zero;
            rb.MoveRotation(rotacionInicial);
            return;
        }

        Quaternion nuevaRotacion = Quaternion.RotateTowards(
            rb.rotation,
            rotacionInicial,
            velocidadGiro * Time.fixedDeltaTime
        );

        rb.angularVelocity = Vector3.zero;
        rb.MoveRotation(nuevaRotacion);
    }

    void OnCollisionStay(Collision collision)
    {
        needsFix = false;
    }

    void OnCollisionExit(Collision collision)
    {
        StartCoroutine(DelayFix());
    

    }
    IEnumerator DelayFix()
    {
        yield return new WaitForSeconds(0.5f);
        needsFix = true;
    }
}
