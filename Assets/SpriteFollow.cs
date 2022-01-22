using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollow : MonoBehaviour
{
    public Transform transformToFollow;
    public GameObject sprite;

    private void Awake()
    {
        Instantiate(sprite);
    }

    private void Update()
    {
        sprite.transform.localPosition = transformToFollow.localPosition;
    }
}
