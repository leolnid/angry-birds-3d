using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : BaseBird
{
  public override void Skill()
  {
    if (SkillStock > 0)
    {
      Rigidbody.velocity *= 2f;
      SkillStock -= 1;
    }
  }

  public override void Start()
  {
    Init(Color.yellow);
  }
}
