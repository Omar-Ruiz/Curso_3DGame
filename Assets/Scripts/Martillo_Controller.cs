using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Martillo_Controller : MonoBehaviour
{
    public float grados = 45f;

    public float velocidad = 90f; // grados por segundo

    private Quaternion rotacionIzquierda;

    private Quaternion rotacionDerecha;

    private Quaternion rotacionObjetivo;
    private Rigidbody rb;

    void Start()

    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        Quaternion rotacionInicial = rb.rotation;

        rotacionIzquierda = rotacionInicial * Quaternion.Euler(0f, 0f, -grados);

        rotacionDerecha = rotacionInicial * Quaternion.Euler(0f, 0f, grados);

        rotacionObjetivo = rotacionDerecha;

    }

    void FixedUpdate()

    {
        Quaternion nuevaRotacion = Quaternion.RotateTowards(

            rb.rotation,

            rotacionObjetivo,

            velocidad * Time.fixedDeltaTime

        );

        rb.MoveRotation(nuevaRotacion);

        if (Quaternion.Angle(nuevaRotacion, rotacionObjetivo) < 0.1f)

        {

            if (rotacionObjetivo == rotacionDerecha)

            {

                rotacionObjetivo = rotacionIzquierda;

            }

            else

            {

                rotacionObjetivo = rotacionDerecha;

            }

        }

    }
}
