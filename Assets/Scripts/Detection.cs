using UnityEngine;

public class Detection : MonoBehaviour
{
    public ScreenManager screenManager;

    public GameObject[] completionMarker;

    public Camera[] cameras;
    public GameObject cameraColor;
    public SkinnedMeshRenderer skinnedRenderer;
    public new Collider collider;

    public ProgressBar suspicion;

    public int completedCounter = 0; //this counter lets me add a condition that fixes the timeScale override problem between the BoxCast and the restartStage()

    public bool stageComplete = false;
    public bool detected = false;
    public bool loseGame = false;

    void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("screenManager").GetComponent<ScreenManager>();
        suspicion = GameObject.FindGameObjectWithTag("suspicionBar").GetComponent<ProgressBar>();
        cameraColor = GameObject.FindGameObjectWithTag("securityCamera");
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        var bounds = collider.bounds;
        detected = false;

        foreach (var camera in cameras)
        {
            //takes the cameras' frustum (what camera sees) to test if it can see player
            Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);

            if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
            {
                detected = true;
                break;
            }
        }

        if (detected)
        {
            skinnedRenderer.sharedMaterial.color = Color.red;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
            suspicionBar();
        }
        else
        {
            skinnedRenderer.sharedMaterial.color = Color.green;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        }

        Vector3 boxSize = new Vector3(0.4f, 0.4f, 0.4f);
        float castDistance = 5f;
        RaycastHit hitInfo;
        foreach (var marker in completionMarker)
        {
            //casts a box above completion markers to detect player
            if (Physics.BoxCast(marker.transform.position, boxSize / 2f, Vector3.up, out hitInfo, Quaternion.identity, castDistance) && completedCounter == 0)
            {
                stageComplete = true;
                completedCounter++;
            }
        }
    }

    public void suspicionBar()
    {
        if (suspicion.current < 10 && screenManager.currentStage == 0)
            suspicion.current += Time.deltaTime;
        else if (suspicion.current < 10 && screenManager.currentStage == 1)
            suspicion.current += Time.deltaTime * 2;
        loseGame = suspicion.current >= suspicion.max ? true : false;
    }

    public bool isDetected()
    {
        return detected;
    }

    public bool isLost()
    {
        return loseGame;
    }

}