using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusScript : MonoBehaviour
{
    private GameObject player;
    private GameObject drone;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        drone = GameObject.Find("drone");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 1.5f, 0);
        drone.transform.position = transform.position + transform.up + transform.right * 2;

        if (Input.GetAxis("Mouse X") > 0)
        {
            //transform.Rotate(transform.up, 200 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1 * 1, 0);
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            //transform.Rotate(transform.up * -1, 200 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1 * -1, 0);
        }


        if (Input.GetAxis("Mouse Y") > 0)
        {
            //transform.Rotate(transform.right * -1, 100 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 1 * -1, transform.rotation.eulerAngles.y, 0);
        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            //transform.Rotate(transform.right, 100 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 1 * 1, transform.rotation.eulerAngles.y, 0);
        }
    }
}
