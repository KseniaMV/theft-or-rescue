using UnityEngine;

public class AllDataSave : MonoBehaviour//в awake копирует данные из серил файла
{
    public static AllDataSave instance { get; private set; }

    private Storage storage;
    private SavedData savedData;

    public static int NumberAvatar { get; private set; }
    public static int NumberVictories { get; private set; }
    public static int NumberLoadedBackground { get; private set; }
    public static int NumberLoadedCharacter { get; private set; }
    public static int NumberLoadedThing { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

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
        savedData.numberAvatart = NumberAvatar;
        savedData.numberVictories = NumberVictories;
        savedData.numberLoadedBackground = NumberLoadedBackground;
        savedData.numberLoadedCharacter = NumberLoadedCharacter;
        savedData.numberLoadedThing = NumberLoadedThing;

        storage.Save(savedData);
    }

    public void LoadAll()
    {
        savedData = (SavedData)storage.Load(new SavedData());

        NumberAvatar = savedData.numberAvatart;
        NumberVictories = savedData.numberVictories;
        NumberLoadedBackground = savedData.numberLoadedBackground;
        NumberLoadedCharacter = savedData.numberLoadedCharacter;
        NumberLoadedThing = savedData.numberLoadedThing;
    }
    public void ConfirmSelectedNumberAvatar(int number)
    {
        NumberAvatar = number;
        savedData.numberAvatart = NumberAvatar;
        storage.Save(savedData);
    }
    public void SaveNumberVictory(int number)
    {
        NumberVictories = number;
    }
    public void SaveLoadedNumberBackground(int number)
    {
        NumberLoadedBackground = number;
        savedData.numberLoadedBackground = NumberLoadedBackground;
        storage.Save(savedData);
    }
    public void SaveLoadedNumberCharacter(int number)
    {
        NumberLoadedCharacter = number;
        savedData.numberLoadedCharacter = NumberLoadedCharacter;
        storage.Save(savedData);
    }
    public void SaveLoadedNumberThing(int number)
    {
        NumberLoadedThing = number;
        savedData.numberLoadedThing = NumberLoadedThing;
        storage.Save(savedData);
    }
}