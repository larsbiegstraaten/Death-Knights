using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class DynamicButtonLinker : MonoBehaviourPun
{
    // Reference to the button, can be set in the inspector if this script is on a GameObject in the scene
    // or found dynamically if the script runs on the instantiated prefab
    public Button sceneTransitionButton;
    public Button makeTeamsButton;
    public alma alma;

    void Start()
    {
        alma = GetComponent<alma>();
        StartCoroutine(findButton());
    }

    public void OnButtonLoadMainSceneClicked()
    {
        // Make sure this object has a PhotonView component
        PhotonView photonView = GetComponent<PhotonView>();

        if (photonView != null)
        {
            // Call the CargarMainScene method as an RPC, targeting all connected players
            //photonView.RPC("CargarMainScene", RpcTarget.All);
            if (photonView.IsMine && PhotonNetwork.IsMasterClient) {
                PhotonNetwork.LoadLevel("MainScene");
            }
        }
        else
        {
            Debug.LogError("PhotonView not found on this GameObject");
        }
    }
    
    IEnumerator findButton()
    {
        yield return new WaitForSeconds(0.5f);
        if (sceneTransitionButton == null)
        {
            sceneTransitionButton = GameObject.Find("CanvasInicial/PanelInicial/ButtonPlay").GetComponent<Button>();
        }
        sceneTransitionButton.onClick.AddListener(OnButtonLoadMainSceneClicked);

        if (makeTeamsButton == null)
        {
            makeTeamsButton = GameObject.Find("CanvasInicial/PanelInicial/ButtonTeams").GetComponent<Button>();
        }
        makeTeamsButton.onClick.AddListener(makeTeams);
    }

    public void makeTeams()
    {
        Debug.Log("Make teams salta");
        photonView.RPC("BuscarAlmas", RpcTarget.All);
        
    }
}

