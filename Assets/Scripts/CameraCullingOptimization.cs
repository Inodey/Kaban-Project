using UnityEngine;

public class CameraCullingOptimization : MonoBehaviour
{
    private Camera mainCamera;
    private Plane[] frustumPlanes;
    private Renderer[] sceneRenderers;
    private float fogEndDistance;
    private float cullingDistanceMultiplier = 1.8f;

    void Start()
    {
        mainCamera = Camera.main;
        sceneRenderers = FindObjectsOfType<Renderer>();

        // Get fog end distance from RenderSettings and multiply by 2.5 for smoother culling
        fogEndDistance = RenderSettings.fogEndDistance * cullingDistanceMultiplier;
    }

    void Update()
    {
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Vector3 camPos = mainCamera.transform.position;

        foreach (Renderer renderer in sceneRenderers)
        {
            float distanceToRenderer = Vector3.Distance(camPos, renderer.bounds.center);

            if (distanceToRenderer > fogEndDistance)
            {
                // Too far beyond fog - disable rendering
                if (renderer.enabled)
                    renderer.enabled = false;
                continue;
            }

            // Check if the object is within camera frustum
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
            {
                if (!renderer.enabled)
                    renderer.enabled = true;
            }
            else
            {
                if (renderer.enabled)
                    renderer.enabled = false;
            }
        }
    }
}