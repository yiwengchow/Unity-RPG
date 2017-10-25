using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 3f;
    private float lookSensitivity = 3f;
    private Camera camera;
    private Transform target;

    private float currentX = 0f;

    private PlayerMotor motor;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
        camera = GameObject.Find("camera").GetComponent<Camera>();
        target = GameObject.Find("Cha_Knight").transform;
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");
        currentX += Input.GetAxisRaw("Mouse Y") / 10;

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);
        
        if (Input.GetMouseButtonDown(0))
        {
            motor.Attack();
        }
	}

    private void LateUpdate()
    {
        if (currentX < -target.position.y - 2.5f)
        {
            currentX = -target.position.y - 2.5f;
        }
        else if (currentX > -target.position.y - 0.5f)
        {
            currentX = -target.position.y -0.5f;
        }

        Debug.Log(target.position.y);
        Debug.Log("x is " + currentX);

        camera.transform.position = new Vector3(camera.transform.position.x, -currentX, camera.transform.position.z);
        camera.transform.LookAt(new Vector3(target.position.x, target.position.y + 1, target.position.z));
    }
}
