using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Vector2 inputVector;    
    public float speed;
    public Scanner scanner;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public Hand[] hands; // 손의 배열
    public RuntimeAnimatorController[] animators; // 애니메이터 컨트롤러 배열

    // 캐릭터 이동방법 3가지
    // 1. Transform.Translate
    // 2. Rigidbody.AddForce
    // 3. Rigidbody.MovePosition
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // 자식 오브젝트에서 Hand 컴포넌트를 가진 모든 오브젝트를 가져옴
    }

    void OnEnable()
    {
        speed *= Character.Speed; // 캐릭터의 속도를 설정
        animator.runtimeAnimatorController = animators[GameManager.instance.playerId];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
        
        // inputVector.x = Input.GetAxisRaw("Horizontal");
        // inputVector.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        Vector2 nextVector = inputVector * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        animator.SetFloat("Speed", inputVector.magnitude);
        if (inputVector.x != 0)
        {
            spriteRenderer.flipX = inputVector.x < 0;
        }
    }   

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

            if (GameManager.instance.health < 0)
            {
                for(int index=2; index < transform.childCount; index++)
                {
                    transform.GetChild(index).gameObject.SetActive(false);
                }
                animator.SetTrigger("Dead");
                GameManager.instance.GameVictory();
            }
    }
}
