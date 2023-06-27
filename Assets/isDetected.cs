using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDetected : MonoBehaviour
{
    public new Camera camera;
    GameObject cameraColor;
    SkinnedMeshRenderer skinnedRenderer;
    Plane[] cameraFrustum;
    new Collider collider;

    public bool detected = false;

    void Start()
    {
        cameraColor = GameObject.FindGameObjectWithTag("securityCamera");
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            detected = true;
            skinnedRenderer.sharedMaterial.color = Color.red;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        }
        else
        {
            detected = false;
            skinnedRenderer.sharedMaterial.color = Color.green;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        }
    }

    public bool isDetectedFunction()
    {
        return detected;
    }
}
