using UnityEngine;
using System.Collections;

public class cameraControls4 : MonoBehaviour {

    //the toggle key is going to be the spacebar
    public bool in2D = true;
    public bool in3D = false;

    //for orbit
    public const float orbitAmount = 10f;
    private float targetAngle = 0;

    //for rotation about x
    private float xRotate = 0;
    private const float rotationAmount = 1f;

    //for translating up by 3
    private float yTranslate = 0;
    private const float translateAmount = 1f;

    //find player
    public Transform player;

    void Start()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5;
    }
	
	// Update is called once per frame
	void Update () {

        //BUTTON
        bool pushButt = Input.GetKeyDown(KeyCode.LeftShift);

        //if the camera is currently in 2D AND button is pressed -> switch to 3D
        if (in2D && pushButt)
        {
            Debug.Log("starting 2D to 3D switch");
            //player.gameObject.SendMessage("start3D");
            player.GetComponent<Player3>().start3D();
            switch3D();
        }

        //if the camera is currently in 3D AND button is pressed -> switch to 2D
       else if(in3D && pushButt)
        {
            Debug.Log(" starting 3D to 2D Switch ");
            switch2D();
        }

        //Orbit if an the angle has been set
        if (targetAngle != 0)
        {
            Orbit();
        }

        //Rotate camera about x
        if (xRotate != 0)
        {
            RotateX();
        }

        //Translate Camera Up y
        if (yTranslate != 0)
        {
            TranslateY();
        }


        //SEND MESSAGE TO PARENT

        //switch from 3D to 2D call when transition is done
        if (targetAngle == rotationAmount)
        {
            Dimension.set2D();
            player.GetComponent<Player3>().end2D();
            //player.gameObject.SendMessage("end2D");
            Debug.Log("now in 2D");
        }

        //switch from 2D to 3D call when transition is done
        if(targetAngle == -rotationAmount)
        {
            Dimension.set3D();

            Debug.Log("now in 3D");
        }
    }

    void switch3D() //now in 3D
    {
        in2D = false;
        in3D = true;

        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 60;

        targetAngle -= 90;

        xRotate += 10;
        yTranslate += 3;
    }

    void switch2D() //now in 2D
    {
        in2D = true;
        in3D = false;

        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5;

        targetAngle += 90;

        xRotate -= 10;
        yTranslate -= 3;

    }

    //Rotate
    protected void Orbit()
    {

        if (targetAngle > 0) //switch from 3D to 2D
        {
            transform.RotateAround(player.transform.position, Vector3.up, -orbitAmount);
            targetAngle -= orbitAmount;
        }
        else if (targetAngle < 0) //switch from 2D to 3D
        {
            //Orbit path
            transform.RotateAround(player.transform.position, Vector3.up, orbitAmount);
            targetAngle += orbitAmount;

        }
    }

    void RotateX()
    {
        if(xRotate > 0) // for 2d -> 3D rotate camera up
        {
            
            transform.Rotate(Vector3.right, rotationAmount);
            xRotate -= rotationAmount;
        }
        else if (xRotate < 0) // 3D -> 2D rotate camera down
        {
            transform.Rotate(Vector3.right, -rotationAmount);
            xRotate += rotationAmount;
        }
    }

    void TranslateY()
    {
        if(yTranslate > 0)//2d -> 3D move camera up
        {
            transform.Translate(Vector3.up * translateAmount);
            yTranslate -= translateAmount;
        }
        else if(yTranslate < 0)//3D -> 2D move camera down
        {
            transform.Translate(Vector3.down * translateAmount);
            yTranslate += translateAmount;
        }
    }
    
}
