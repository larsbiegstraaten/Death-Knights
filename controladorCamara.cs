using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorCamara : MonoBehaviour
{
    public Transform objetivo; // El objeto que la c�mara debe seguir
    public float suavizado = 5f; // Controla la suavidad del seguimiento de la c�mara

    private Vector3 offset; // La distancia entre la c�mara y el objetivo

    void Start()
    {
        offset = transform.position - objetivo.position;
    }

    void FixedUpdate()
    {
        // Calcula la posici�n objetivo de la c�mara
        Vector3 posicionObjetivo = objetivo.position + offset;

        // Aplica suavizado al seguimiento
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado * Time.deltaTime);
    }
}
