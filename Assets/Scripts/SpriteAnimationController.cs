using System;
using System.Collections.Generic;

using UnityEngine;

public class SpriteAnimationController : MonoBehaviour
{
    public void PlayAnimationAt(SpriteAnimation animationPrefab, Transform location)
    {
        Instantiate(animationPrefab, location.position, Quaternion.identity);
    }

    public void PlayAnimationScaledAt(SpriteAnimation animationPrefab, SpriteRenderer location)
    {
        var size = location.sprite.bounds.size;
        var animatedSprite = Instantiate(animationPrefab, location.transform.position, Quaternion.identity);
        animatedSprite.transform.localScale = new Vector3(size.x / animatedSprite.Bounds.x, size.y / animatedSprite.Bounds.y, 0);
    }
}