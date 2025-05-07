using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriteRenderer;

    SpriteRenderer playerSpriteRenderer;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);

    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftPos = Quaternion.Euler(0, 0, -35f); // 회전 값
    Quaternion leftPosReverse = Quaternion.Euler(0, 0, -135f); // 회전 값



    void Awake()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInParent<SpriteRenderer>();
        if (spriteRenderers.Length > 1)
        {
            playerSpriteRenderer = spriteRenderers[1]; // 두 번째 SpriteRenderer를 가져옴
        }
    }

    void LateUpdate()
    {
        bool isReverse = playerSpriteRenderer.flipX;
        if (isLeft) // 근접
        {
            transform.localRotation = isReverse ? leftPosReverse : leftPos; 
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 6;
        }
        else // 원거리
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 6 : 4;
        }
    }
}