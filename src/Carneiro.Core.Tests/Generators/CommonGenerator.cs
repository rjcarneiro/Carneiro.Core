namespace Carneiro.Core.Tests.Generators;

/// <summary>
/// Common generator.
/// </summary>
public static class CommonGenerator
{
    /// <summary>
    /// Generates the unique identifier.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static string GenerateUniqueId(this Faker faker) => faker.Random.Guid().ToString("N");

    /// <summary>
    /// Generates the email.
    /// </summary>
    public static string GenerateEmail(this Faker faker) => $"{faker.Random.String2(5)}{faker.GetTicks()}{faker.Random.String2(7)}@{faker.Internet.DomainWord()}.{faker.Internet.DomainSuffix()}";

    /// <summary>
    /// Generates the name.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static string GenerateName(this Faker faker) => $"{faker.Random.String2(5)}{faker.GetTicks()}{faker.Commerce.Product()}{faker.Random.String2(7)}";

    /// <summary>
    /// Generates the phone number.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static string GeneratePhoneNumber(this Faker faker) => faker.Random.Replace("89#######");

    /// <summary>
    /// Generates the password.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static string GeneratePassword(this Faker faker) => faker.Random.Replace("??#?#?#?##%/#?#???3?#?##=?ºç»#?3csx??xf%67#$###");

    /// <summary>
    /// Picks the random long except <paramref name="unexpectedValue" />.
    /// </summary>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="unexpectedValue">The unexpected value.</param>
    public static long PickRandomLongExcept(this BaseScenario baseScenario, long unexpectedValue) => baseScenario.Faker.PickRandomLongExcept(unexpectedValue);

    /// <summary>
    /// Picks the random long except <paramref name="unexpectedValue" />.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="unexpectedValue">The unexpected value.</param>
    public static long PickRandomLongExcept(this Faker faker, long unexpectedValue)
    {
        long randomAccountId;

        do
        {
            randomAccountId = faker.GenerateLongId();
        } while (randomAccountId == unexpectedValue);

        return randomAccountId;
    }

    /// <summary>
    /// Shuffles the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    public static List<T> Shuffle<T>(this List<T> items) => items.OrderBy(_ => Guid.NewGuid()).ToList();

    /// <summary>
    /// Picks the random int except <paramref name="unexpectedValue"/>.
    /// </summary>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="unexpectedValue">The unexpected value.</param>
    public static int PickRandomIntExcept(this BaseScenario baseScenario, int unexpectedValue) => baseScenario.Faker.PickRandomIntExcept(unexpectedValue);

    /// <summary>
    /// Picks the random int except <paramref name="unexpectedValue"/>.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="unexpectedValue">The unexpected value.</param>
    public static int PickRandomIntExcept(this Faker faker, int unexpectedValue)
    {
        int randomAccountId;

        do
        {
            randomAccountId = faker.GenerateIntId();
        } while (randomAccountId == unexpectedValue);

        return randomAccountId;
    }

    /// <summary>
    /// Picks a new random decimal except <paramref name="unexpectedValue"/>.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="unexpectedValue">The unexpected value.</param>
    public static decimal PickRandomDecimalExcept(this Faker faker, decimal unexpectedValue)
    {
        decimal randomAccountId;

        do
        {
            randomAccountId = faker.Random.GenerateDecimalWithTwoPlaces();
        } while (randomAccountId == unexpectedValue);

        return randomAccountId;
    }

    /// <summary>
    /// Picks the random string except <paramref name="unexpectedStr"/> with <c>10</c> length maximum.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="unexpectedStr">The unexpected string.</param>
    public static string PickRandomStringExcept(this Faker faker, string unexpectedStr) => faker.PickRandomStringExcept(unexpectedStr, maxLength: 10);

    /// <summary>
    /// Picks the random string except <paramref name="unexpectedStr" /> with <c>2</c> minimum length and <paramref name="maxLength" /> maximum length.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="unexpectedStr">The unexpected string.</param>
    /// <param name="maxLength">The maximum length.</param>
    public static string PickRandomStringExcept(this Faker faker, string unexpectedStr, int maxLength) => faker.PickRandomStringExcept(unexpectedStr, minLength: 2, maxLength: 10);

    /// <summary>
    /// Picks the random string except <paramref name="unexpectedStr" /> between <paramref name="minLength" /> and <paramref name="maxLength" /> length.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="unexpectedStr">The unexpected string.</param>
    /// <param name="minLength">The minimum length.</param>
    /// <param name="maxLength">The maximum length.</param>
    public static string PickRandomStringExcept(this Faker faker, string unexpectedStr, int minLength, int maxLength)
    {
        string newStr;
        do
        {
            newStr = faker.Random.String2(minLength: minLength, maxLength: maxLength);
        } while (newStr.Equals(unexpectedStr, StringComparison.OrdinalIgnoreCase));

        return newStr;
    }

