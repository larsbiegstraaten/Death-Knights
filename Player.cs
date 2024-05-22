using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string Nickname;
    public int PlayerID;
    public string Password;

    // Constructor para inicializar el objeto
    public Player(string nickname, int playerID, string password)
    {
        Nickname = nickname;
        PlayerID = playerID;
        Password = password;
    }
}

// Ejemplo de cómo usar la clase Player en un MonoBehaviour
public class PlayerExample : MonoBehaviour
{
    void Start()
    {

    }


    void createPlayer(string name, int ID, string password)
    {
        Player player = new Player(name, ID, password);
    }
}
