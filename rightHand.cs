using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rightHand : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public HingeJoint2D joint;
    public Rigidbody2D player;
    public float timer;
    public bool isHanging;
    public GameObject rotPoint;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        joint.enabled = false;

        isHanging = false;

        rotPoint = GameObject.FindGameObjectWithTag("RotationPoint");
    }

    private void Update()
    {
        //let go
        if (Input.GetMouseButton(1))
        {
            joint.enabled = false;
            Destroy(gameObject);
        }

        //not hanging so enable timer
        if (!isHanging)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.transform.rotation = Quaternion.identity;
                Destroy(gameObject);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider belongs to the object you want to react to
        if (other.CompareTag("platform"))
        {
            //allow player to rotate
            player.constraints = RigidbodyConstraints2D.None;

            //tell shooting script to stop timer
            rotPoint.GetComponent<Shooting>().isHanging = true;
            isHanging = true;

            //enable joint and set player
            joint.enabled = true;
            joint.connectedBody = player;

            // Freeze the position when the collider enters
            rb.constraints = RigidbodyConstraints2D.FreezePosition;

        }
    }
}
