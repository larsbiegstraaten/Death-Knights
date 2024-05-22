using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public TMP_InputField inputfield;
    public GameObject Message;
    public GameObject Content;
    public Canvas chatCompleto;
    public Lobby Lobby;
    // Start is called before the first frame update
    void Start()
    {
      Lobby = GameObject.Find("Lobby").GetComponent<Lobby>();
      inputfield.interactable = true;
        chatCompleto.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
               chatCompleto.enabled = !chatCompleto.enabled;
        }
        if (chatCompleto.enabled)
        {

            inputfield.Select();
            inputfield.ActivateInputField();
            inputfield.interactable = true;
        }
        else 
        {
            inputfield.interactable = false;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            SendMessage();
        }
    }

    public void SendMessage()
    {
        Debug.Log("Pulsado boton");
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, ( " : " + inputfield.text));
        inputfield.text = " ";
     
    }

    [PunRPC]
    public void GetMessage(string ReceiveMessage)
    {
        Debug.Log("PASO");
        GameObject M = Instantiate(Message, Content.transform.position, Quaternion.identity, Content.transform);
        M.GetComponent<Message>().MiTexto.text = ReceiveMessage;
    }
}
