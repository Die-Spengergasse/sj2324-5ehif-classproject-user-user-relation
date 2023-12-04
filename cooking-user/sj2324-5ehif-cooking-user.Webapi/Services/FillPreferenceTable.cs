using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Webapi.Services;

public class PreferenceRecord
{
    public string Name { get; set; }
}

public class FillPreferenceTable
{
    private readonly UserContext _context;

    public FillPreferenceTable(UserContext context)
    {
        _context = context;
    }

    public void ReadCSV()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };

        using var reader = new StreamReader("./Services/preference.csv");
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<PreferenceRecord>();
        foreach (var record in records)
        {
            if (_context.Preferences.Any(p => p.Name == record.Name)) continue;
            var preference = new Preference(record.Name);
            _context.Preferences.Add(preference);
        }
        _context.SaveChanges();
    }
}