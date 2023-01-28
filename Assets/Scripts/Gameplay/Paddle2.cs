using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Paddle2 : MonoBehaviour {
  public float movementSpeed = 1;
  public float width = 1;

  private PlayerControls _controls;
  private float _movement;

  void Awake() {
    _controls = new PlayerControls();
    _controls.Enable();
    _controls.Gameplay.Enable();
  }

  void OnEnable() => _controls.Gameplay.Move1.performed += OnMove;
  void OnDisable() => _controls.Gameplay.Move1.performed -= OnMove;
  void OnMove(InputAction.CallbackContext ctx) => _movement = ctx.ReadValue<float>();

  void Update() {
    // Move the paddle based on the current input value.
    transform.position += movementSpeed * Time.deltaTime * Vector3.zero.SetY(_movement);
    var position = transform.position;

    // calculate the size of the paddle.
    // Divide in 2 so we can use it to prevent paddle from leaving screen.
    var size = transform.localScale.y / 2;

    // If the position of "origin" or center of the paddle, plus it's height is greater than the
    // size of the screen, we have hit the top or bottom.
    var hitTopOfScreen = position.y + size > GameState.screenSize.y;
    var hitBottomOfScreen = position.y - size < -GameState.screenSize.y;

    // If we hit either the top or the bottom, keep the y portion of the position from going past the screen.
    if (hitTopOfScreen) {
      transform.position = position.SetY(GameState.screenSize.y - size);
    }
    if (hitBottomOfScreen) {
      transform.position = position.SetY(-GameState.screenSize.y + size);
    }
  }
}
