using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class BombermanTests
{
    private GameObject game; // Stores Instance of Entire Game
    private Player player1, player2;

    [SetUp]
    void SetUp()
    {
        GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(gamePrefab);

        Player[] players = Object.FindObjectsOfType<Player>();

        foreach(var player in players)
        {
            if(player.playerNumber == 1)
            {
                player1 = player;
            }
            else if(player.playerNumber == 2)
            {
                player2 = player;
            }
        }
    }

    [TearDown]
    void TearDown()
    {

    }
}
