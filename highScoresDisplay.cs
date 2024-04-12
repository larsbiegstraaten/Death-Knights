using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class highScoresDisplay : MonoBehaviour
{
    public GameObject highScoreRowPrefab;
    public Transform highScoreContainer;

    public void DisplayHighScores(List<string> nicknames, List<int> kills, List<int> oleadas)
    {
        // Ensure the container is empty to start with
        foreach (Transform child in highScoreContainer)
        {
            Destroy(child.gameObject);
        }

        // Assume all lists are the same length
        for (int i = 0; i < nicknames.Count; i++)
        {
            // Instantiate a new high score row for each entry
            GameObject newRow = Instantiate(highScoreRowPrefab, highScoreContainer);

            // Find the TextMeshProUGUI components and set their text
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (i + 1).ToString();
            texts[1].text = nicknames[i];
            texts[2].text = oleadas[i].ToString(); 
            texts[3].text = kills[i].ToString(); 
             
        }
    }
}
