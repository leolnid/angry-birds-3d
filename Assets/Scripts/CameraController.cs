using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
  [FormerlySerializedAs("_WASDSpeed")] [SerializeField] private float wasdSpeed = 10f;
  [FormerlySerializedAs("_MouseSpeed")] [SerializeField] private float mouseSpeed = 35f;

  void Update()
  {
    float xAxisValue = Input.GetAxis("Horizontal");
    float zAxisValue = Input.GetAxis("Vertical");
    Vector2 scale = Input.mouseScrollDelta;

    if (Camera.main != null)
    {
      Camera.main.transform.Translate(
          (new Vector3(xAxisValue, 0.0f, zAxisValue) * wasdSpeed +
          new Vector3(scale.x, 0.0f, scale.y) * mouseSpeed) * Time.deltaTime);
    }
  }
}
