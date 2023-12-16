using Accord.Math.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using static FullNameGenerator;

namespace SalesTrend.WPF.Models.Helpers;

public class DataGenerator
{
    private static Random _random = new Random();
    private static int _entityIdCounter { get; set; } = 100000;
    public static List<Company> GenerateCompanies()
    {
        var companyNames = new List<string>();

        var companies = companyNames.Select(data =>
        {
            var email = $"{data.Replace(" ", "").ToLower()}@mail.ru";
            var url = $"https://{data.Split(' ')[1].ToLower()}.рф";

            var generator = new FullNameGenerator();
            var fullname = generator.GetFullNameString(generator.GenerateRandomFullName());
            return new Company
            {
                CompanyId = Guid.NewGuid(),
                Name = data,
                Email = email,
                Url = url
            };
        }).ToList();

        return companies;
    }

    public static List<Phone> GeneratePhonesForCompany(Guid companyId, int count)
    {
        var phones = new List<Phone>();

        for (int i = 0; i < count; i++)
        {
            phones.Add(new Phone
            {
                PhoneId = _entityIdCounter--,
                PhoneNumber = $"+7{_random.Next(900, 999)}{_random.Next(100, 999)}{_random.Next(1000, 9999)}",
                EntityId = companyId
            });
        }

        return phones;
    }

