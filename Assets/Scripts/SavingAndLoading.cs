using UnityEngine;

public class Saving_And_Loading : MonoBehaviour
{
[System.Serializable]
    public class SaveData
    {
        public Vector3 playerPos;
    }

    public static void SaveCurrentData()
    {
        SaveData newSaveData = new SaveData();
        //This is currently not working I think it's because there isn't an object to record the player save data I'm most likely wrong about this
        //newSaveData.playerPos = ProtagonistController.i.transform.position;

        string convertedData = JsonUtility.ToJson(newSaveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveData.json", convertedData);
    }

    public static SaveData LoadData()
    {
        string dataToLoad = System.IO.File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        return JsonUtility.FromJson<SaveData>(dataToLoad);
    }
    public static bool IsThereDataToLoad()
    {
        return System.IO.File.Exists(Application.persistentDataPath + "/SaveData.json");
    }

    public static void DeleteAllData()
    {
        //Deletes the Json file to find it and manually delete the file you need to go to Users -> your account name -> AppData -> LowcalLow -> business name
        System.IO.File.Delete(Application.persistentDataPath + "/SaveData.json");
    }

}
