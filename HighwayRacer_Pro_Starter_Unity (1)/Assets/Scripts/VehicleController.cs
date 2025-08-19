using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;

    [Header("Wheel Meshes (optional)")]
    public Transform FL_mesh, FR_mesh, RL_mesh, RR_mesh;

    [Header("Drive & Handling")]
    public float maxSteerAngle = 28f;
    public float motorTorque = 220f;
    public float brakeTorque = 1800f;
    public float clutchMul = 0.35f; // when clutch pressed
    public float maxSpeedKmh = 300f;

    [Header("Gears")]
    public int gear = 1; // 1..6
    public float[] gearRatios = {0f, 3.2f, 2.1f, 1.5f, 1.1f, 0.9f, 0.75f};
    public float finalDrive = 3.4f;
    public float wheelRadius = 0.31f; // meters
    public float rpmMin = 800f, rpmMax = 8000f;
    public float rpm;

    [Header("Inputs (keys)")]
    public KeyCode throttleKey = KeyCode.W;
    public KeyCode brakeKey = KeyCode.S;
    public KeyCode steerLeftKey = KeyCode.LeftArrow;
    public KeyCode steerRightKey = KeyCode.RightArrow;
    public KeyCode clutchKey = KeyCode.Q;
    public KeyCode gearUpKey = KeyCode.D;
    public KeyCode gearDownKey = KeyCode.A;

    [Header("Air Suspension")]
    public AirSuspension airSuspension;

    Rigidbody rb;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.3f, 0f);
    }

    void Update(){
        // Steering
        float steer = 0f;
        if (Input.GetKey(steerLeftKey)) steer -= 1f;
        if (Input.GetKey(steerRightKey)) steer += 1f;
        float targetSteer = steer * maxSteerAngle;
        FL.steerAngle = Mathf.Lerp(FL.steerAngle, targetSteer, 0.2f);
        FR.steerAngle = Mathf.Lerp(FR.steerAngle, targetSteer, 0.2f);

        // Gear change (only when clutch)
        if (Input.GetKeyDown(gearUpKey) && Input.GetKey(clutchKey)) gear = Mathf.Clamp(gear+1, 1, 6);
        if (Input.GetKeyDown(gearDownKey) && Input.GetKey(clutchKey)) gear = Mathf.Clamp(gear-1, 1, 6);

        // Update visual wheels
        UpdateWheelMesh(FL, FL_mesh);
        UpdateWheelMesh(FR, FR_mesh);
        UpdateWheelMesh(RL, RL_mesh);
        UpdateWheelMesh(RR, RR_mesh);

        // Air suspension live hotkeys
        if (airSuspension){
            if (Input.GetKeyDown(KeyCode.Alpha1)) airSuspension.SetMode(AirSuspension.Mode.FrontLow);
            if (Input.GetKeyDown(KeyCode.Alpha2)) airSuspension.SetMode(AirSuspension.Mode.AllHigh);
            if (Input.GetKeyDown(KeyCode.Alpha3)) airSuspension.SetMode(AirSuspension.Mode.Slammed);
        }
    }

    void FixedUpdate(){
        // Speeds
        float speedKmh = rb.velocity.magnitude * 3.6f;
        float throttle = Input.GetKey(throttleKey) ? 1f : 0f;
        float brake = Input.GetKey(brakeKey) ? 1f : 0f;
        bool clutch = Input.GetKey(clutchKey);

        // Motor torque affected by clutch
        float appliedTorque = motorTorque * (clutch ? clutchMul : 1f);

        // Apply to rear wheels
        RL.motorTorque = appliedTorque * throttle;
        RR.motorTorque = appliedTorque * throttle;

        // Brakes
        float bt = brake * brakeTorque;
        FL.brakeTorque = bt;
        FR.brakeTorque = bt;
        RL.brakeTorque = bt;
        RR.brakeTorque = bt;

        // Limit max speed
        if (speedKmh > maxSpeedKmh){
            RL.motorTorque = 0f;
            RR.motorTorque = 0f;
        }

        // RPM estimate from wheel speed & gear
        float wheelRps = (rb.velocity.magnitude / (2f*Mathf.PI*wheelRadius));
        float gr = gearRatios[Mathf.Clamp(gear,1,6)] * finalDrive;
        rpm = Mathf.Clamp(wheelRps * gr * 60f, rpmMin, rpmMax);
    }

    void UpdateWheelMesh(WheelCollider wc, Transform mesh){
        if (!wc || !mesh) return;
        Vector3 pos; Quaternion rot;
        wc.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }

    public float GetSpeedKmh(){
        return rb ? rb.velocity.magnitude * 3.6f : 0f;
    }
}
