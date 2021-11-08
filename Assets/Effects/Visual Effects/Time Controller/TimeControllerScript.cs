using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerScript : MonoBehaviour
{
    #region Setup

    public float defaultGameSpeed = 1f;
    float currentGameSpeed;
    float clampedGameSpeed;

    private void Start()
    {
        currentGameSpeed = defaultGameSpeed;
    }

    #endregion

    #region Set Game Speed Functions

    public void SetGameSpeed(float amount, float duration)
    {
        float newSpeed = defaultGameSpeed + amount;

        if (currentGameSpeed > newSpeed)
        {
            currentGameSpeed = newSpeed;

            clampedGameSpeed = Mathf.Clamp(currentGameSpeed, 0.01f, 10f);
            Time.timeScale = clampedGameSpeed;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            if (duration > 0)
            {
                StartCoroutine(ResetGameSpeed(amount, duration));
            }
        }
    }

    public void ResetGameSpeedMethod(float amount, float delay)
    {
        StartCoroutine(ResetGameSpeed(amount, delay));
    }

    IEnumerator ResetGameSpeed(float amount, float delay)
    {
        yield return new WaitForSeconds(delay);
        currentGameSpeed -= amount;
        clampedGameSpeed = Mathf.Clamp(currentGameSpeed, 0.01f, 10f);
        Time.timeScale = clampedGameSpeed;
    }

    #endregion
}
