using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    private void Start() {
        Init();
    }

    public void Init()
    {
        GameManager.instance.timeController.AddTime(this);
    }
    
    public void Invoke()
    {
        onTimeTick?.Invoke();
    }

    private void OnDestroy() {
        GameManager.instance.timeController.RestTime(this);
    }

}
