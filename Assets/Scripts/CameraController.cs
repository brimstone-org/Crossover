using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraOffset;
    [SerializeField]
    private Transform currentTarget; //the current stack that I am looking at
    private Coroutine transition;
    private Vector3 previousPosition; //to turn the camera around
    private bool transitioning; //to prevent rotating the camera while transitioning
    private Camera thisCamera;


    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }
    /// <summary>
    /// moves camera to the selected stack
    /// </summary>
    /// <param name="target"></param>
    public void MoveToStack(Transform target)
    {
        if (transition != null)
        {
            StopCoroutine(transition);
        }
        transition = StartCoroutine(CameraTransition(target));
    }

    private IEnumerator CameraTransition(Transform target)
    {
        transitioning = true;
        Vector3 startingPos = transform.position;
        Vector3 finalPos = target.position + cameraOffset;
        float elapsedTime = 0;
        Quaternion startingRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 25, 0);
        while (elapsedTime < 1)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / 1));
            transform.rotation = Quaternion.Lerp(startingRot, targetRot, (elapsedTime / 1));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = finalPos;
        transform.rotation = targetRot;
        currentTarget = target;
        transitioning = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = thisCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1) && !transitioning && currentTarget!=null)
        {
            Vector3 direction = previousPosition - thisCamera.ScreenToViewportPoint(Input.mousePosition);
            transform.position = currentTarget.position ;
            transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            transform.Rotate(new Vector3(0, 1, 0), -direction.x * 100, Space.World);
            transform.Translate(new Vector3(0,10, -25));
            previousPosition = thisCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
