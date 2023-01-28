using UnityEngine;

public static class Extensions {
  /// <summary> Set the vectors X value. </summary>
  public static Vector3 SetX(this Vector3 vec, float value) {
    vec.x = value;
    return vec;
  }

  /// <summary> Set the vectors Y value. </summary>
  public static Vector3 SetY(this Vector3 vec, float value) {
    vec.y = value;
    return vec;
  }

  /// <summary> Set the vectors Z value. </summary>
  public static Vector3 SetZ(this Vector3 vec, float value) {
    vec.z = value;
    return vec;
  }

  /// <summary> Scale the vectors X value. </summary>
  public static Vector3 ScaleX(this Vector3 vec, float scalar) {
    vec.x *= scalar;
    return vec;
  }

  /// <summary> Scale the vectors Y value. </summary>
  public static Vector3 ScaleY(this Vector3 vec, float scalar) {
    vec.y *= scalar;
    return vec;
  }

  /// <summary> Scale the vectors Z value. </summary>
  public static Vector3 ScaleZ(this Vector3 vec, float scalar) {
    vec.z *= scalar;
    return vec;
  }

  /// <summary> Generate a random "sign", either 1 or -1. </summary>
  public static int RandomSign() {
    return Random.Range(0, 2) == 0 ? -1 : 1;
  }
}