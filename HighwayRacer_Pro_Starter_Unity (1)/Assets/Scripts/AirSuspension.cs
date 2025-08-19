using UnityEngine;

public class AirSuspension : MonoBehaviour
{
    public enum Mode { Off, FrontLow, AllHigh, Slammed }
    public Transform body; // parent transform to move slightly
    public float frontOffset = -0.12f;
    public float allHigh = 0.20f;
    public float slammed = -0.18f;
    public float smooth = 6f;

    Mode mode = Mode.Off;
    float targetY = 0f;

    public void SetMode(Mode m){
        mode = m;
        switch(mode){
            case Mode.FrontLow: targetY = (frontOffset/2f); break;
            case Mode.AllHigh: targetY = allHigh; break;
            case Mode.Slammed: targetY = slammed; break;
            default: targetY = 0f; break;
        }
    }

    void Update(){
        if (!body) return;
        var pos = body.localPosition;
        pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * smooth);
        body.localPosition = pos;
    }
}
