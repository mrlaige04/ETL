using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace ETL.Classes
{
    internal class FileManager
    {
        FileSystemWatcher CSVwatcher = new(@"..\..\..\Files\folder_a\", "*.csv");
        FileSystemWatcher TXTwatcher = new(@"..\..\..\Files\folder_a\", "*.txt");
        string pathOutputBase = @"..\..\..\Files\folder_b\";
        string pathOutputToday = "";
        MetaLogWriter metawriter;
        TXTConverter txtconverter = new();
        int outputCounter = 0;
        public FileManager()
        {
            pathOutputToday = pathOutputBase + DateTime.Now.ToString("MM/dd/yyyy") + @"\";


            CSVwatcher.Created += CSVCreated;
            TXTwatcher.Created += TXTCreated;
            CSVwatcher.EnableRaisingEvents = true;
            TXTwatcher.EnableRaisingEvents = true;
        }

        private void CSVCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created CSV: {e.FullPath}";
            Console.WriteLine(value);
        }

        private async void TXTCreated(object sender, FileSystemEventArgs e)
        {
            Output output = await txtconverter.GetOutputFromTXTFileAsync(e.FullPath);
            
            
            if(!Directory.Exists(pathOutputToday))
            {
                Directory.CreateDirectory(pathOutputToday);
                outputCounter = 0;
            }
            
            using (StreamWriter sw = new(pathOutputToday + "output" + outputCounter + ".json"))
            {
                await sw.WriteAsync(JsonSerializer.Serialize(output));
                outputCounter++;
            }

            File.Delete(e.FullPath);
        }       

        
    }
}
