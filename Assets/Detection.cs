using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    public new Camera camera;
    GameObject cameraColor;
    SkinnedMeshRenderer skinnedRenderer;
    Plane[] cameraFrustum;
    new Collider collider;

    public TextMeshProUGUI countdown;
    private float count = 3;
    public GameObject detectedWarning;
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

        if (detected)
        {
            detectedWarning.SetActive(true);
            startWarningCountdown();
        }
        else
        {
            detectedWarning.SetActive(false);
            resetWarningCountdown();
        }
    }

    public bool isDetected()
    {
        return detected;
    }

    public void startWarningCountdown()
    {
        if (count >= 0)
        {
            count -= Time.deltaTime;
            int roundedCount = Mathf.RoundToInt(count);
            countdown.text = roundedCount.ToString();
        }
    }

    public void resetWarningCountdown()
    {
        count = 3;
    }

}