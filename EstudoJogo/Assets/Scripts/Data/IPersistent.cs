using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistent
{
    public string Read();
    public void Load(string jsonString);
}
