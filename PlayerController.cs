using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    public float Velocidad;
    public PhotonView View;
    public GameObject Cam;
    public int Vida;
    public Gradient G;
    public SpriteRenderer Rend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (View.IsMine)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Velocidad * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.K)) {
                //RecibirDanio();
                View.RPC("RecibirDanio", RpcTarget.All);
            }
        }
        else {
            Cam.SetActive(false);
        }
        Rend.color = G.Evaluate(Vida / 100f);
    }
    [PunRPC]
    public void RecibirDanio() {
        Vida -= 10;
    }
}
