using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessing.BL.Controllers.Implementations
{
    public class FileSystemController : IDataProcessing
    {
        private readonly DataStructure _dataStructure;

        public FileSystemController()
        {

        }

        public FileSystemController(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public Task GetListOfFiles()
        {
            var allFiles = Directory.GetFiles(_dataStructure.Input_address, "*.*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                _dataStructure.ListOfFiles.Add(file);
            }

            return null;
        }

        public Task GetListOfInputData()
        {
            foreach (var item in _dataStructure.ListOfFiles)
            {
                using (var sr = new StreamReader(item, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Trim().ToLower();
                        _dataStructure.ListOfInputData.Add(line);
                    }                   
                }
            }

            return null;
        }

        public Task ProcessingSpecifiedPath()
        {
            throw new NotImplementedException();
        }

        public Task ProcessInputData()
        {
            foreach (var item in _dataStructure.ListOfInputData)
            {
                var words = item.Split(new char[] { ',' });
                words[0] = words[0].Trim().ToLower();

                if (_dataStructure.ListOfProcessedData.ContainsKey(words[0]))
                {
                    _dataStructure.ListOfProcessedData[words[0]] += Convert.ToInt32(words[1]);
                }
                else
                {
                    _dataStructure.ListOfProcessedData.Add(words[0], Convert.ToInt32(words[1]));
                }
            }

            return null;
        }

        public Task WriteDataToFile()
        {
            using (var sw = new StreamWriter(Constants.output, false, Encoding.UTF8))
            {
                foreach (var keyValue in _dataStructure.ListOfProcessedData)
                {
                    sw.WriteLine(keyValue.Key + "," + keyValue.Value);
                }
            }

            Console.WriteLine("Data successfully saved to the file output.txt!");

            return null;
        }
    }
}
