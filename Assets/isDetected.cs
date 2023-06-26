using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDetected : MonoBehaviour
{
    public Camera camera;
    GameObject cameraColor; //NEW
    SkinnedMeshRenderer skinnedRenderer;
    //MeshRenderer renderer;
    Plane[] cameraFrustum;
    Collider collider;
    

    void Start()
    {
        cameraColor = GameObject.FindGameObjectWithTag("securityCamera"); //NEW
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        //renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }


    void Update()
    {
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            skinnedRenderer.sharedMaterial.color = Color.red;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        }
        else
        {
            skinnedRenderer.sharedMaterial.color = Color.green;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;

        }
    }
}
