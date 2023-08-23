using UnityEngine;

public class AllDataSave : MonoBehaviour//в awake копирует данные из серил файла
{
    public static AllDataSave Instance { get; private set; }

    private Storage storage;
    private SavedData savedData;

    public int NumberAvatar;//номер аватара
    public int NumberCurrentWins;//колво правельных ответов в текущей игре
    public int NumberTotalWins;//колво правельных ответов в общем
    public int NumberCurrentTotalWins;//колво правельных ответов в общем, для сохранения
    public int RemainingTimeBeforeWarning;//оставшееся время для ответа
    public int NumberChance;//номер попытки
    public string GoldenAchievements;
    public string SilverAchievements;
    public int[] Characters;
    public int[] Things;
    public bool[] Answers;
    public int NumberBackground;
    public int NumberAnswer;//номер текущего ответа
    public int RemainingNumberAttempts;//осталось ответов
    public string LastCurrentAchievement;//последнее достижение в текущей игре
    public string NameTotalLastAchievement;//последне достижение
    public int NumberCompletedGames;//колво пройденных игр
    public bool IsOutOfGame;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
        savedData.NumberAvatart = NumberAvatar;
        savedData.NumberCurrentWins = NumberCurrentWins;
        savedData.NumberTotalWins = NumberTotalWins;
        savedData.NumberCurrentTotalWins = NumberCurrentTotalWins;
        savedData.RemainingTimeBeforeWarning = RemainingTimeBeforeWarning;
        savedData.NumberChance = NumberChance;
        savedData.GoldenAchievements = GoldenAchievements;
        savedData.SilverAchievements = SilverAchievements;
        savedData.Characters = Characters;
        savedData.Answers = Answers;
        savedData.Things = Things;
        savedData.NumberBackground = NumberBackground;
        savedData.NumberAnswer = NumberAnswer;
        savedData.RemainingNumberAttempts = RemainingNumberAttempts;
        savedData.LastCurrentAchievement = LastCurrentAchievement;
        savedData.NameTotalLastAchievement = NameTotalLastAchievement;
        savedData.NumberCompletedGames = NumberCompletedGames;
        savedData.IsOutOfGame = IsOutOfGame;

        storage.Save(savedData);
    }

#if UNITY_EDITOR
    [ContextMenu("Delete data")]
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();

        storage = new Storage();
        storage.DeleteData();

        Debug.Log("Данные удалены!");
    }
#endif
    public void NullAndSaveArraysData()
    { 
        Characters = null;
        Answers = null;
        Things = null;
    }
    public void NullAndSaveLevelData()
    {
        SaveLastAcvievement(null);
        RemainingNumberAttempts = 0;
        NumberChance = 0;
        RemainingTimeBeforeWarning = 0;
    }
    public void LoadAll()
    {
        savedData = (SavedData)storage.Load(new SavedData());

        NumberAvatar = savedData.NumberAvatart;
        NumberCurrentWins = savedData.NumberCurrentWins;
        NumberTotalWins = savedData.NumberTotalWins;
        NumberCurrentTotalWins = savedData.NumberCurrentTotalWins;
        RemainingTimeBeforeWarning = savedData.RemainingTimeBeforeWarning;
        NumberChance = savedData.NumberChance;
        GoldenAchievements = savedData.GoldenAchievements;
        SilverAchievements = savedData.SilverAchievements;

        Characters = savedData.Characters;
        Answers = savedData.Answers;
        Things = savedData.Things;
        NumberBackground = savedData.NumberBackground;
        NumberAnswer = savedData.NumberAnswer;
        RemainingNumberAttempts = savedData.RemainingNumberAttempts;
        LastCurrentAchievement = savedData.LastCurrentAchievement;
        NumberCompletedGames = savedData.NumberCompletedGames;
        NameTotalLastAchievement = savedData.NameTotalLastAchievement;
        IsOutOfGame = savedData.IsOutOfGame;
    }
    public void SaveIsOutOfGame(bool isOut)
    {
        IsOutOfGame = isOut;
        savedData.IsOutOfGame = isOut;
        storage.Save(savedData);
    }
    public void SaveNameTotalLastAchievement(string name)
    {
        NameTotalLastAchievement = name;
        savedData.NameTotalLastAchievement = NameTotalLastAchievement;
        storage.Save(savedData);
    }
    public void SaveCurrentTotalWins(int value)
    {
        NumberCurrentTotalWins = value;
        savedData.NumberCurrentTotalWins = NumberCurrentTotalWins;
        storage.Save(savedData);
    }
    public void AddAndSaveNumberCompletedGames()
    {
        NumberCompletedGames++;
        savedData.NumberCompletedGames = NumberCompletedGames;
        storage.Save(savedData);
    }
    public void SaveLastAcvievement(string lastAchiev)
    {
        LastCurrentAchievement = lastAchiev;
        savedData.LastCurrentAchievement = LastCurrentAchievement;
        storage.Save(savedData);
    }
    public void SaveRemainingNumberAttempts(int value)
    {
        RemainingNumberAttempts = value;
        savedData.RemainingNumberAttempts = RemainingNumberAttempts;
        storage.Save(savedData);
    }
    public void SaveNumberAnswer(int num)
    {
        NumberAnswer = num;
        savedData.NumberAnswer = num;
        storage.Save(savedData);
    }
    public void SaveDataLevel(int[] chars, int[] thins, bool[] answers, int numBackground)
    {
        Characters = chars;
        savedData.Characters = Characters;
        Things = thins;
        savedData.Things = Things;
        Answers = answers;
        savedData.Answers = answers;
        NumberBackground = numBackground;
        savedData.NumberBackground = numBackground;

        storage.Save(savedData);
    }
    public void SaveSelectedNumberAvatar(int number)
    {
        NumberAvatar = number;
        savedData.NumberAvatart = NumberAvatar;
        storage.Save(savedData);
    }
    public void SaveCurrentWins(int number)
    {
        NumberCurrentWins = number;
        savedData.NumberCurrentWins = NumberCurrentWins;
        storage.Save(savedData);
    }
    public void SaveTotalWins()
    {
        NumberTotalWins += NumberCurrentWins;//NumberCurrentTotalWins;
        SaveCurrentWins(0);
        savedData.NumberTotalWins = NumberTotalWins;
        storage.Save(savedData);
    }
    public void SaveRemainingTimeBeforeWarning(int value)
    {
        RemainingTimeBeforeWarning = value;
        savedData.RemainingTimeBeforeWarning = RemainingTimeBeforeWarning;
        storage.Save(savedData);
    }
    public void SaveSecondChance(int chance )
    {
        NumberChance = chance;
        savedData.NumberChance = NumberChance;
        storage.Save(savedData);
    }
    public void SaveGoldenAchievement(string achievement)
    {
        GoldenAchievements = achievement;
        savedData.GoldenAchievements = GoldenAchievements;
        storage.Save(savedData);
    }
    public void SaveSilverAchievement(string achievement)
    {
        SilverAchievements = achievement;
        savedData.SilverAchievements = SilverAchievements;
        storage.Save(savedData);
    }
}