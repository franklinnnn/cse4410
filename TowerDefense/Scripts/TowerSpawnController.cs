using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnController : MonoBehaviour
{
    
    GameController control;

    SpriteRenderer render;

    private void Awake()
    {
        control = FindObjectOfType<GameController>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(control.credits >= control.currentTowerCost)
        {
            Debug.Log("Tower spawned");
            control.GiveMoney(-control.currentTowerCost);
            Instantiate(control.tower, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnMouseOver() 
    {
        if(control.credits >= control.currentTowerCost)
        {
            render.color = Color.green;
        }
        else
        {
            render.color = Color.red;
        }
    }

    private void OnMouseExit() {
        {
            render.color = Color.white;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
