using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionCamara : MonoBehaviour
{

    public Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Convierte las coordenadas del cursor en pantalla en coordenadas del mundo
        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));

        // Calcula la direccion hacia donde mirar
        Vector3 lookDirection = cursorPos - transform.position;

        // Haz que el personaje mire hacia la direccion del cursor
        transform.forward = lookDirection.normalized;
        

        // Opcional: Limita la rotacion en ciertos ejes si es necesario
        Vector3 newEulerAngles = transform.eulerAngles;
        newEulerAngles.y = 0f; // Manten la rotacion en el eje X en 0 grados
        newEulerAngles.x = 0f; // Manten la rotacion en el eje Z en 0 grados
        transform.eulerAngles = newEulerAngles;
        mainCamera.transform.forward = lookDirection.normalized;
    }
}
