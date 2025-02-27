using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Camera cam;  
    public float colorChangeSpeed = 1.0f;  // Brzina promjene boje

    private Color[] colors = new Color[] { new Color(0.29f, 0.00f, 0.51f), new Color(0.33f, 0.42f, 0.18f), new Color(0.00f, 0.00f, 0.55f) }; 
    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0.0f;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        cam.backgroundColor = colors[currentColorIndex];
    }

    void Update()
    {
        cam.backgroundColor = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);
        t += Time.deltaTime * colorChangeSpeed;

        if (t >= 1.0f)
        {
            t = 0.0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % colors.Length;
        }
    }
}
