using UnityEngine;

public class StaticHover : MonoBehaviour
{
    [Tooltip("Height of bobbing motion")]
    public float hoverAmplitude = 0.5f;

    [Tooltip("Speed of bobbing motion")]
    public float hoverFrequency = 1f;

    private Vector3 startPosition;

    void Start()
    {
        // Record initial position so the Ghoul doesn't drift over time
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
