using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] [Range(0f,15f)]private float defaultDistance = 6f;
    [SerializeField] [Range(0f,10f)]private float maxDistance = 15f;
    [SerializeField] [Range(0f,10f)]private float minDistance = 2f;
    
    [SerializeField] [Range(0f,10f)]private float smoothing = 4f;
    [SerializeField] [Range(0f,10f)]private float zoomSensitivity = 6f;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    //jank
    //public CinemachineVirtualCamera virtualCameraPrefab;
    public Transform player;
    public CinemachineVirtualCamera virtualCamera;
    //end of jank


    private float currentTargetDistance;

    
//jank
    void LateUpdate()
    {
        //player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = GameObject.Find("CameraLookPoint").GetComponent<Transform>();
        virtualCamera.LookAt = GameObject.Find("CameraLookPoint").GetComponent<Transform>();
    }
//end of jank
    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();

        currentTargetDistance = defaultDistance;
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;
        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentTargetDistance == currentDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}