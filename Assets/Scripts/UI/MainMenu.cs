using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
  public Button startButton;
  public Button exitButton;

  void Start() {
    startButton.onClick.AddListener(GameState.GoToGameplay);
    exitButton.onClick.AddListener(GameState.ExitGame);
  }
}
