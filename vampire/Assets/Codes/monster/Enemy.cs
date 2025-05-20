using UnityEngine;
using System.Collections; // Add this line to resolve the error


public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animators;
    public Rigidbody2D target; // 오타 수정

    bool isLive = true;
    Rigidbody2D rigid;
    Collider2D collider;
    Animator animator;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;
    void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = true;
        animator = GetComponent<Animator>();
        speed = 2.0f;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        // 방향 = 위치 차이의 정규화 * 속도 * 시간 // normalized 피타고라스 대각선만큼 더가는걸 방지
        Vector2 nextVector = (target.position - rigid.position).normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
        if (!isLive)
            return;
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive =  true;
        collider.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animators[data.spriteType];
        speed = data.speed;
        health = data.health;
        maxHealth = data.health;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
        health -= bullet.damage;
        StartCoroutine(KnockBack());

            if (health > 0)
            {
                animator.SetTrigger("Hit");

            }
            else
            {
                isLive = false;
                collider.enabled = false;
                rigid.simulated = false;
                spriteRenderer.sortingOrder = 1;
                animator.SetBool("Dead", true);
                GameManager.instance.GetExp();
                GameManager.instance.kill++;

            }
        }
    }

    // 비동기 코루틴
    public IEnumerator KnockBack()
    {
      yield return wait; // 하나의 물리 프레임 딜레이
      Vector3 playerPos = GameManager.instance.player.transform.position;
      Vector3 dirVec = transform.position - playerPos;
      rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    public void Dead()
    {
        //isLive = false;
        gameObject.SetActive(false);
    }

}
