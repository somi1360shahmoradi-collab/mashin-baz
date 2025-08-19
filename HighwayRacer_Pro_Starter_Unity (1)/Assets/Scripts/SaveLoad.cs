using UnityEngine;

public static class SaveLoad
{
    [System.Serializable]
    public class Data{
        public int carId;
        public int rimId;
        public int exhaustId;
        public float rideF;
        public float rideR;
    }

    public static Data Current = new Data();

    public static void Save(){
        PlayerPrefs.SetString("HR_Data", JsonUtility.ToJson(Current));
        PlayerPrefs.Save();
    }

    public static Data Load(){
        if (!PlayerPrefs.HasKey("HR_Data")) return null;
        try{
            return JsonUtility.FromJson<Data>(PlayerPrefs.GetString("HR_Data"));
        } catch { return null; }
    }
}
