using System;
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
        string pathOutputBase = @"..\..\..\Files\folder_b\";
        string pathOutputToday = "";
        int outputCounter = 0;
        
        FolderReader folderReader = new();
        MetaLogWorker metalogworker = new();
        readonly FileConverter fileConverter = new();
        readonly System.Timers.Timer timer = new(800);



        public FileManager()
        {           
            timer.Elapsed += CheckFiles;   
        }

        private async void CheckFiles(object? sender, ElapsedEventArgs e)
        {
            List<string> paths = folderReader.GetFilesPath(@"..\..\..\Files\folder_a\").ToList();
            if(paths!=null && paths.Count>0)
            {
                await ProcessFileAsync(paths);
            }
            if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 59 && DateTime.Now.Second>=59)
            {
                metalogworker.SetPath(pathOutputBase + DateTime.Now.ToString(@"dd/MM/yyyy") + @$"\meta.log");
                metalogworker.Write();
                metalogworker.Dispose();
                Console.WriteLine("Meta log created");
            }
            
        }

        private async Task WriteToJsonFromOutputAsync(Output output)
        {
            pathOutputToday = pathOutputBase + DateTime.Now.ToString("dd/MM/yyyy") + @"\";
            if (!Directory.Exists(pathOutputToday))
            {
                Directory.CreateDirectory(pathOutputToday);
                outputCounter = 0;
            }

            using (StreamWriter sw = new(pathOutputToday + "output" + ++outputCounter + ".json"))
            {
                await sw.WriteAsync(JsonSerializer.Serialize(output));
            }
            
        }
        
        #region ManageFileManager
        public void Start()
        {
            timer.Enabled = true;
        }
        public void Stop()
        {
            timer.Enabled = false;
        }
        #endregion            
        
        #region ProcessingFiles
        
        private async Task ProcessFileAsync(IEnumerable<string> paths)
        {
            await Task.Run(() =>
            {
                

                
                List<string> csvPaths = paths.Where(x => x.Contains(".csv")).ToList();
                List<string> txtPaths = paths.Where(x => x.Contains(".txt")).ToList();

                csvPaths?.ForEach(async x =>
                {
                    Output output = await fileConverter.GetOutputFromCSVFileAsync(x, metalogworker);                   
                    await WriteToJsonFromOutputAsync(output);
                });

                txtPaths?.ForEach(async x =>
                {
                    Output output = await fileConverter.GetOutputFromTXTFileAsync(x, metalogworker);
                    await WriteToJsonFromOutputAsync(output);
                });

                
            });
        }
        #endregion
    }
}
