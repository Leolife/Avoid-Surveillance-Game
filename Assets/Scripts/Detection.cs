using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    public Camera[] cameras;
    public GameObject cameraColor;
    public SkinnedMeshRenderer skinnedRenderer;
    public new Collider collider;

    public ProgressBar suspicion;

    public bool detected = false;
    public bool loseGame = false;

    void Start()
    {
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
    }

    public void suspicionBar()
    {
        if (suspicion.current < 10)
            suspicion.current += Time.deltaTime;
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