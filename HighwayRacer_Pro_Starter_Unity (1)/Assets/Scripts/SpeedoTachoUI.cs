using UnityEngine;
using UnityEngine.UI;

public class SpeedoTachoUI : MonoBehaviour
{
    public VehicleController vehicle;
    public Text speedText;
    public RectTransform speedNeedle; // rotate -120 .. +120 deg (0..300 km/h)
    public RectTransform rpmNeedle;   // rotate -120 .. +120 deg (800..8000 rpm)

    void Update(){
        if (!vehicle) return;
        float kmh = vehicle.GetSpeedKmh();
        if (speedText) speedText.text = Mathf.RoundToInt(kmh).ToString();

        float spNorm = Mathf.Clamp01(kmh / 300f);
        float spAng = Mathf.Lerp(-120f, 120f, spNorm);
        if (speedNeedle) speedNeedle.localRotation = Quaternion.Euler(0,0, -spAng);

        float rpmNorm = Mathf.InverseLerp(vehicle.rpmMin, vehicle.rpmMax, vehicle.rpm);
        float rpmAng = Mathf.Lerp(-120f, 120f, rpmNorm);
        if (rpmNeedle) rpmNeedle.localRotation = Quaternion.Euler(0,0, -rpmAng);
    }
}
