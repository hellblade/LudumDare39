using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpeedDisplay : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] Sprite low;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite boost;

    Image image;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        switch (player.CurrentSpeed)
        {
            case Speed.NoPower:
                image.sprite = low;

                break;
            case Speed.Medium:
                image.sprite = normal;

                break;
            case Speed.Boost:
                image.sprite = boost;

                break;
            default:
                break;
        }
    }
}

