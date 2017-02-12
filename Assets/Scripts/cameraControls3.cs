using UnityEngine;
using System.Collections;

public class cameraControls3 : MonoBehaviour {

    //the toggle key is going to be the spacebar
    public bool in2D = true;
    public bool in3D = false;

    //for rotation
    public const float rotationAmount = 10f;
    private float targetAngle = 0;

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
            player.SendMessage("start3D");
            switch3D();
        }

        //if the camera is currently in 3D AND button is pressed -> switch to 2D
       else if(in3D && pushButt)
        {
            Debug.Log(" starting 3D to 2D Switch ");
            //player.SendMessage("end2D");
            switch2D();
        }

        //Rotate if an the angle has been set
        if (targetAngle != 0)
        {
            Rotate();
        }

        //SEND MESSAGE TO PARENT

        //switch from 3D to 2D call when transition is done
        if (targetAngle == rotationAmount)
        {
            Dimension.set2D();
            player.SendMessage("end2D");
            Debug.Log("now in 2D");
        }

        //switch from 2D to 3D call when transition is done
        if(targetAngle == -rotationAmount)
        {
            Dimension.set3D();

            Debug.Log("now in 3D");
        }
    }

    void switch3D()
    {
        in2D = false;
        in3D = true;

        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 60;

        targetAngle -= 90;
    }

    void switch2D()
    {
        in2D = true;
        in3D = false;

        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5;

        targetAngle += 90;

    }

    //Rotate
    protected void Rotate()
    {

        if (targetAngle > 0) //switch from 3D to 2D
        {
            transform.RotateAround(player.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0) //switch from 2D to 3D
        {
            transform.RotateAround(player.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
}
