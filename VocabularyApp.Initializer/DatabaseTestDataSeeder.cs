using VocabularyApp.Initializer.DataProviders;

namespace VocabularyApp.Initializer;

public interface IDatabaseTestDataSeeder
{
    Task SeedTestDataAsync();
}

public class DatabaseTestDataSeeder : IDatabaseTestDataSeeder
{
    private readonly IDatabaseTestDataSeederDataProvider dataProvider;

    public DatabaseTestDataSeeder(IDatabaseTestDataSeederDataProvider dataProvider)
    {
        this.dataProvider = dataProvider;
    }

    public async Task SeedTestDataAsync()
    {
        var words = new List<Application.Entities.Word>()
        {
            new() { WordTitle = "adaptál", Description = "1. Alkalmassá tesz, átalakít, átformál, átdolgoz, 2. valamilyen gépet, készüléket eredeti rendeltetésétől eltérően más célra is alkalmassá tesz" },
            new() { WordTitle = "paternalizmus", Description = "Az az álláspont vagy gyakorlat, amikor egy személy vagy intézmény korlátozza vagy irányítja egy másik személy döntéseit azért, mert úgy véli, hogy ez az illető javára válik." },
            new() { WordTitle = "szubjektivitás", Description = "Az az állapot, amikor egy észlelés, vélemény vagy döntés személyes tapasztalatokon vagy érzelmeken alapul, és nem objektív tényeken." },
            new() { WordTitle = "ambiszekszuális", Description = "Olyan egyén, aki vonzódik mind a férfiakhoz, mind a nőkhöz." },
            new() { WordTitle = "mellébeszélés", Description = "Bonyolult, általában indoklás nélküli vagy félrevezető beszéd." },
            new() { WordTitle = "ultrakrepidarianizmus", Description = "Az a szokás, hogy valaki olyan témákban mondja el a véleményét, amelyekben nincs kellő szaktudása vagy tapasztalata." },
            new() { WordTitle = "hiperbolikus", Description = "Túlzó, erősen eltúlzott, hiperbóléra utaló." },
            new() { WordTitle = "misantrópia", Description = "Az az állapot, amikor valaki általánosságban utálja az embereket vagy elveszítette a hitét az emberi jóban." },
            new() { WordTitle = "ebullió", Description = "Az az állapot, amikor egy folyadék forráspontja alatt lévő nyomáson gyorsan kezd gőzzé válni vagy forrni." },
            new() { WordTitle = "defenestráció", Description = "Az az aktus, amikor valakit vagy valamit kidobnak egy ablakon. A szó átvitt értelemben is használatos, ami azt jelenti, hogy valakit vagy valamit egy pozícióból vagy státuszból eltávolítanak." }
        };

        await dataProvider.PersistAsync(words);
    }
}