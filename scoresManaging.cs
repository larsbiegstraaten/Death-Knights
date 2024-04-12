using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoresManaging : MonoBehaviour
{
    public TextMeshProUGUI TscoreTeam1;
    public TextMeshProUGUI TscoreTeam2;
    public int scoreTeam1;
    public int scoreTeam2;

    public bool enMainScene = false;

    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        photonView = GetComponent<PhotonView>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            enMainScene = true;
            TscoreTeam1 = GameObject.FindGameObjectWithTag("Tscore1").GetComponent<TextMeshProUGUI>();
            TscoreTeam2 = GameObject.FindGameObjectWithTag("Tscore2").GetComponent<TextMeshProUGUI>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && enMainScene)
        {
            photonView.RPC("updateScore", target: RpcTarget.All);
        }
    }
    [PunRPC]
    public void updateScore()
    {
        TscoreTeam1.text = scoreTeam1.ToString();
        TscoreTeam2.text = scoreTeam2.ToString();
    }

}
