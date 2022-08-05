using ETL;
using ETL.Classes;
//FolderReader folderReader = new();
//IEnumerable<string> files = folderReader.GetFilesPath();
//FileReader csvreader = new();
//foreach (var filepath in files)
//{
//    Console.WriteLine(csvreader.ReadFile(filepath));
//    Console.WriteLine(new string('-',50));
//}
CSVReader csvreader = new($@"E:\WORK\Radency\ETL\Files\folder_a\payment.csv");


IEnumerable<Payer> records = csvreader.GetAllRecords<Payer>();

foreach (var record in records)
{
    try
    {
        Console.WriteLine(record.ConvertToJson());
    }
    catch (MissingFieldException ex)
    {
        Console.WriteLine(ex.Message);
    }
}