using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance = null;
  public SlingShotController slingShotController;
  public bool isInGame = false;

  private List<Pig> _pigList;

  private Vector3 _firstClick;
  private bool _isHelpsDraw = false;

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance == this)
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }

  public void Register(SlingShotController controller)
  {
    slingShotController = controller;

    if (slingShotController.HasBird())
      slingShotController.NextBird();

    isInGame = true;
  }

  public void Register(Pig pig)
  {
    if (_pigList == null)
      _pigList = new List<Pig>();

    _pigList.Add(pig);
  }

  public void Update()
  {
    if (isInGame && Input.GetMouseButtonDown(0))
    {
      _firstClick = Input.mousePosition;
      _isHelpsDraw = true;
    }

    if (isInGame && _isHelpsDraw)
    {
      slingShotController.DrawLine(_firstClick - Input.mousePosition);
    }

    if (isInGame && Input.GetMouseButtonUp(0))
    {
      slingShotController.LaunchBird(_firstClick - Input.mousePosition);
      _isHelpsDraw = false;
    }

    if (isInGame && Input.GetKeyDown(KeyCode.Space))
      slingShotController.UseLastBirdSkill();

    if (isInGame && Input.GetMouseButtonDown(1))
      this.slingShotController.NextBird();

  }
}
