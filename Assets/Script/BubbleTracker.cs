using System.Collections.Generic;
using UnityEngine;

public class BubbleTracker : MonoBehaviour
{
    public static List<GameObject> Bubbles = new List<GameObject>();

    void OnEnable()
    {
        Bubbles.Add(gameObject);
    }

    void OnDisable()
    {
        Bubbles.Remove(gameObject);
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (Bubbles.Count == 0)
        {
            Debug.Log("All bubbles are popped! Player wins!");

            // Find the player using the tag "Player1"
            GameObject player = GameObject.FindWithTag("Player1");
            if (player != null)
            {
                PlayerBehaviourScript playerController = player.GetComponent<PlayerBehaviourScript>();
                if (playerController != null)
                {
                    playerController.OnWin();
                }
            }
        }
    }
}
