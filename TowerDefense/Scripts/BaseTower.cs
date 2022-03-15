using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public float shootSpeed;
    public float rotationSpeed;
    public Transform target;
    public float attackRange;

    public Transform child;

    protected float coolDown;
    public GameObject bullet;

    public GameObject[] bulletSpawnPositions;
    public float cost;

    public GameObject flash;

    protected AudioSource src;

    private void Awake() 
    {
        src = GetComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        GetComponent<CircleCollider2D>().radius = attackRange;
        coolDown = shootSpeed;
    }
        
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Enemy") && target == null)
        {
            target = collision.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Enemy") && target == null)
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Enemy") && target == collision.transform)
        {
            target = null;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Debug.Log(target.gameObject);
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);

            if(coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }
            else
            {
                Shoot();
            }
        }
    }

    public virtual void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        coolDown = shootSpeed;
    }

    private void LateUpdate() 
    {
        child.transform.rotation = Quaternion.identity;
    }
}
