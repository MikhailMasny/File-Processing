using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: добавить параллельность.

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemController : IDataProcessing
    {
        private readonly DataStructure _dataStructure;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public FileSystemController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public FileSystemController(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        /// <inheritdoc/>
        public bool CheckForSpecifiedPath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return false;
                }

                _dataStructure.Input_address = path;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
        }

        /// <inheritdoc/>
        public bool GetListOfFiles()
        {
            string[] allFiles;

            try
            {
                allFiles = Directory.GetFiles(_dataStructure.Input_address, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }

            if (allFiles.Count() == 0)
            {
                return false;
            }

            foreach (var file in allFiles)
            {
                _dataStructure.ListOfFiles.Add(file);
            }

            return true;
        }

        /// <inheritdoc/>
        public void GetListOfInputData()
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
        }

        /// <inheritdoc/>
        public bool ProcessInputData()
        {
            foreach (var item in _dataStructure.ListOfInputData)
            {
                try
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
                catch(FormatException)
                {
                    Console.WriteLine($"Невозможно выполнить операцию со строкой: {item}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.ToString());

                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public bool WriteDataToFile()
        {
            try
            {
                using (var sw = new StreamWriter(Constants.output, false, Encoding.UTF8))
                {
                    foreach (var keyValue in _dataStructure.ListOfProcessedData)
                    {
                        sw.WriteLine(keyValue.Key + "," + keyValue.Value);
                    }
                }

                Console.WriteLine("Data successfully saved to the file output.txt!");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
        }

        /// <inheritdoc/>
        public Task ProcessingSpecifiedPath()
        {
            throw new NotImplementedException();
        }

        
    }
}
