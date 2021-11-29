using Microsoft.EntityFrameworkCore;
using TemporalTablesSample;

using var axonDbContext = new AxonDbContext();

while (true)
{
    Console.WriteLine(
                "1: To Show Current Tax Rate" + Environment.NewLine +
                "2: To Increase 5 Percent To Tax Rate" + Environment.NewLine +
                "3: To Decrease 5 Percent Form Tax Rate" + Environment.NewLine +
                "4: To Show All Changes On Tax Rate" + Environment.NewLine +
                "Q: Quit"
                );


    var command = Console.ReadKey().KeyChar;
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    switch (command)
    {
        case '1':
            ShowCurrentTaxRate();
            break;
        case '2':
            IncreaseTaxRate();
            break;
        case '3':
            DecreaseTaxRate();
            break;
        case '4':
            ShowAllHistories();
            break;
        case 'q':
        case 'Q':
            return;
        default:
            break;
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine();
    Console.ResetColor();
}
void ShowCurrentTaxRate()
{
    var taxRateSetting = axonDbContext.Settings.Single(p => p.Key == Setting.TaxRateSettingKey);
    Console.WriteLine($"Current Tax Rate = {taxRateSetting.Value}");
}

void IncreaseTaxRate()
{
    var taxRateSetting = axonDbContext.Settings.Single(p => p.Key == Setting.TaxRateSettingKey);
    taxRateSetting.Value += 5;
    axonDbContext.SaveChanges();
    Console.WriteLine($"Tax Rate Increased to {taxRateSetting.Value}");
}

void DecreaseTaxRate()
{
    var taxRateSetting = axonDbContext.Settings.Single(p => p.Key == Setting.TaxRateSettingKey);
    taxRateSetting.Value -= 5;
    axonDbContext.SaveChanges();
    Console.WriteLine($"Tax Rate Decreased to {taxRateSetting.Value}");
}

void ShowAllHistories()
{
    var taxSettingHistories = axonDbContext.Settings
    .TemporalAll()
    .OrderBy(temporal => EF.Property<DateTime>(temporal, "ValidFrom"))
    .Select(temporal => new
    {
        Rate = temporal.Value,
        ValidFrom = EF.Property<DateTime>(temporal, "ValidFrom"),
        ValidTo = EF.Property<DateTime>(temporal, "ValidTo")
    })
    .ToList();

    foreach (var taxSettingHistory in taxSettingHistories)
    {
        Console.WriteLine($"From: {taxSettingHistory.ValidFrom} To: {taxSettingHistory.ValidTo} = {taxSettingHistory.Rate}");
    }
}
