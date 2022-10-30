using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Dialogue/Actor")]
public class Actor : ScriptableObject
{
    //this Script only gets Name and face of the NPC
    public string Name;
    public Sprite portrait;
}
