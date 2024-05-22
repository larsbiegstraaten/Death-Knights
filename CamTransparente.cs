using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTransparente : MonoBehaviour
{
    private TransparentObjects _fader;
    private GameObject _currentObject; // Variable para almacenar el objeto detectado actualmente
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        _currentObject = null; // Inicializamos el objeto actual como nulo
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Ray ray = new Ray(cam.transform.position, player.transform.position - cam.transform.position);
            Debug.DrawRay(cam.transform.position, player.transform.position - cam.transform.position, Color.blue);
            RaycastHit trans;
            if (Physics.Raycast(ray, out trans))
            {
                if (trans.collider != null)
                {
                    if (trans.collider.gameObject == player)
                    {
                        // No hay nada entre la cámara y el jugador
                        if (_fader != null)
                        {
                            _fader.doFade = false;
                        }
                    }
                    else
                    {
                        if (_currentObject != null && _currentObject != trans.collider.gameObject)
                        {
                            // Restaurar la transparencia del objeto anterior si cambia
                            TransparentObjects previousFader = _currentObject.GetComponent<TransparentObjects>();
                            if (previousFader != null)
                            {
                                previousFader.doFade = false;
                            }
                        }

                        _fader = trans.collider.gameObject.GetComponent<TransparentObjects>();
                        if (_fader != null)
                        {
                            _fader.doFade = true;
                            _currentObject = trans.collider.gameObject; // Actualizamos el objeto actual
                        }
                    }
                }
            }
        }
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CamTransparente : MonoBehaviour
//{
//    private List<TransparentObjects> _faders; // Lista para almacenar los objetos detectados
//    private GameObject _player;
//    public Camera cam;

//    // Start is called before the first frame update
//    void Start()
//    {
//        _faders = new List<TransparentObjects>();
//        _player = GameObject.FindGameObjectWithTag("Player");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (_player != null)
//        {
//            Ray ray = new Ray(cam.transform.position, _player.transform.position - cam.transform.position);
//            Debug.DrawRay(cam.transform.position, _player.transform.position - cam.transform.position, Color.blue);
//            RaycastHit[] hits = Physics.RaycastAll(ray);

//            foreach (RaycastHit hit in hits)
//            {
//                if (hit.collider != null)
//                {
//                    GameObject obj = hit.collider.gameObject;
//                    if (obj != _player && obj.GetComponent<TransparentObjects>() != null)
//                    {
//                        TransparentObjects fader = obj.GetComponent<TransparentObjects>();
//                        if (!_faders.Contains(fader))
//                        {
//                            // Agregar el objeto a la lista si no está presente
//                            _faders.Add(fader);
//                        }
//                    }
//                }
//            }

//            // Aplicar transparencia a todos los objetos detectados
//            foreach (TransparentObjects fader in _faders)
//            {
//                fader.doFade = true;
//            }
//        }
//    }
//}


