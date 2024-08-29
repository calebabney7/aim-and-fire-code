using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject rightHand;
    public GameObject player;
    public Transform handTransform;
    public bool canShoot;
    private float timer;
    public bool isHanging;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the position of the mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        //get rotation
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //fire hand
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            Instantiate(rightHand, handTransform.position, Quaternion.identity);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        }

        //let go with hand
        if (Input.GetMouseButton(1))
        {
            canShoot=true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            player.transform.rotation = Quaternion.identity;
        }
        
        //start timer 
        if(!canShoot)
        {
            timer += Time.deltaTime;

            //times up
            if(timer > 1)
            {
                canShoot = true;
                timer = 0;
            }
        }
        
    }
}
