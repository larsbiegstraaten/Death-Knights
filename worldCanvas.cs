using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldCanvas : MonoBehaviour
{
    public Transform MainCam;
    public Camera cam;

    public Transform worldSpaceCanvas;

    public Controlador controlador;
    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(CamaraVidayNombre());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - MainCam.transform.position);

    }

    public IEnumerator CamaraVidayNombre()
    {
        yield return new WaitForSeconds(1f);
        controlador = GetComponentInParent<Controlador>();
        MainCam = Camera.main.transform;
        transform.SetParent(worldSpaceCanvas);
    }
}
