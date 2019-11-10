using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class BombermanTests
{
    private GameObject game; // Stores Instance of Entire Game
    private Player[] players;

    // Method for getting reference to player by index
    public Player GetPlayer(int index)
    {
        // Loops through all players from SetUp function
        foreach (var player in players)
        {
            // Compares the playerNumber with given index
            if(player.playerNumber == index)
            {
                // Returns that player
                return player;
            }
        }
        // All else fails, return null
        return null;
    }
    [SetUp]
    public void SetUp()
    {
        GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(gamePrefab);
        players = Object.FindObjectsOfType<Player>();
    }

    // >> TESTS GO HERE <<
    [UnityTest]
    public IEnumerator Player1DropsBomb()
    {
        Player player1 = GetPlayer(1);

        // Simulate Bomb Dropping
        player1.DropBomb();

        // Wait for last frame
        yield return new WaitForEndOfFrame();

        // Check if Bomb Exists in the Scene
        Bomb bomb = Object.FindObjectOfType<Bomb>();

        // Bomb is not null
        Assert.IsTrue(bomb != null, "The Bomb didn't spawn");
    }

    [UnityTest]
    public IEnumerator Player2DropsBomb()
    {
        Player player2 = GetPlayer(2);

        // Simulate Bomb Dropping
        player2.DropBomb();

        // Wait for last frame
        yield return new WaitForEndOfFrame();

        // Check if Bomb Exists in the Scene
        Bomb bomb = Object.FindObjectOfType<Bomb>();

        // Bomb is not null
        Assert.IsTrue(bomb != null, "The Bomb didn't spawn");
    }

    [UnityTest]
    public IEnumerator Player1Moves()
    {
        Player player1 = GetPlayer(1);

        Vector3 oldPosition = player1.transform.position;

        player1.Move(true, false, false, false);

        yield return new WaitForFixedUpdate();

        Vector3 newPosition = player1.transform.position;

        Assert.IsTrue(oldPosition !=newPosition);
    }

    [UnityTest]
    public IEnumerator Player2Moves()
    {
        Player player2 = GetPlayer(2);

        Vector3 oldPosition = player2.transform.position;

        player2.Move(true, false, false, false);

        yield return new WaitForFixedUpdate();

        Vector3 newPosition = player2.transform.position;

        Assert.IsTrue(oldPosition != newPosition);
    }

    [UnityTest]
    public IEnumerator Player1Dies()
    {
        Player player1 = GetPlayer(1);

        player1.DropBomb();

        yield return new WaitForSeconds(5.1f);

        Player player = Object.FindObjectOfType<Player>();

        Assert.IsTrue(player != null, "Player 1 is not found.");
    }

    [TearDown]
    public void TearDown()
    {
        // Remove the Game from the Scene
        Object.Destroy(game);
    }
}
