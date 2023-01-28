using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : Singleton {
  public static Vector2 screenSize { get; private set; }
  public static int playerOneScore { get; private set; }
  public static int playerTwoScore { get; private set; }

  void OnEnable() {
    GameObject.DontDestroyOnLoad(this);
    var controls = new PlayerControls();
    controls.Enable();
    controls.Gameplay.Enable();

    controls.Gameplay.Exit.performed += _ => ExitGame();

    var size = Screen.safeArea.size;
    var cam = Camera.main;
    screenSize = cam.ScreenToWorldPoint(new Vector3(size.x, size.y, -cam.transform.position.z));

    Ball.OnBallEnterGoal += UpdateScore;
  }

  void OnDisable() {
    Ball.OnBallEnterGoal -= UpdateScore;
  }

  void UpdateScore(bool enteredLeftGoal) {
    var playerOneScoreText = GameObject.Find("Player 1 Score").GetComponent<TMPro.TMP_Text>();
    var playerTwoScoreText = GameObject.Find("Player 2 Score").GetComponent<TMPro.TMP_Text>();

    // Going into left goal scores a point for right player (player 2).
    if (enteredLeftGoal) {
      playerTwoScore += 1;
      playerTwoScoreText.text = playerTwoScore.ToString();
    } else {
      playerOneScore += 1;
      playerOneScoreText.text = playerOneScore.ToString();
    }

    if (playerOneScore >= 2 || playerTwoScore >= 2) {
      GoToMainMenu();
    }
  }

  public static void ExitGame() {
    Application.Quit(0);

#if UNITY_EDITOR
    UnityEditor.EditorApplication.ExitPlaymode();
#endif
  }

  public static void GoToMainMenu() {
    playerOneScore = 0;
    playerTwoScore = 0;
    SceneManager.LoadScene("Main Menu");
  }

  public static void GoToGameplay() {
    playerOneScore = 0;
    playerTwoScore = 0;
    SceneManager.LoadScene("Gameplay");
  }
}
