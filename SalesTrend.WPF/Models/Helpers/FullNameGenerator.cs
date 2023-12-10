using System;

public class FullNameGenerator
{
    private readonly string[] maleNames = { "Александр", "Иван", "Михаил", "Дмитрий", "Сергей", "Николай", "Андрей", "Юрий", "Владимир", "Артем" };
    private readonly string[] femaleNames = { "Александра", "Ирина", "Мария", "Екатерина", "Светлана", "Наталья", "Анна", "Юлия", "Елена", "Ольга" };
    private readonly string[] lastNames = { "Иванов", "Петров", "Сидоров", "Козлов", "Морозов", "Новиков", "Кузнецов", "Павлов", "Васильев", "Смирнов" };
    private readonly Random random = new Random();

    public class FullName
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Gender Gender { get; set; }
    }

    public FullName GenerateRandomFullName()
    {
        Gender gender = (Gender)random.Next(2);

        string firstName, lastName, patronymic;

        if (gender == Gender.Male)
        {
            firstName = maleNames[random.Next(maleNames.Length)];
        }
        else
        {
            firstName = femaleNames[random.Next(femaleNames.Length)];
        }

        lastName = lastNames[random.Next(lastNames.Length)];

        patronymic = GeneratePatronymic(gender, firstName);

        return new FullName
        {
            Name = firstName,
            Surname = lastName,
            Patronymic = patronymic,
            Gender = gender
        };
    }

    private string GeneratePatronymic(Gender gender, string firstName)
    {
        string patronymic;

        if (gender == Gender.Male)
        {
            patronymic = firstName.EndsWith("а") ? firstName + "ович" : firstName + "евич";
        }
        else
        {
            patronymic = firstName.EndsWith("а") ? firstName + "овна" : firstName + "евна";
        }

        return patronymic;
    }

    public string GetFullNameString(FullName fullName)
    {
        return $"{(fullName.Gender == Gender.Male ? "Мужчина" : "Женщина")}: {fullName.Surname} {fullName.Name} {fullName.Patronymic}";
    }

    public enum Gender
    {
        Male,
        Female
    }
}