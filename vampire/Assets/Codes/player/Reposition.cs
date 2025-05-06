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
        float distanceX = Mathf.Abs(playerPos.x - myPos.x);
        float distanceY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVector;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch(transform.tag){
            case "Ground":
                if(distanceX > distanceY){
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else{
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if(collider.enabled){
                    
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}
