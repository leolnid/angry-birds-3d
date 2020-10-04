using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class BaseBird : MonoBehaviour
{
  [FormerlySerializedAs("_damage")] [SerializeField]
  protected float damage;
  protected float CollisionDamageMultiplier = 0.01f;
  protected float Health;
  protected Renderer Renderer;
  protected int SkillStock;

  protected Rigidbody Rigidbody;
  protected Material Material;

  public abstract void Start();
  public abstract void Skill();

  public virtual void Launch(Vector3 direction)
  {
    Rigidbody.isKinematic = false;
    Rigidbody.useGravity = true;

    Debug.Log("Force:" + direction);
    Debug.Log("RB:" + Rigidbody.velocity);
    Rigidbody.AddForce(direction);
    Debug.Log("RB:" + Rigidbody.velocity);
  }

  public virtual void Awake()
  {
    Rigidbody = GetComponent<Rigidbody>();
    Renderer = GetComponent<Renderer>();
    Material = Renderer.material;
    Rigidbody.isKinematic = true;
    Rigidbody.useGravity = false;
  }

  protected virtual void Init(
      Color skin,
      float damage = 1f,
      int skillStock = 1)
  {
    Material.SetColor("_Color", skin);
    this.damage = damage;
    SkillStock = skillStock;
  }

  void OnCollisionEnter(Collision other)
  {
    InterractiveObject obj = other.collider.GetComponent<InterractiveObject>();
    if (obj != null)
    {
      obj.TakeDamage(this.damage);
    }
  }

  void OnCollisionStay(Collision other)
  {
    InterractiveObject obj = other.collider.GetComponent<InterractiveObject>();
    if (obj != null)
    {
      obj.TakeDamage(this.damage * CollisionDamageMultiplier);
    }
  }

  public Vector3[] SimulatePath(Vector3 force, int size, float stepSize = 0.1f, int maxIterations = 10000)
  {
    float timestep = Time.fixedDeltaTime * stepSize;
    float stepDrag = 1 - Rigidbody.drag * timestep;
    // TODO: Find where 0.04f was lost. Magic!    
    Vector3 velocity = force * 0.04f / Rigidbody.mass * timestep + Rigidbody.velocity;
    Vector3 gravity = Physics.gravity * timestep * timestep;
    Vector3 position = Rigidbody.position;

    Vector3[] path = new Vector3[size];
    int curPathNumber = 0;

    for (int i = 0; i < maxIterations && curPathNumber < size; i++)
    {
      if (i % 10 == 0)
      {
        path[curPathNumber] = position;
        curPathNumber++;
      }

      velocity += gravity;
      velocity *= stepDrag;

      position += velocity;
    }

    return path;
  }
}
