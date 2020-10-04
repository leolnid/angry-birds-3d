using System;
using UnityEngine;

public static class BirdFactory
{
  public static BaseBird Create(Bird birdType)
  {
    Type type = Type.GetType(typeof(BaseBird).Namespace + "." + birdType.ToString(), throwOnError: false);

    if (type == null)
    {
      throw new InvalidOperationException(birdType.ToString() + " is not a known dto type");
    }

    if (!typeof(BaseBird).IsAssignableFrom(type))
    {
      throw new InvalidOperationException(type.Name + " does not inherit from AbstractDto");
    }

    GameObject newBird = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    newBird.transform.localScale = Vector3.one * 0.5f;
    newBird.AddComponent<Rigidbody>();
    newBird.AddComponent(type);

    return newBird.GetComponent<BaseBird>();
  }
}
