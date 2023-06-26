using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDetected : MonoBehaviour
{
    public Camera camera;
    SkinnedMeshRenderer renderer; //no need
    Plane[] cameraFrustum;
    Collider collider;

    void Start()
    {
        renderer = GetComponent<SkinnedMeshRenderer>(); //no need
        collider = GetComponent<Collider>();
    }


    void Update()
    {
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            renderer.sharedMaterial.color = Color.red;
        }
        else
        {
            renderer.sharedMaterial.color = Color.green;
        }
    }
}
