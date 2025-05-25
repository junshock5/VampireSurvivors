using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D collider;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch(transform.tag){
            case "Ground":
                float diffx = playerPos.x - myPos.x;
                float diffy = playerPos.y - myPos.y;
                float dirX = diffx < 0 ? -1 : 1;
                float dirY = diffy < 0 ? -1 : 1;
                diffx = Mathf.Abs(diffx);
                diffy = Mathf.Abs(diffy);

                if(diffx > diffy){
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else{
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if(collider.enabled){
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                    transform.Translate(ran + dist * 2 );
                }
                break;
        }
    }
}
