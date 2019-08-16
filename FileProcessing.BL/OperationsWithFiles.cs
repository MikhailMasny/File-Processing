using FileProcessing.BL.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

// TODO: добавить параллельность.
// TODO: сделать данный класс абстрактным.

namespace FileProcessing.BL
{
    /// <summary>
    /// Класс для основных операций с файлами.
    /// </summary>
    public class OperationsWithFiles
    {
        private readonly DataStructure _dataStructure;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public OperationsWithFiles() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public OperationsWithFiles(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        /// <summary>
        /// Запуск обработки.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            // TODO: Сделать в виде возвращаемого кода.

            var isPathExist = CheckForSpecifiedPath(_dataStructure.Input_address);
            if (!isPathExist)
            {
                return false;
            }

            var isFilesExists = GetListOfFiles();
            if (!isFilesExists)
            {
                return false;
            }

            GetListOfInputData();

            var isProcessed = ProcessInputData();
            if (!isProcessed)
            {
                return false;
            }

            var isWrited = WriteDataToFile();
            if (!isWrited)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверить наличие указанного пути.
        /// </summary>
        /// <returns>Результат операции.</returns>
        private bool CheckForSpecifiedPath(string path)
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

        /// <summary>
        /// Получить список файлов для считывания данных.
        /// </summary>
        /// <returns>Результат операции.</returns>
        private bool GetListOfFiles()
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

        /// <summary>
        /// Получить данные со всех выбранных файлов.
        /// </summary>
        private void GetListOfInputData()
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

        /// <summary>
        /// Обработать считанные данные.
        /// </summary>
        private bool ProcessInputData()
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
                catch (FormatException)
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

        /// <summary>
        /// Записать результат в файл output.txt
        /// </summary>
        /// <returns>Результат операции.</returns>
        private bool WriteDataToFile()
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
    }
}
