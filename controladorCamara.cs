using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorCamara : MonoBehaviour
{
    public Transform objetivo; // El objeto que la cámara debe seguir
    public float suavizado = 5f; // Controla la suavidad del seguimiento de la cámara

    private Vector3 offset; // La distancia entre la cámara y el objetivo

    void Start()
    {
        offset = transform.position - objetivo.position;
    }

    void FixedUpdate()
    {
        // Calcula la posición objetivo de la cámara
        Vector3 posicionObjetivo = objetivo.position + offset;

        // Aplica suavizado al seguimiento
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado * Time.deltaTime);
    }
}
