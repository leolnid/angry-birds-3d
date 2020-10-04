using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class InterractiveObject : MonoBehaviour
{
  [FormerlySerializedAs("Health")] [SerializeField]
  protected float health;

  protected Renderer Renderer;
  protected Material Material;

  protected virtual void Start()
  {
    Renderer = GetComponent<Renderer>();
    Material = Renderer.material;
    health = 1f;
  }

  public virtual void TakeDamage(float damage)
  {
    if (health <= damage)
      Destroy(gameObject);

    health -= damage;
  }
}
