using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class CameraShake : MonoBehaviour
{
    /*
    private CinemachineVirtualCamera camera;
    private CinemachineBasicMultiChannelPerlin shakeComponent;

    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        shakeComponent = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float duration, float magnitude)
    {
        StopShake();
        StartCoroutine(IShake(duration, magnitude));
    }

    public void StopShake()
    {
        StopAllCoroutines();
        //Debug.Log("StopShaking");
        shakeComponent.m_AmplitudeGain = 0f;
    }

    private IEnumerator IShake(float duration, float magnitude)
    {
        //Debug.Log("StartShaking");
        shakeComponent.m_AmplitudeGain = magnitude;

        yield return new WaitForSeconds(duration);

        StopShake();
    }
    */
}