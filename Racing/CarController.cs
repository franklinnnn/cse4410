using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    Rigidbody2D carRigidbody;
    public float speed;
    public float backSpeed;
    public float rotateSpeed;
    Vector2 input;

    public string inputXName;
    public string inputYName;

    public GameObject cam;
    public int num;

    [SerializeField]
    int currentLap = 0;
    GameController cont;

    float cools;
    public float slickTimer;
    public bool slicked = false;
    public float slickRotation;
    Vector2 slickDirection;

    public float regDrag;
    public float slickDrag;
    float currentDrag;
    public float dragLerp;

    public bool hitCheckPoints = false;

    private void OnEnable() 
    {
        GameObject c = Instantiate(cam, transform.position, Quaternion.identity);
        c.GetComponent<CameraController>().target = transform;
        if(num == 1)
        {
            c.GetComponent<Camera>().rect = new Rect(new Vector2(0f, 0f), new Vector2(0.5f, 1f));
        }
        else{
            c.GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 0f), new Vector2(0.5f, 1f));
        }

        currentLap = 0;
        currentDrag = regDrag;
    }

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
        cont = FindObjectOfType<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cont.started && !slicked){
            input = new Vector2(Input.GetAxis(inputXName), Input.GetAxis(inputYName));
            if(input.x != 0)
                transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime * input.x);
            if(input.y > 0)
                carRigidbody.AddForce(transform.up * input.y * speed * Time.deltaTime);
            if(input.y < 0)
                carRigidbody.AddForce(transform.up * input.y * backSpeed * Time.deltaTime);
        }
        if(slicked){
            carRigidbody.AddForce(slickDirection * backSpeed * Time.deltaTime);
            transform.Rotate(0, 0, slickRotation * Time.deltaTime);
            if(cools <= 0)
                slicked = false;
        }
        if(cools > 0)
            cools -= Time.deltaTime;

        currentDrag = slicked ? slickDrag : regDrag;
        carRigidbody.drag = Mathf.Lerp(carRigidbody.drag, currentDrag, dragLerp * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Goal") && hitCheckPoints)
        {    
            currentLap++;
            if(currentLap >= cont.laps)
                cont.EndGame(num);
            hitCheckPoints = false;
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {    
            slickDirection = transform.up;
            cools = slickTimer;
            slicked = true;
        }

        if(collision.gameObject.CompareTag("CheckPoints"))
        {
            hitCheckPoints = true;
        }

    }
}
