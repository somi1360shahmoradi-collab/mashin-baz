using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageUI : MonoBehaviour
{
    [Header("UI")]
    public Dropdown carDropdown;
    public Dropdown rimDropdown;
    public Dropdown exhaustDropdown;
    public Slider rideFront;
    public Slider rideRear;
    public Button startButton;

    [Header("Preview Target")]
    public Transform carRoot; // the car in garage center
    public Transform rimsParent;
    public Transform exhaustsParent;

    [Header("Materials")]
    public Renderer bodyRenderer;
    public Color defaultColor = new Color(0.12f,0.56f,1f);

    void Start(){
        if (startButton) startButton.onClick.AddListener(OnStart);
        Apply();
    }

    public void Apply(){
        // Show/hide rim & exhaust variants by selection index
        if (rimsParent){
            for(int i=0;i<rimsParent.childCount;i++) rimsParent.GetChild(i).gameObject.SetActive(i == rimDropdown.value);
        }
        if (exhaustsParent){
            for(int i=0;i<exhaustsParent.childCount;i++) exhaustsParent.GetChild(i).gameObject.SetActive(i == exhaustDropdown.value);
        }
        // Height adjust preview
        if (carRoot){
            var p = carRoot.localPosition;
            p.y = (rideFront.value + rideRear.value) * 0.5f;
            carRoot.localPosition = p;
        }
        SaveLoad.Current = new SaveLoad.Data{
            carId = carDropdown.value,
            rimId = rimDropdown.value,
            exhaustId = exhaustDropdown.value,
            rideF = rideFront.value,
            rideR = rideRear.value
        };
        SaveLoad.Save();
    }

    void OnStart(){
        Apply();
        SceneManager.LoadScene("Highway");
    }
}
