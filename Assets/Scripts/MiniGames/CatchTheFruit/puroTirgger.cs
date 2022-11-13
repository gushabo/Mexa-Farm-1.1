using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class puroTirgger : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterMovement>().AddPoints();
            Destroy(gameObject);
        }
    }
}
