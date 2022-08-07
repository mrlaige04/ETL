﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Timers;

namespace ETL.Classes
{
    internal class FileManager
    {

        FolderReader folderReader = new();
        FileSystemWatcher CSVwatcher = new(@"..\..\..\Files\folder_a\", "*.csv");
        FileSystemWatcher TXTwatcher = new(@"..\..\..\Files\folder_a\", "*.txt");
        string pathOutputBase = @"..\..\..\Files\folder_b\";
        string pathOutputToday = "";
        MetaLogWriter metawriter;
        LineConverter lineConverter = new();
        CSVConverter csvconverter;
        int outputCounter = 0;
        public FileManager()
        {
            pathOutputToday = pathOutputBase + DateTime.Now.ToString("MM/dd/yyyy") + @"\";            

            System.Timers.Timer timer = new(1500);
            timer.Elapsed += ProcessFiles;
            timer.Enabled = true;            
        }
        
        private async void ProcessFiles(object? sender, ElapsedEventArgs e)
        {
            await ProcessDefaultFilesAsync();
        }     

        private async Task WriteToJsonFromOutputAsync(Output output)
        {
            if (!Directory.Exists(pathOutputToday))
            {
                Directory.CreateDirectory(pathOutputToday);
                outputCounter = 0;
            }

            using (StreamWriter sw = new(pathOutputToday + "output" + outputCounter + ".json"))
            {
                await sw.WriteAsync(JsonSerializer.Serialize(output));
                outputCounter++;
            }  
        }
        
        private async Task ProcessDefaultFilesAsync()
        {
            await Task.Run(() =>
            {
                List<string> defaultFilesPathes = folderReader.GetFilesPath().ToList();
                List<string> csvPaths = defaultFilesPathes.Where(x => x.Contains(".csv")).ToList();
                List<string> txtPaths = defaultFilesPathes.Where(x => x.Contains(".txt")).ToList();

                csvPaths.ForEach(async x =>
                {
                    Output output = await lineConverter.GetOutputFromCSVFileAsync(x);
                    await WriteToJsonFromOutputAsync(output);
                    File.Delete(x);
                });

                txtPaths.ForEach(async x =>
                {
                    Output output = await lineConverter.GetOutputFromTXTFileAsync(x);
                    await WriteToJsonFromOutputAsync(output);
                    File.Delete(x);
                });
            });
        }
    }
}
