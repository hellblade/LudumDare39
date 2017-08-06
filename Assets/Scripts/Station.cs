using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Station : MonoBehaviour
{
    [SerializeField] float power = 1000;
    new SpriteRenderer renderer;

    [SerializeField] Sprite spriteWithPower;
    [SerializeField] Sprite spriteWithOutPower;
    [SerializeField] int scoreOnPickup;

    public bool HasPower
    {
        get { return power > 0; }
    }

    public Vector3 GetBounds()
    {
        return spriteWithPower.bounds.size;
    }

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    //private void LateUpdate()
    //{
    //    renderer.sprite = (power > 0) ? spriteWithPower : spriteWithOutPower;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (power <= 0)
            return;

        var energy = collision.gameObject.GetComponent<Power>();
        if (energy)
        {
            energy.GainPower(power);
            power = 0;
            renderer.sprite = spriteWithOutPower;

            if (collision.gameObject.GetComponent<PlayerController>())
            {
                GameManager.Instance.AddScore(scoreOnPickup);
            }
        }
    }
}
