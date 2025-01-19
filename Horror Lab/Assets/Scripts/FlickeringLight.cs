using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light pointLight;
    public float minIntensity = 3.75f;
    public float maxIntensity = 4.5f;
    public float flickerSpeed = 0.3f;

    private void Update()
    {
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1));
        pointLight.intensity = intensity;
    }
}
