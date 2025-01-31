using UnityEngine;

public class Enemy : MonoBehaviour
{
   public float speed;
    public Rigidbody2D target; // 오타 수정
    bool isLive = true;
    Rigidbody2D rigid;
   SpriteRenderer spriteRenderer;

    void Awake()
    {
        speed = 2.0f;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;
        // 방향 = 위치 차이의 정규화 * 속도 * 시간 // normalized 피타고라스 대각선만큼 더가는걸 방지
        Vector2 nextVector = (target.position - rigid.position).normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

}
