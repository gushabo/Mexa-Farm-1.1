using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusoPared : MonoBehaviour
{
    //points and lifes
    [SerializeField] public float lives = 5f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("frutas")){
            Destroy(other.gameObject);
            LessLives();
        }
    }

    public void LessLives()
    {
        lives -= 1;
    }

}
