using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [field: SerializeField] public List<NetworkObject> Players {get; private set;}

    public void AddPlayer(NetworkObject player)
    {
        if(Players.Contains(player))
        {
            Debug.LogError("Player is already added");
            return;
        }

        Players.Add(player);
    }

    public void RemovePlayer(NetworkObject player)
    {
        Players.Remove(player);
    }

   
}
