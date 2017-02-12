using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player3 : MonoBehaviour {
    Rigidbody rigBod;

    Vector3 velocity;
    Vector3 position;

    //CameraAnimate cameraAnimate;

    public float speed = 2f;
    public float slideSpeed = 2f;
    public float jumpSpeed = 3f;
    public float slideBounds = 4f;
    public float multCoolDown = 4f;
    public float deathDelay = 1f;

    public float numDeathVisara;
    public float visaraForwardForce;
    public float visaraSpreadForce;

    public GameObject Visara;

    public GameObject deathText;

    public AudioClip deathSound;
    public AudioClip multBostSound;

    Transform[] children;

    float distToGround;
    float oldZ = 0f;
    float multiplyer;
    float score;
    float multTimer;

    bool isDead;

    MeshRenderer DTextRend;

    // Use this for initialization
    void Start ()
    {
        isDead = false;

        Dimension.set2D();

        multiplyer = 1;

        velocity = new Vector3();

        rigBod = GetComponent<Rigidbody>();

        DTextRend = deathText.GetComponent<MeshRenderer>();
        DTextRend.enabled = false;

        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;

        children = GetComponentsInChildren<Transform>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        //setting our internal Vector 3 velocity to the Rigid Bodys velocity/tranform position for pass though and changes behavor
        velocity = rigBod.velocity;
        position = transform.position;

        if (!isDead)
        {
            Debug.Log("update");

            if (Dimension.is2D())
            {
                update2D();
            }
            else if (Dimension.is3D())
            {
                update3D();
            }

            //making us move foward
            velocity.x = speed;

            updateScore();
        }

        transform.position = position;
        rigBod.velocity = velocity;
    }

    void updateScore()
    {
        multTimer += Time.deltaTime;
        if(multTimer >= multCoolDown)
        {
            multiplyer = 0.0f;
            multTimer = 0.0f;
        }
        score += Time.deltaTime * speed * multiplyer;
        print("Score: " + (int)score + "Mult: " + multiplyer);
    }

    void multBost()
    {
        playClip(multBostSound);
        multiplyer += 0.1f;
        multTimer = 0.0f;
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
            Debug.Log("Dead");
            StartCoroutine(Death());
        }
        else if(other.gameObject.tag == "MultBost")
        {
            multBost();
            Destroy(other.gameObject);
        }
    }

    IEnumerator Death()
    {
        isDead = true;
        playClip(deathSound);

        GetComponent<MeshRenderer>().enabled = false;

        for(int i = 0; i < children.Length; ++i)
        {
            if (children[i].GetComponent<Rigidbody>() != null)
            {
                children[i].GetComponent<Rigidbody>().isKinematic = false;
                children[i].GetComponent<Rigidbody>().AddForce(new Vector3(visaraForwardForce, Random.Range(-visaraSpreadForce, visaraSpreadForce), Random.Range(0f, 2 * visaraSpreadForce)));
                children[i].parent = null;
            }
        }
        for(int i = 0; i < numDeathVisara; ++i)
        {
            Instantiate(Visara, new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f)), transform.rotation).transform.GetComponent<Rigidbody>().AddForce(new Vector3(visaraForwardForce,Random.Range(-visaraSpreadForce, visaraSpreadForce), Random.Range(0f, 2 * visaraSpreadForce)));
        }


        GameObject.FindGameObjectWithTag("Music And Beat").GetComponent<BeatAndMusic>().mute();
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //check if we are toching the ground when
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void playClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,transform.position, 1f);
    }
}
