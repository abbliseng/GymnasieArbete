using UnityEngine;
using System.Collections;

public class MouseMovement : MonoBehaviour
{

    protected Transform _XForm_Camera;
    protected Transform _XForm_CameraPivot;

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;
    public float maxZoomOut = 100f;

    public bool CameraDisabled = false;

    [Header("Movement")]
    public GameObject sphere;
    public CharacterController characterController;
    public float speed = 6.0f;

    // Use this for initialization
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_CameraPivot = this.transform.parent;
    }

    void LateUpdate()
    {
        // Movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0f, v).normalized;
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + this._XForm_CameraPivot.eulerAngles.y;
            sphere.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 mDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(mDir.normalized * speed * Time.deltaTime);
        }
        // Handles rotation
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    CameraDisabled = !CameraDisabled;

        if (!CameraDisabled)
        {
            if(Input.GetMouseButton(1))
            {
                //Rotation of the Camera based on Mouse Coordinates
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                    _LocalRotation.y += Input.GetAxis("Mouse Y") * MouseSensitivity * -1;

                    //Clamp the y Rotation to horizon and not flipping over at the top
                    if (_LocalRotation.y < 0f)
                        _LocalRotation.y = 0f;
                    else if (_LocalRotation.y > 90f)
                        _LocalRotation.y = 90f;
                }
            }
            //Zooming Input from our Mouse Scroll Wheel
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                ScrollAmount *= (this._CameraDistance * 0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, maxZoomOut);
            }
        }

        //Actual Camera Rig Transformations
        sphere.transform.rotation = Quaternion.Euler(0, _LocalRotation.x, 0);
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_CameraPivot.rotation = Quaternion.Lerp(this._XForm_CameraPivot.rotation, QT, Time.deltaTime * OrbitDampening);

        if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }
}