using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;
    public float lerpSpeed;

    void LateUpdate() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position, lerpSpeed * Time.deltaTime);
    }
}
