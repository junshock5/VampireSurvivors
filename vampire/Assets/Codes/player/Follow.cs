using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect == null)
        {
            Debug.LogError("RectTransform component is missing on this GameObject.");
        }
    }

    void FixedUpdate()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera is not found in the scene.");
            return;
        }

        if (GameManager.instance == null || GameManager.instance.player == null)
        {
            Debug.LogError("GameManager or Player is not properly initialized.");
            return;
        }

        // Update the position of the RectTransform
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}