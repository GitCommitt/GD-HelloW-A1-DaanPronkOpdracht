using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Material daySkybox;
    public Material nightSkybox;
    public Light sun;

    public float dayLength = 120f;
    private float time;

    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;

        float t = Mathf.PingPong(time / dayLength, 1);

        sun.transform.rotation = Quaternion.Euler((t * 360f) - 90f, 170f, 0);

        if (t < 0.5f)
        {
            RenderSettings.skybox.Lerp(daySkybox, nightSkybox, t * 2);
        }
        else
        {
            RenderSettings.skybox.Lerp(nightSkybox, daySkybox, (t - 0.5f) * 2);
        }

        sun.intensity = Mathf.Lerp(1f, 0.2f, t);
    }
}
