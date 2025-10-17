using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    AudioSource angryCat;
    [SerializeField] private LineRenderer rubberBand;
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    // fields set in Unity Inspector pane
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    public GameObject projLinePrefab;

    // fields set dynamically
    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public Vector3 rubberLaunchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Start()
    {
        rubberBand.SetPosition(0, firstPoint.position);
        rubberBand.SetPosition(2, secondPoint.position);
        rubberLaunchPos = rubberBand.GetPosition(1);
    }
    
    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void OnMouseEnter()
    {
        print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        if (FindObjectOfType<GameManager>().gameOver || FindObjectOfType<GameManager>().gameStart == false)   // if the game is over, the player cannot launch anymore
        {
            return;
        }
        // The player has pressed the mouse button while over Slingshot
        aimingMode = true;

        // Instantiate a Projectile
        projectile = Instantiate(projectilePrefab) as GameObject;

        // Start it at the launchPoint
        projectile.transform.position = launchPos;

        // Set it to isKinematic for now
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        angryCat = projectile.GetComponent<AudioSource>();
    }

    void Update()
    {
        // If Slingshot is not in aimingMode, don't run this code
        if (!aimingMode) return;
        
        // Get the current mouse position in 2D screen coordinates
        Vector3 mousePos3D = GetMousePosition();

        /* if (Input.GetMouseButton(0))
        {
            rubberBand.SetPosition(1, mousePos3D);
        } */

        // Find the delta form the launchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;
        // Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButton(0))
        {
            rubberBand.SetPosition(1, projPos); // Set the middle of the rubber band to where the projectile is
        }

        if (Input.GetMouseButtonUp(0))
        {
            // The mouse has been released
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;

            if (angryCat != null)
            {
                angryCat.Play();
            }

            // Switch to slingshot view immediately before setting POI
            FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);

            FollowCam.POI = projectile; // Set the _MainCamera POI
            // Add a ProjectileLine to the Projectile
            Instantiate<GameObject>(projLinePrefab, projectile.transform);

            rubberBand.SetPosition(1, rubberLaunchPos);

            projectile = null;
            MissionDemolition.SHOT_FIRED();
        }
    }
    
    Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z -= Camera.main.transform.position.z;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePositionInWorld;
    }
}