    /// <summary>
    /// Generates the decimal with two places.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static decimal GenerateDecimalWithTwoPlaces(this Faker faker) => faker.Random.GenerateDecimalWithTwoPlaces();

    /// <summary>
    /// Generates the decimal with two places.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="min">The minimum value in decimal.</param>
    /// <param name="max">The maximum value in decimal.</param>
    public static decimal GenerateDecimalWithTwoPlaces(this Faker faker, decimal min, decimal max) => faker.Random.GenerateDecimalWithPlaces(2, min, max);

    /// <summary>
    /// Generates the decimal with two places.
    /// </summary>
    /// <param name="randomizer">The randomizer.</param>
    public static decimal GenerateDecimalWithTwoPlaces(this Randomizer randomizer) => randomizer.GenerateDecimalWithPlaces(2);

    /// <summary>
    /// Generates the decimal with <paramref name="places"/> places.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="places">The places.</param>
    public static decimal GenerateDecimalWithPlaces(this Faker faker, int places) => faker.Random.GenerateDecimalWithPlaces(places);

    /// <summary>
    /// Generates the decimal with <paramref name="places"/> places.
    /// </summary>
    /// <param name="randomizer">The randomizer.</param>
    /// <param name="places">The places.</param>
    public static decimal GenerateDecimalWithPlaces(this Randomizer randomizer, int places) => randomizer.GenerateDecimalWithPlaces(places, min: .01M, max: 25000);

    /// <summary>
    /// Generates the decimal with places.
    /// </summary>
    /// <param name="randomizer">The randomizer.</param>
    /// <param name="places">The places.</param>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    public static decimal GenerateDecimalWithPlaces(this Randomizer randomizer, int places, int min, int max) => Math.Round(randomizer.Decimal(min, max), places);

    /// <summary>
    /// Generates the decimal with places.
    /// </summary>
    /// <param name="randomizer">The randomizer.</param>
    /// <param name="places">The places.</param>
    /// <param name="min">The minimum value in decimal.</param>
    /// <param name="max">The maximum value in decimal.</param>
    public static decimal GenerateDecimalWithPlaces(this Randomizer randomizer, int places, decimal min, decimal max) => Math.Round(randomizer.Decimal(min, max), places);

    /// <summary>
    /// Generates the identifier.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static long GenerateLongId(this Faker faker) => faker.Random.Long(min: 1);

    /// <summary>
    /// Generates the identifier.
    /// </summary>
    /// <param name="faker">The faker.</param>
    public static int GenerateIntId(this Faker faker) => faker.Random.Int(min: 1);

    /// <summary>
    /// Generates the int identifier.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="min">The minimum.</param>
    public static int GenerateIntId(this Faker faker, int min) => faker.Random.Int(min: min);

    /// <summary>
    /// Picks a random string from <paramref name="list"/> without items in the <paramref name="without"/> list.
    /// </summary>
    /// <param name="faker">The faker.</param>
    /// <param name="list">The list.</param>
    /// <param name="without">The without.</param>
    public static string PickRandomWithout(this Faker faker, IEnumerable<string> list, IEnumerable<string> without) => faker.PickRandom<string>(list.Except(without));

    /// <summary>
    /// Gets the <see cref="DateTime.Ticks"/> from <see cref="DateTime.UtcNow"/>.
    /// </summary>
    /// <param name="faker"></param>
    public static string GetTicks(this Faker faker) => DateTime.UtcNow.Ticks.ToString();

    /// <summary>
    /// Converts a <see cref="DateTime"/> from <see cref="DateTime.UtcNow"/> into a random hour, minute and second of the same day.
    /// </summary>
    /// <param name="faker"></param>
    public static DateTime ToRandomTimeInSameDay(this Faker faker) => faker.ToRandomTimeInSameDay(DateTime.UtcNow);

    /// <summary>
    /// Converts a <paramref name="dateTime"/> into a random hour, minute and second of the same day.
    /// </summary>
    /// <param name="faker"></param>
    /// <param name="dateTime"></param>
    public static DateTime ToRandomTimeInSameDay(this Faker faker, DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day, faker.Random.Int(0, 23), faker.Random.Int(0, 59), faker.Random.Int(0, 59), DateTimeKind.Utc);

    /// <summary>
    /// Converts a <paramref name="dateTime"/> into a random hour, minute and second of the same day.
    /// </summary>
    /// <param name="faker"></param>
    /// <param name="dateTime"></param>
    public static DateTime ToRandomTimeInSameDay(this Bogus.DataSets.Date faker, DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day, faker.Random.Int(0, 23), faker.Random.Int(0, 59), faker.Random.Int(0, 59), DateTimeKind.Utc);
}