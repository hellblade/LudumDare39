using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    GameManager manager;

	// Use this for initialization
	void Start ()
    {
        manager = FindObjectOfType<GameManager>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            manager.LevelCompleted();
        }
    }
}
