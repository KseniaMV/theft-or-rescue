using UnityEngine;

public class AllDataSave : MonoBehaviour//в awake копирует данные из серил файла
{
    private Storage storage;
    private SavedData savedData;

    public static int NumberLanguage { get; private set; }
    public static int NumberAvatar { get; private set; }

    private void Awake()
    {
        storage = new Storage();
        savedData = new SavedData();

        LoadAll();
    }
    private void OnApplicationQuit()
    {
        SaveAll();
    }
    public void SaveAll()
    {
        savedData.numberLanguage = NumberLanguage;
        savedData.numberAvatart = NumberAvatar;

        storage.Save(savedData);
    }

    public void LoadAll()
    {
        savedData = (SavedData)storage.Load(new SavedData());

        NumberLanguage = savedData.numberLanguage;
        NumberAvatar = savedData.numberAvatart;
    }
    public void SelectNumberLanguage(int number)
    {
        NumberLanguage = number;
        savedData.numberLanguage = NumberLanguage;
        storage.Save(savedData);
        //change languge
    }
    public void ConfirmSelectedNumberAvatar(int number)
    {
        NumberAvatar = number;
        savedData.numberAvatart = NumberAvatar;
        storage.Save(savedData);
        //change avatars in other panels
    }

}
