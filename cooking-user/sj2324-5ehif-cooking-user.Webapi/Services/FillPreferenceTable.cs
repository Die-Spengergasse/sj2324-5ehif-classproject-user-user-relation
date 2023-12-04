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
            HasHeaderRecord = false
        };

        using var reader = new StreamReader("./Services/preference.csv");
        using var csv = new CsvReader(reader, config);
        var csvRecords = csv.GetRecords<PreferenceRecord>().ToList();

        var dbRecords = _context.Preferences.ToList();

        // Delete preferences from the database that do not exist in the CSV file
        foreach (var dbRecord in dbRecords.Where(dbRecord => csvRecords.All(r => r.Name != dbRecord.Name)))
        {
            _context.Preferences.Remove(dbRecord);

            // Remove the preference from all users
            foreach (var user in _context.Users) user.RemovePreference(dbRecord);
        }

        // Add preferences from the CSV file that do not exist in the database
        foreach (var preference in from csvRecord in csvRecords
                 where dbRecords.All(r => r.Name != csvRecord.Name)
                 select new Preference(csvRecord.Name)) _context.Preferences.Add(preference);

        _context.SaveChanges();
    }
}