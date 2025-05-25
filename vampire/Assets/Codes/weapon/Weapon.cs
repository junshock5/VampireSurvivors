using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;

    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        //player = GetComponentInParent<Player>();
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed){
                    timer = 0;
                    Fire();
                }
                break;
        }

        // test code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp(10, 1);
        }
    }
    
    public void LevelUp(float damange, int count)
    {
        this.damage = damange * Character.Damage;
        this.count += count;
        if(id == 0)
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // basic set
        name = "Weapon_" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        
        // property set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for(int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                Batch();
                speed = 150 * Character.WeaponSpeed;
                break;
            default:
                speed = 0.3f * Character.WeaponRate;
                break;
        }
        
        // hand set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriteRenderer.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if( i < transform.childCount) { //내가가진 자식오브젝트를 재사용
                bullet = transform.GetChild(i);
            }
            else
            { // 자식보다 개수가 더많다면 풀매니저에서 객체 가져오기
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            bullet.parent = transform;
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; // Corrected line

            Vector3 rotVeC = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVeC);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100은 플레이어 총알
        }
    }

    void Fire()
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);


        AudioManager.instance.PlaySfx(AudioManager.SfxType.Range);
    }
}
