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

    public TextMeshProUGUI countdown;
    private float count = 3;
    public GameObject detectedWarning;
    public bool detected = false;
    public bool loseGame = false;

    void Start()
    {
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
            detectedWarning.SetActive(true);
            startWarningCountdown();
        }
        else
        {
            skinnedRenderer.sharedMaterial.color = Color.green;
            cameraColor.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
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
            if (roundedCount == 0)
                loseGame = true;
            else 
                loseGame = false;
        }
    }

    public void resetWarningCountdown()
    {
        count = 3;
    }

    public bool isLost()
    {
        return loseGame;
    }

}