using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : InterractiveObject
{
  protected override void Start()
  {
    base.Start();
    health = 5f;
  }
}
