using UnityEngine;

public class WeatherTimeManager : MonoBehaviour
{
    public Light sun;
    public Material skyboxDay;
    public Material skyboxNight;
    public bool rainy;
    public bool foggy;
    [Range(0f,2f)] public float timeOfDay = 1f; // 0 شب، 1 ظهر، 2 شب

    void Update(){
        if (RenderSettings.skybox && sun){
            if (timeOfDay <= 0.5f){ // صبح/روز
                RenderSettings.skybox = skyboxDay;
                sun.intensity = 1.0f;
            } else if (timeOfDay < 1.5f){ // عصر
                RenderSettings.skybox = skyboxDay;
                sun.intensity = 0.7f;
            } else { // شب
                RenderSettings.skybox = skyboxNight;
                sun.intensity = 0.2f;
            }
        }
        RenderSettings.fog = foggy;
        if (foggy){
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = 0.012f;
        }
        // Rain FX را با ParticleSystem جدا بسازید و بر اساس 'rainy' فعال/غیرفعال کنید.
    }
}