    public List<Product> GenerateProducts(int count)
    {
        if (count > ElectronicProductNames.Count)
        {
            throw new ArgumentException("Количество не может превышать общее количество доступных наименований товаров.");
        }

        List<Product> products = new List<Product>();
        List<string> availableProductNames = new List<string>(ElectronicProductNames);

        for (int i = 1; i <= count; i++)
        {
            int randomIndex = _random.Next(availableProductNames.Count);
            string productName = availableProductNames[randomIndex];
            availableProductNames.RemoveAt(randomIndex);

            Product product = new Product
            {
                ProductId = _entityIdCounter--,
                Article = GenerateArticle("АРТ-", 6),
                Name = productName
            };

            products.Add(product);
        }

        return products;
    }
    private string GenerateArticle(string prefix, int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string randomPart = new string(Enumerable.Repeat(chars, length - prefix.Length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
        return prefix + randomPart;
    }

    public List<string> CompanyNames { get; } = new List<string>
        {
            ("ООО ТехноТрейд"),
            ("ЗАО ЭлектроМагазин"),
            ("ПАО Инновационные Решения"),
            ("ООО Торговый Элит"),
            ("ЗАО Продвинутые Технологии"),
            ("ПАО ЭлектроСпектр"),
            ("ООО Торговый Центр Гарант"),
            ("ООО ЭкоЭлектроника"),
            ("ПАО Современные Тренды"),
            ("ООО ТехноДизайн")
        };

    public List<string> ElectronicProductNames { get; } = new List<string>
        {
            "Смартфон", "Ноутбук", "Смарт-телевизор", "Цифровая камера", "Беспроводные наушники",
            "Фитнес-трекер", "Робот-пылесос", "Смарт-термостат", "Дрон", "Игровая консоль",
            "Наушники", "Планшет", "Смарт-часы", "Камера видеонаблюдения", "Электрическая зубная щетка",
            "Портативное зарядное устройство", "Умный домашний хаб", "Bluetooth-колонка", "Электронная книга", "Электробритва",
            "Беспроводной маршрутизатор", "VR-шлем", "Мини-проектор", "Беспроводная мышь", "Внешний жесткий диск",
            "LED-настольная лампа", "Кофеварка", "Очиститель воздуха", "Смарт-дверной звонок", "Холодильник с функцией Смарт",
            "Видеорегистратор для автомобиля", "Репитер Wi-Fi сигнала", "3D-принтер", "Домашний кинотеатр", "Смарт-зеркало",
            "Умные весы", "Беспроводная клавиатура", "Беспроводные наушники с Bluetooth", "Экшн-камера", "USB-C хаб",
            "Умный замок", "Умный сад", "Графический планшет", "Солнечный портативный аккумулятор", "Смарт-кофеварка",
            "Мини-кондиционер", "Подводный дрон"
        };

    public Individual GenerateIndividual(int addressId)
    {
        var generator = new FullNameGenerator();
        var fullname = generator.GenerateRandomFullName();
        var individual = new Individual
        {
            IndividualId = Guid.NewGuid(),
            Name = fullname.Name,
            Surname = fullname.Surname,
            Patronymic = fullname.Patronymic,
            //AddressId = addressId,
            TaxNumber = _random.Next(100000000, 999999999),
            PassportSerial = _random.Next(1000, 9999),
            PassportNumber = _random.Next(100000, 999999),
            IssueDate = GenerateRandomDate(),
            IssuedBy = "отделением выдачи паспортов г. Н"
        };

        return individual;
    }
    private DateTime GenerateRandomDate()
    {
        int range = (new DateTime(2040, 1, 1) - new DateTime(2025, 1, 1)).Days;
        return new DateTime(2025, 1, 1).AddDays(_random.Next(range));
    }

    public LegalEntity GenerateLegalEntity(int addressId)
    {
        var generator = new FullNameGenerator();
        var fullname = generator.GenerateRandomFullName();
        var companyName = CompanyNames[_random.Next(CompanyNames.Count)];

        var legalEntity = new LegalEntity
        {
            LegalEntityId = Guid.NewGuid(),
            Name = companyName,
            TaxNumber = _random.Next(100000000, 999999999),
            ContactPersonFullName = generator.GetFullNameString(generator.GenerateRandomFullName()),
            Email = $"{companyName.Replace(" ", "").ToLower()}@mail.ru",
            ShortName = $"{companyName.Split(' ')[1]}"
        };

        return legalEntity;
    }


    #region address
    public static List<Address> GenerateAddressesForCompany(Guid companyId, int count,
        List<Street> streets, List<StreetType> streetsTypes, List<LocalityType> localityTypes, List<Locality> localities)
    {

        var addresses = new List<Address>();

        for (int i = 0; i < count; i++)
        {
            addresses.Add(new Address
            {
                AddressId = _entityIdCounter--,
                House = _random.Next(1, 100).ToString(),
                Apartment = _random.Next(1, 200).ToString(),
                StreetId = streets[_random.Next(streets.Count)].StreetId,
                LocalityId = localities[_random.Next(localities.Count)].LocalityId,
                CompanyId = companyId
            });
        }

        return addresses;
    }

    public static List<Street> GenerateStreets(List<StreetType> streetTypes)
    {
        var streetNames = new[]
        {
                "Ленина", "Мира", "Советская", "Молодежная", "Центральная",
                "Школьная", "Новая", "Заречная", "Садовая", "Луговая"
            };

        var streets = new List<Street>();
        foreach (var name in streetNames)
        {
            streets.Add(new Street
            {
                StreetId = _entityIdCounter--,
                Name = name,
                StreetTypeId = streetTypes[_random.Next(streetTypes.Count)].StreetTypeId
            });
        }
        return streets;
    }

    public static List<StreetType> GenerateStreetTypes()
    {
        return new List<StreetType>
            {
                new StreetType { StreetTypeId = 1, Name = "Улица", ShortName = "ул." },
                new StreetType { StreetTypeId = 2, Name = "Проспект", ShortName = "пр." },
                new StreetType { StreetTypeId = 3, Name = "Бульвар", ShortName = "бул." },
                new StreetType { StreetTypeId = 4, Name = "Переулок", ShortName = "пер." },
                new StreetType { StreetTypeId = 5, Name = "Проезд", ShortName = "пр-д" }
            };
    }

    public static List<Locality> GenerateLocalities(List<LocalityType> localityTypes)
    {
        var localityNames = new[]
        {
                "Погорелка", "Дубровка",
                "Вязовка", "Каменка",
                "Березовка", "Степаново",
                "Липово", "Заборье",
                "Терновка", "Черничка"
            };

        var localities = new List<Locality>();
        int idCounter = -1;  // Начните с отрицательного значения
        foreach (var name in localityNames)
        {
            localities.Add(new Locality
            {
                LocalityId = _entityIdCounter--,  // Уменьшайте значение на каждой итерации
                Name = name,
                LocalityTypeId = localityTypes[_random.Next(localityTypes.Count)].LocalityTypeId
            });
        }
        return localities;
    }

    public static List<LocalityType> GenerateLocalityTypes()
    {
        return new List<LocalityType>
                {
                    new LocalityType { LocalityTypeId = 1, Name = "Город", ShortName = "г." },
                    new LocalityType { LocalityTypeId = 2, Name = "Поселок", ShortName = "п." },
                    new LocalityType { LocalityTypeId = 3, Name = "Деревня", ShortName = "д." },
                    new LocalityType { LocalityTypeId = 4, Name = "Хутор", ShortName = "х." },
                    new LocalityType { LocalityTypeId = 5, Name = "Село", ShortName = "с." }
                };
    }
    #endregion

}
