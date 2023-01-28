using UnityEngine;

public class Singleton : MonoBehaviour {
  private static Singleton _instance;

  void Awake() {
    if (_instance != null && _instance != this) {
      Destroy(gameObject);
    } else {
      _instance = this;
    }
  }
}