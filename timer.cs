using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviourPun
{
    public TextMeshProUGUI timerText;
    private float timerDuration = 60f; 
    public PhotonView photonView;
    public controladorEscenas ce;

    public GameObject canvas3vs3;
    public GameObject selloGana1;
    public GameObject selloGana2;
    public GameObject selloPierde1;
    public GameObject selloPierde2;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            // Only the MasterClient starts the timer
            photonView.RPC("StartTimer", RpcTarget.AllBuffered, timerDuration);
        }
    }

    [PunRPC]
    public void StartTimer(float duration)
    {
        StartCoroutine(CountdownTimer(duration));
    }

    private IEnumerator CountdownTimer(float duration)
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            UpdateTimerDisplay(remainingTime);
            yield return new WaitForSeconds(1.0f);
            remainingTime--;
        }

        // Timer finished
        UpdateTimerDisplay(0);
        if (remainingTime <= 0)
        {
            if (ce.KillsT1 > ce.KillsT2)
            {
                FinPartida(1);
            }
            else if (ce.KillsT2 > ce.KillsT1)
            {
                FinPartida(2);
            }
            else
            {
                StartCoroutine(CountdownTimer(58f));
                // encender aqui un aviso de que empieza la prorroga
            }

        }        
                
        
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timerText != null)
        {
            timeToDisplay += 1;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void FinPartida(int t)
    {
        if(t == 1)
        {
            //ha ganado el team 1

            canvas3vs3.SetActive(true);
            selloGana1.SetActive(true);
            selloPierde2.SetActive(true);
        }
        else if(t == 2)
        {
            // ha ganado el team 2

            canvas3vs3.SetActive(true);
            selloGana2.SetActive(true);
            selloPierde1.SetActive(true);
        }
    }
    public void salirMenu()
    {
        SceneManager.LoadScene(0);
    }
}
