using ETL;
using ETL.Classes;
using System.Text.Json;

//FileManager fmanager = new();
StreamReader sr = new(@"E:\WORK\Radency\ETL\Files\folder_a\data.csv");
string output = sr.ReadToEnd();
Console.WriteLine(output);
CSVConverter csvconverter = new(@"E:\WORK\Radency\ETL\Files\folder_a\data.csv");
IEnumerable<TransactionDTO> dtos = csvconverter.GetAllRecords<TransactionDTO>();
foreach (var dto in dtos)
{
    Console.WriteLine(dto.ConvertToJSON());
}
Console.ReadKey();