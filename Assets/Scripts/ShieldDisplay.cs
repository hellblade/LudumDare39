using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShieldDisplay : MonoBehaviour
{

    [SerializeField] Health shield;
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;

    Image image;
   

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = shield.ShieldEnabled ? on : off;
    }
}

