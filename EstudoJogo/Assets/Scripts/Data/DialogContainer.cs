using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialogue/Dialogue")]
public class DialogContainer : ScriptableObject
{
    public List<string> line;
    public Actor actor;
}
