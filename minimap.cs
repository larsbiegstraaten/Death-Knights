using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimap : MonoBehaviour
{
    public Transform Player;
    public PhotonView photonView;
    public GameObject Pedro;
    public Camera MinimapCam;
    public RawImage rawImage;
    public alma alma;
    public GameObject miAlma;
    public GameObject canvas;
    public GameObject padre;
    public GameObject mapHolder;

    private void Start()
    {
        alma = GameObject.FindGameObjectWithTag("alma").GetComponent<alma>();
        miAlma = alma.miAlma;
        Player = transform.parent.Find("Pedro Piquero de " + miAlma.GetComponent<alma>().nickname);
        Pedro = Player.gameObject;
        photonView = Pedro.GetPhotonView();
        MinimapCam = gameObject.GetComponent<Camera>();
        StartCoroutine(camarasMiniMap());
        
    }

    private void LateUpdate()
    {

        if (photonView.IsMine)
        {
            Vector3 newPosition = Player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            
            //Vector3 direccion = Player.position - transform.position;

            //float angleRad = Mathf.Atan2(direccion.y, direccion.x);
            //float angleDeg = angleRad * Mathf.Deg2Rad;
            //Flecha.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
            
    }

    public IEnumerator camarasMiniMap()
    {
        yield return new WaitForSeconds(1f);
        if (photonView.IsMine)
        {
            RenderTexture playerMinimapTexture = new RenderTexture(100, 100, 0);
            playerMinimapTexture.name = "PlayerMinimapTexture_" + photonView.Owner.NickName;
            MinimapCam.targetTexture = playerMinimapTexture;
            padre = GameObject.Find("Pedro Piquero de " + miAlma.GetComponent<alma>().nickname.ToString());
            canvas = padre.transform.Find("Canvas").gameObject;
            mapHolder = canvas.transform.Find("CanvasMiniMapa").gameObject;
            rawImage = mapHolder.GetComponent<RawImage>();


            if (rawImage != null)
            {
                Debug.Log("Hay textura");
                rawImage.texture = playerMinimapTexture;
            }
            else
            {
                Debug.Log("No hay textura");
            }
        }
    }
}
