using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SlingShotController : MonoBehaviour
{
  [Header("Base Setup")]
  [SerializeField] private Bird[] availableTypeBirds;
  private List<BaseBird> _availableBaseBirds;


  [Space(20)]
  public Vector3 birdsActivePosition;
  public Vector3 birdsInactiveStartPosition;
  public Vector3 birdsInactiveOffset;

  [SerializeField] private float birdAcceleration = 2f;
  [SerializeField] private int numberOfHelpSegments = 100;

  private Vector3 _currentInactiveBirdPosition;
  private BaseBird _currentBird;
  private bool _isLaunch;

  private LineRenderer _lineRenderer;

  void Start()
  {
    _availableBaseBirds = new List<BaseBird>();
    _currentInactiveBirdPosition = birdsInactiveStartPosition;

    foreach (Bird birdType in availableTypeBirds)
    {
      BaseBird bird = BirdFactory.Create(birdType);
      bird.gameObject.transform.SetParent(transform);

      bird.gameObject.transform.position = transform.position + _currentInactiveBirdPosition;
      _currentInactiveBirdPosition += birdsInactiveOffset;

      _availableBaseBirds.Add(bird);
    }

    _lineRenderer = GetComponent<LineRenderer>();
    GameManager.Instance.Register(this);
  }

  public void UseLastBirdSkill()
  {
    if (_isLaunch)
    {
      _currentBird.Skill();
    }
  }

  public void LaunchBird(Vector3 vector3)
  {
    if (!_isLaunch)
    {
      _currentBird.Launch(vector3 * birdAcceleration);
      _isLaunch = true;
    }
  }

  public bool HasBird()
  {
    return _availableBaseBirds.Count > 0;
  }

  public BaseBird NextBird()
  {
    _currentBird = _availableBaseBirds[0];
    _currentBird.transform.position = transform.position + birdsActivePosition;
    _availableBaseBirds.RemoveAt(0);

    StartCoroutine(MoveAllBirds());
    _isLaunch = false;
    return _currentBird;
  }

  public void DrawLine(Vector3 vector3)
  {
    if (!_isLaunch)
    {
      Vector3[] path = _currentBird.SimulatePath(vector3, numberOfHelpSegments);
      _lineRenderer.positionCount = path.Length;

      for (int i = 0; i < path.Length; i++)
        _lineRenderer.SetPosition(i, path[i]);
    }

  }

  private IEnumerator MoveAllBirds()
  {
    for (int i = 0; i < _availableBaseBirds.Count; i++)
    {
      GameObject bird = _availableBaseBirds[i].gameObject;
      bird.transform.position = transform.position + birdsInactiveStartPosition + birdsInactiveOffset * i;
      yield return new WaitForSeconds(0.1f);
    }
    yield return null;
  }
}
