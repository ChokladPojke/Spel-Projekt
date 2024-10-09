using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    public float hoverHeight = 0.5f;
    public float hoverSpeed = 2f;
    private float initialY;

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        float newY = initialY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
