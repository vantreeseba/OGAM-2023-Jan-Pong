using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour {
  [Tooltip("This is the movement speed of the ball in m/s")]
  public float moveSpeed = 1;

  private Rigidbody _rigidbody;
  private PlayerControls _controls;

  // Setup a public event, so other game systems can know when the ball
  // has hit the left or right side of the screen.
  public static event System.Action<bool> OnBallEnterGoal;

  void Awake() {
    _rigidbody = GetComponent<Rigidbody>();

    _controls = new PlayerControls();
    _controls.Enable();
    _controls.Gameplay.Enable();

    // Reset the ball at the beginning of the game.
    StartCoroutine(ResetBall());
  }

  void OnEnable() => _controls.Gameplay.Restart.performed += OnRestart;
  void OnDisable() => _controls.Gameplay.Restart.performed -= OnRestart;
  private void OnRestart(InputAction.CallbackContext ctx) => StartCoroutine(ResetBall());

  void FixedUpdate() {
    var radius = 0.5f;
    var position = transform.position;

    var hitTopOfScreen = position.y + radius > GameState.screenSize.y;
    var hitBottomOfScreen = position.y - radius < -GameState.screenSize.y;

    if (hitTopOfScreen || hitBottomOfScreen) {
      // invert the y velocity, to make it "bounce".
      _rigidbody.velocity = _rigidbody.velocity.ScaleY(-1);
    }

    var hitLeftSideOfScreen = position.x - radius < -GameState.screenSize.x;
    var hitRightSideOfScreen = position.x + radius > GameState.screenSize.x;

    if (hitLeftSideOfScreen || hitRightSideOfScreen) {
      // trigger ball entering goal event.
      OnBallEnterGoal?.Invoke(hitLeftSideOfScreen);
      StartCoroutine(ResetBall());
    }
  }

  void OnCollisionEnter(Collision collision) {
    // Get the contact point.
    var contact = collision.GetContact(0);

    // Calculate the size of the paddle,
    // divide by two so we can tell if it hit the top part or bottom part.
    var paddleSize = contact.otherCollider.bounds.size.y / 2f;

    // Shift the size of the paddle from to 0..1, so we can use it to scale the velocity.
    var normalizedPaddleSize = 1f / paddleSize;

    // Get the vector pointing from the center of the paddle to the point where it was hit by the ball.
    var newVel = (contact.point - contact.otherCollider.bounds.center);

    // Scale the y part of the new velocity so that hitting the edge of the paddle makes it go faster
    // in the vertical direction.
    // The more towards the center it is, the more it reduces the y part of the velocity.
    newVel = newVel.ScaleY(normalizedPaddleSize);

    // Normalize the velocity so it's a unit vector (a vector with length 1).
    // Multiply that by the move speed to set the new velocity.
    _rigidbody.velocity = newVel.normalized * moveSpeed;
  }

  IEnumerator ResetBall() {
    // Place the ball in the center of the screen with 0 velocity.
    _rigidbody.velocity = Vector3.zero;
    _rigidbody.position = Vector3.zero;

    yield return new WaitForSeconds(0.5f);

    // After half a second, build a random velocity that prefers the x direction.
    var xVel = Random.Range(0.4f, 1) * Extensions.RandomSign();
    var yVel = Random.Range(0, 0.6f) * Extensions.RandomSign();

    // Normalize the vector, and apply the move speed, but initial velocity is half until it hits a paddle.
    _rigidbody.velocity = new Vector3(xVel, yVel, 0).normalized * moveSpeed / 2;
  }
}
