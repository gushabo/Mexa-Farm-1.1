using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puroTirgger : MonoBehaviour
{
    [SerializeField] CharacterMovement characterMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("frutas"))
        {
            Debug.Log("Choca con la fruta");
            Destroy(other.gameObject);
            characterMovement.points += 1;
        }
    }
}
