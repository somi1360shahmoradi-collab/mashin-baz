using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VehicleController player;
    public Transform rimsParent;
    public Transform exhaustsParent;
    public Transform carBody; // for air suspension

    public AirSuspension airSuspension;

    void Start(){
        var data = SaveLoad.Load();
        if (data == null) data = SaveLoad.Current;

        // Apply rims/exhaust
        if (rimsParent){
            for (int i=0;i<rimsParent.childCount;i++) rimsParent.GetChild(i).gameObject.SetActive(i == data.rimId);
        }
        if (exhaustsParent){
            for (int i=0;i<exhaustsParent.childCount;i++) exhaustsParent.GetChild(i).gameObject.SetActive(i == data.exhaustId);
        }

        // Air suspension binds to body
        if (airSuspension){
            airSuspension.body = carBody;
        }
    }
}
