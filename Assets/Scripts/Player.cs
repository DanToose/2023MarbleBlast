using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public bool DebugOn;

    public float GroundProximityTolerence;
    public float TestRadius;

    //public float RollForce;
    public float JumpForce;
    public float TweakValue;

    public GameObject MainCam;
    public GameObject CamOrbit;
    public GameObject CameraRestingPos;
    public GameObject CameraResetPosition;

    public Vector3 CamOffset;
    public float CamAngle;
    public float MouseSensitivity;

    private Rigidbody rb;
    public bool canAirControl;
    private float HorizontlComponent;
    private float VerticalComponent;
    private Vector3 MovementVector;
    private float OrbitAngle;

    private bool CameraCollision;

    private GameObject gc;

    public bool useSeeThrough;
    private GameObject objectHit;

    public Material defaultMaterial;
    public Material hitMaterial;
    public Material previousMaterial;
    //
    public float horizontalSpeed = 2.0f; // speed of horizontal rotation
    public float verticalSpeed = 2.0f; // speed of vertical rotation
    public float distance = 10.0f; // distance between camera and player

    //

    [HideInInspector] public Vector3 ResetPosition;
    [HideInInspector] public Quaternion ResetRotation; // not actually used yet

    void Start()
    {
        DebugOn = true;

        rb = GetComponent<Rigidbody>();
        gc = GameObject.FindGameObjectWithTag("GameController");
        ResetPosition = gc.GetComponent<GameController>().LevelStart.transform.position + gc.GetComponent<GameController>().StartOffset;

        ResetRotation = Quaternion.identity; // because I'm lazy... eventually change this to the orientation of the start object
    }

    void LateUpdate() // Was FixedUpdate
    {
        HandleInput();
        UpdateCamera();

        if (DebugOn == true)
        {
            Debug.DrawRay(transform.position, Vector3.down * GroundProximityTolerence, Color.green);
            Debug.DrawRay(transform.position, (transform.position - CameraRestingPos.transform.position) * -1, Color.yellow);
        }
    }

    private bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, GroundProximityTolerence))
            return true;
        else
            return false;
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.R))
            ResetPlayer();

        if (CheckGrounded() == true)
        {
            if (!canAirControl)
            {
                HorizontlComponent = (Input.GetAxis("Horizontal"));
                VerticalComponent = (Input.GetAxis("Vertical"));
            }

            if (Input.GetKey(KeyCode.Space))
                rb.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }

        if (canAirControl)
        {
            HorizontlComponent = (Input.GetAxis("Horizontal"));
            VerticalComponent = (Input.GetAxis("Vertical"));
        }

        MovementVector = new Vector3(HorizontlComponent, 0, VerticalComponent);

        MovementVector = Quaternion.AngleAxis(OrbitAngle, Vector3.up) * MovementVector;
        MovementVector.Normalize();

        rb.AddForce(MovementVector, ForceMode.Impulse);
        rb.AddForce(MovementVector * TweakValue, ForceMode.VelocityChange);

        OrbitAngle += (Input.GetAxis("Mouse X") * MouseSensitivity);
        CamAngle -= (Input.GetAxis("Mouse Y") * MouseSensitivity);
        CamAngle = Mathf.Clamp(CamAngle, -90, 90); // CHANGE THE VALUES OF -90 AND 90 TO ADJUST VERTICAL MOUSE CLAMPING
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
            CameraCollision = false;
    }

    private void UpdateCamera()
    {
        CamOrbit.transform.position = transform.position;

        if (CameraCollision == false)
        {
            Quaternion rotation = Quaternion.Euler(CamAngle, OrbitAngle, 0);
            MainCam.transform.position = transform.position - rotation * Vector3.forward * distance;
            MainCam.transform.rotation = rotation;
        }

        CheckCamCollision();
    }

    private void CheckCamCollision()
    {
        Vector3 a = transform.position;
        Vector3 b = (transform.position - CameraRestingPos.transform.position) * -1;
        float l = Vector3.Magnitude(transform.position - CameraRestingPos.transform.position);
        RaycastHit h;
        Ray r = new Ray(a, b);
        if (!useSeeThrough)
        {
            if (Physics.Raycast(r, out h, l))
                CameraRestingPos.transform.position = h.point;
            else
                CameraRestingPos.transform.position = CameraResetPosition.transform.position;

            CameraCollision = Physics.Raycast(r, l);

            MainCam.transform.position = CameraRestingPos.transform.position;
        }
        else
        {
            if (Physics.Raycast(r, out h, l))
            {
                previousMaterial = defaultMaterial;
                if (h.transform.gameObject.GetComponent<Renderer>().sharedMaterial != hitMaterial)
                {
                    defaultMaterial = h.transform.gameObject.GetComponent<Renderer>().material;
                }
                if (h.transform.gameObject != objectHit && objectHit != null)
                {
                    objectHit.transform.gameObject.GetComponent<MeshRenderer>().material = previousMaterial;
                }
                objectHit = h.transform.gameObject;
                objectHit.GetComponent<MeshRenderer>().material = hitMaterial;
            }
            else
            {
                if (objectHit != null)
                {
                    objectHit.transform.gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                }
            }
            CameraCollision = Physics.Raycast(r, l);
        }

    }

    public void ResetPlayer()
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.position = ResetPosition; //gc.GetComponent<GameController> ().LevelStart.transform.position+gc.GetComponent<GameController> ().StartOffset;


    }

}

