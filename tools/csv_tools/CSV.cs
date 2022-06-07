using System.Text.RegularExpressions;

namespace csv_tools{
    public class CSV{

        private string _fileLocation = "";
        private string _outLocation = "";
        private int _readLines = 0;
        private int _writeLines = 0;

        public CSV(string fileLocation)
        {
            _fileLocation = fileLocation;
            _outLocation = fileLocation.Replace(".csv", "_converted.csv");
        }

        public void ProceedWithOption(ToolOptions option)
        {
            Console.WriteLine("Please wait...");
            ProcessingFile(option);
        }

        private void ProcessingFile(ToolOptions option)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(_fileLocation))
            {
                string? header = sr.ReadLine();
                if (header == null || header == String.Empty)
                    return;
                
                if(option == ToolOptions.AddTripDuration)
                {
                    AddHeaderWithExtraColumn(header);
                }
                else
                {
                    AddHeader(header);
                }
                
                string? line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if(line == null)
                        continue;

                    _readLines++;

                    switch(option)
                    {
                        case ToolOptions.ConvertDates:
                            AddLineToFile(String.Join(',', GetConvertedDates(line)));
                            break;
                        case ToolOptions.AddTripDuration:
                            AddLineToFile(String.Join(',', GetAddedColumn(line)));
                            break;
                        case ToolOptions.ConvertDecimals:
                            AddLineToFile(String.Join(',', GetConvertedNumbers(line)));
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine("All Done! Read " + _readLines + " lines. Wrote " + _writeLines + " lines.");                
            }
        }

        public string[] GetConvertedDates(string line)
        {
            line = line.Replace("\"", "");
            string[] row = line.Split(',');
            DateTime ukStartDT = DateTime.Parse(row[1].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            DateTime ukEndDT = DateTime.Parse(row[2].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            row[1] = ukStartDT.ToString();
            row[2] = ukEndDT.ToString();

            return row;
        }

        public string[] GetAddedColumn(string line)
        {
            line = line.Replace("\"", "");
            string[] row = line.Split(',');
            DateTime ukStartDT = DateTime.Parse(row[2].ToString());
            DateTime ukEndDT = DateTime.Parse(row[3].ToString());
            row[2] = ukStartDT.ToString();
            row[3] = ukEndDT.ToString();
            TimeSpan ts = ukEndDT - ukStartDT;
            Array.Resize(ref row, row.Length + 1);
            row[row.Length - 1] = ts.TotalSeconds.ToString();

            return row;
        }

        public string[] GetConvertedNumbers(string line)
        {
            var number = from Match match in Regex.Matches(line, "\"([^\"]*)\"")
                        select match.ToString();

            if (number != null && number.Count() > 0)
            {
                string? original = number.FirstOrDefault();
                string? replacement = original.Replace(",", "");
                line = line.Replace(original, replacement);
            }

            string[] row = line.Split(',');

            return row;
        }

        public void AddHeader(string header)
        {
                _readLines++;
                
                AddLineToFile(header);
        }

        public void AddHeaderWithExtraColumn(string header)
        {
            _readLines++;
            string[] headerArr = header.Split(',');
            Array.Resize(ref headerArr, headerArr.Length + 1);
            headerArr[headerArr.Length - 1] = "tripduration";
            AddLineToFile(String.Join(',', headerArr));
        }

        void AddLineToFile(string line)
        {
            using (StreamWriter sw = new StreamWriter(_outLocation, true))
            {
                sw.WriteLine(line);
                _writeLines++;
            }
        }
        
    }
}