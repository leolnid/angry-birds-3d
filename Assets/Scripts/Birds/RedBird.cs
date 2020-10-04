using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBird : BaseBird
{
  public override void Skill()
  {
    if (SkillStock > 0)
    {
      Rigidbody.velocity = Vector3.zero;
      SkillStock -= 1;
    }
  }

  public override void Start()
  {
    Init(Color.red);
  }
}
