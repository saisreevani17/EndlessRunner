using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //if bullets touch the collider, destroy them
    {
        Destroy(collision.gameObject);
    }
}
