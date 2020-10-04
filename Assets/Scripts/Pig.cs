using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : InterractiveObject
{
  protected override void Start()
  {
    base.Start();
    Material.SetColor("_Color", Color.green);

    GameManager.Instance.Register(this);
  }
}
