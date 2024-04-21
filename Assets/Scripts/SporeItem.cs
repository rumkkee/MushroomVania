using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SporeType
{
    Normal,
    Ice,
    Fire,
    Teleport
}

public class SporeItem : MonoBehaviour
{
    public Sprite sporeSprite;

    public GameObject sporePrefab;

    public SporeType sporeType;

    public AudioClip SelectSound;

}
