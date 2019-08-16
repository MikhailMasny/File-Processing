using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// Контроллер для скачивания и обработки всех документов указанных в файле.
    /// </summary>
    public class HttpController : IDataProcessing
    {
        private readonly DataStructure _dataStructure;
        private readonly OperationsWithFiles _operationsWithFiles;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public HttpController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public HttpController(string mode, string http_address)
        {
            _dataStructure = new DataStructure
            {
                Input_mode = mode,
                Input_address = http_address
            };

            //_operationsWithFiles = new OperationsWithFiles(_dataStructure);
        }

        /// <inheritdoc/>
        public void StartProcessing()
        {
            var isCompleted = ProcessingSpecifiedPath();

            if (!isCompleted)
            {
                Console.WriteLine("ERROR!");
            }

            //var isSuccessfullyCompleted = _operationsWithFiles.Start();

            //if (isSuccessfullyCompleted)
            //{
            //    Console.WriteLine("OK!");
            //}
            //else
            //{
            //    Console.WriteLine("NOT OK!");
            //}
        }

        /// <summary>
        /// Обработка указанного http пути.
        /// </summary>
        private bool ProcessingSpecifiedPath()
        {
            try
            {
                var now = DateTime.Now;
                var date = now.ToString("yyyyMMdd");
                var time = now.ToString("hhmmss");

                var tempFolder = CreateFolder(date + time);
                var path = Directory.GetCurrentDirectory();




                DownloadFiles(_dataStructure.Input_address, null, Convert.ToInt32(date));
                var addressList = HttpFileContent($"{date}.txt");

                int i = 0;
                StringBuilder sb = new StringBuilder(path + @"\" + tempFolder + @"\" + "file");
                foreach (var item in addressList)
                {
                    //HttpDownload(item, sb.ToString(), i);
                    i++;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Создание временной папки для загрузки материала с веб-узла.
        /// </summary>
        /// <returns>Возвращает название созданной папки.</returns>
        public string CreateFolder(string dt)
        {
            var tempFolder = "temp_" + dt;

            var dirInfo = new DirectoryInfo(tempFolder);
            dirInfo.Create();

            return tempFolder;
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

            //if (allFiles.Count() == 0)
            //{
            //    return false;
            //}

            foreach (var file in allFiles)
            {
                _dataStructure.ListOfFiles.Add(file);
            }

            return true;
        }

        /// <summary>
        /// Загрузка материала с веб-узла.
        /// </summary>
        /// <param name="url">Веб-узел.</param>
        /// <param name="tempFolder">Название папки для скачивания.</param>
        /// <param name="i">Счетчик.</param>
        private void DownloadFiles(string url, string tempFolder, int i)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, $"{tempFolder}{i}.txt");
                Console.WriteLine($"File successfully {url} downloaded!");
            }
        }

        /// <summary>
        /// Чтение загруженного файла для получения коллекции списка адресов с файлами.
        /// </summary>
        /// <param name="filename">Название файла для чтения.</param>
        /// <returns>Возвращения коллекции со списком файлов для загрузки.</returns>
        public ICollection<string> HttpFileContent(string filename)
        {
            ICollection<string> adrressInFile = new List<string>();
            using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        adrressInFile.Add(line);
                    }
                }
            }

            return adrressInFile;
        }
    }
}
