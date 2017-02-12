using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player3 : MonoBehaviour {
    Rigidbody rigBod;

    Vector3 velocity;
    Vector3 position;

    //CameraAnimate cameraAnimate;

    public float speed = 2;
    public float slideSpeed = 2;
    public float jumpSpeed = 3;
    public float slideBounds = 4;

    float distToGround;
    float oldZ = 0f;

    // Use this for initialization
    void Start ()
    {
        Dimension.set2D();

        velocity = new Vector3();

        rigBod = GetComponent<Rigidbody>();

        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        //cameraAnimate = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraAnimate>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //setting our internal Vector 3 velocity to the Rigid Bodys velocity/tranform position for pass though and changes behavor
        velocity = rigBod.velocity;
        position = transform.position;

        if (Dimension.is2D())
        {
            update2D();
        }

        else if (Dimension.is3D())
        {
            update3D();
        }

        //making us move foward
        velocity.x = speed;// * Time.deltaTime * 60;

        transform.position = position;
        rigBod.velocity = velocity;
    }

    void update2D()
    {
        //jump implementation
        if (Input.GetKeyDown("space") && isGrounded())
        {
            velocity.y = jumpSpeed;
            print("jump");
        }
    }

    void update3D()
    {
        //sliding and consticting to bounds
        //slide to right
        if (transform.position.z >= -slideBounds)
        {
            if (Input.GetKey("d"))
            {
                velocity.z = -slideSpeed;
            }
        }
        else
        {
            position.z = -slideBounds;
        }

        //slide to left
        if (transform.position.z <= slideBounds)
        {
            if (Input.GetKey("a"))
            {
                velocity.z = slideSpeed;
            }
        }
        else
        {
            position.z = slideBounds;
        }


        if ((!Input.GetKey("d") && !Input.GetKey("a")) || (Input.GetKey("d") && Input.GetKey("a")))
        {
            velocity.z = 0;
        }

        //save our z position for later
        oldZ = transform.position.z;
    }


     public void start3D()
    {
        //Centering us
        print("start3D");
        position.z = oldZ;
        transform.position = position;
    }

    public void end2D()
    {
        //locking us for 2d
        print("end2D");
        position.z = -slideBounds;
        transform.position = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //check if we are toching the ground when
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
