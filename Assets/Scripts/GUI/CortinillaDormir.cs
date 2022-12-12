using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CortinillaDormir : MonoBehaviour
{

    [SerializeField] GameObject go;
    [SerializeField] Animator anim;
    Sleep dormir;

    void Start()
    {
        dormir = GameManager.instance.player.GetComponent<Sleep>();
    }

    public void aparecerMensaje()
    {
        anim.Play("FadeIn");
        go.SetActive(true);
        GameManager.instance.timeController.cuentaTiempo = false;
    }

    public void CerrarMensaje()
    {
        go.SetActive(false);
        anim.Play("FadeOut");
        GameManager.instance.timeController.cuentaTiempo = true;
    }

}
