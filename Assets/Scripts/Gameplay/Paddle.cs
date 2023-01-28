using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour {
  public float movementSpeed = 1;
  public float width = 1;

  private PlayerControls _controls;
  private float _movement;

  void Start() {
    _controls = new PlayerControls();
    _controls.Enable();
    _controls.Gameplay.Enable();

    _controls.Gameplay.Move.performed += ctx => _movement = ctx.ReadValue<float>();

  }

  void Update() {
    transform.position += movementSpeed * Time.deltaTime * new Vector3(0, _movement, 0);

    var pos = transform.position;
    if (pos.y > 20) {
      transform.position = new Vector3(pos.x, 20, pos.z);
    }
    if (pos.y < -20) {
      transform.position = new Vector3(pos.x, -20, pos.z);
    }

    // transform.localScale = new Vector3(1, width, 1);
  }
}
