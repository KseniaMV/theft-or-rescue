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
    public static int NumberCurrentWins { get; private set; }
    public static int NumberTotalWins { get; private set; }
    public static int RemainingTimeBeforeWarning {get; private set;}
    public static int CurrentRightAction { get; private set; }
    public static bool SecondChance { get; private set; }

    [Header("test")]
    public int numVictories, numBg, numChar, numThing, numCurWins, numTotalWins, remainingTime, currentRightAction;
    public bool seconChance;

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
        savedData.numberCurrentWins = NumberCurrentWins;
        savedData.numberTotalWins = NumberTotalWins;
        savedData.remainingTimeBeforeWarning = RemainingTimeBeforeWarning;
        savedData.currentRightAction = CurrentRightAction;
        savedData.secondChance = SecondChance;

        storage.Save(savedData);
    }

    public void LoadAll()
    {
        savedData = (SavedData)storage.Load(new SavedData());

        NumberAvatar = savedData.numberAvatart;
        numVictories = NumberVictories = savedData.numberVictories;//
        numBg =  NumberLoadedBackground = savedData.numberLoadedBackground;//
        numChar = NumberLoadedCharacter = savedData.numberLoadedCharacter;//
        numThing = NumberLoadedThing = savedData.numberLoadedThing;//
        numCurWins = NumberCurrentWins = savedData.numberCurrentWins;//
        numTotalWins = NumberTotalWins = savedData.numberTotalWins;//
        remainingTime = RemainingTimeBeforeWarning = savedData.remainingTimeBeforeWarning;//
        currentRightAction = CurrentRightAction = savedData.currentRightAction;//
        seconChance = SecondChance = savedData.secondChance;//
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

        numVictories = NumberVictories;//
    }
    public void SaveLoadedNumberBackground(int number)
    {
        NumberLoadedBackground = number;
        savedData.numberLoadedBackground = NumberLoadedBackground;
        storage.Save(savedData);
        numBg = NumberLoadedBackground;//
    }
    public void SaveLoadedNumberCharacter(int number)
    {
        NumberLoadedCharacter = number;
        savedData.numberLoadedCharacter = NumberLoadedCharacter;
        storage.Save(savedData);
        numChar = NumberLoadedCharacter;//
    }
    public void SaveLoadedNumberThing(int number)
    {
        NumberLoadedThing = number;
        savedData.numberLoadedThing = NumberLoadedThing;
        storage.Save(savedData);
        numThing = NumberLoadedThing;//
    }
    public void SaveCurrentWins(int number)
    {
        NumberCurrentWins = number;
        savedData.numberCurrentWins = NumberCurrentWins;
        storage.Save(savedData);
        numCurWins = NumberCurrentWins;//
    }
    public void SaveTotalWins()
    {
        NumberTotalWins++;
        savedData.numberTotalWins = NumberTotalWins;
        storage.Save(savedData);
        numTotalWins = NumberTotalWins;//
    }
    public void SaveRemainingTimeBeforeWarning(int value)
    {
        RemainingTimeBeforeWarning = value;
        savedData.remainingTimeBeforeWarning = RemainingTimeBeforeWarning;
        storage.Save(savedData);
        remainingTime = RemainingTimeBeforeWarning;//
    }
    public void SaveCurrentRightAction(int value)
    {
        CurrentRightAction = value;
        savedData.currentRightAction = CurrentRightAction;
        storage.Save(savedData);
        currentRightAction = CurrentRightAction;//
    }
    public void SaveSecondChance(bool chance )
    {
        SecondChance = chance;
        savedData.secondChance = SecondChance;
        storage.Save(savedData);
        seconChance = SecondChance;//
    }
}