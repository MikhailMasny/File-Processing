﻿using FileProcessing.BL.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileProcessing.BL
{
    /// <summary>
    /// Класс для основных операций с файлами.
    /// </summary>
    public abstract class OperationsWithFiles
    {
        private readonly DataStructure _dataStructure;

        /// <summary>
        /// Блокировка заданного объекта.
        /// </summary>
        static readonly object locker = new object();

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
        /// Проверить наличие указанного пути.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public virtual bool CheckForSpecifiedPath()
        {
            try
            {
                if (!Directory.Exists(_dataStructure.Input_address))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(Constants.EXCEPTION_PROCESS_FAILED, e.ToString());

                return false;
            }
        }

        /// <summary>
        /// Получить список файлов для считывания данных.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public virtual bool GetListOfFiles()
        {
            string[] allFiles;

            try
            {
                allFiles = Directory.GetFiles(_dataStructure.Input_address, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception e)
            {
                Console.WriteLine(Constants.EXCEPTION_PROCESS_FAILED, e.ToString());

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
        public virtual void GetListOfInputData(string item)
        {
            using (var sr = new StreamReader(item, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Trim().ToLower();

                    lock (locker)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            _dataStructure.ListOfInputData.Add(line);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Обработать считанные данные.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public virtual bool ProcessInputData()
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
                    Console.WriteLine(Constants.EXCEPTION_OPERATION_ON_STRING + item);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine(Constants.EXCEPTION_OPERATION_ON_STRING + item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(Constants.EXCEPTION_PROCESS_FAILED, e.ToString());

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Записать результат в файл output.txt
        /// </summary>
        /// <returns>Результат операции.</returns>
        public virtual bool WriteDataToFile()
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

                Console.WriteLine(Constants.DATA_SAVED_TO_FILE);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(Constants.EXCEPTION_PROCESS_FAILED, e.ToString());

                return false;
            }
        }
    }
}
