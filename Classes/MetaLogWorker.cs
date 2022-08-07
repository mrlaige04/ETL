namespace ETL.Classes
{
    internal class MetaLogWorker : IDisposable
    {
        string path = "";
        public int parsedFiles = 0;
        public int parsedLines = 0;
        public int foundErrors = 0;
 
        public void Write()
        {
            string message = "parsed files: " + parsedFiles + "\n" + "parsed lines: " + parsedLines + "\n" + "foundErrors: " + foundErrors + "\n";
            using (var sw = new StreamWriter(path, false))
            {
                sw.WriteLine(message);
            }
        }        

        public void SetPath(string path)
        {
            this.path = path;
        }
        
        public void Dispose()
        {
            parsedFiles = 0;
            parsedLines = 0;
            foundErrors = 0;
        }     
    }
}
