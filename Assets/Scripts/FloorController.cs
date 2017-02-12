using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public float fallTime;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Invoke("ToggleRigid", fallTime);
	}

    void Update() {
        if (transform.position.y < -15) {
            Destroy(this.gameObject);
        }
    }
	
	private void ToggleRigid() {
        rb.isKinematic = false;
    }
}
