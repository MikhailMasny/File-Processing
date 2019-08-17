﻿using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// Контроллер для скачивания и обработки всех документов указанных в файле.
    /// </summary>
    public class HttpController : OperationsWithFiles, IDataProcessing
    {
        private readonly DataStructure _ds;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public HttpController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public HttpController(DataStructure dataStructure) : base(dataStructure)
        {
            _ds = dataStructure;
        }

        /// <inheritdoc/>
        public void StartProcessing()
        {
            var isDownloaded = ProcessingSpecifiedHttpPath();
            if (!isDownloaded)
            {
                Console.WriteLine("ERROR!!!");
            }

            var isPathExist = CheckForSpecifiedPath();
            if (!isPathExist)
            {
                Console.WriteLine("ERROR!!!");
            }

            var isFilesExists = GetListOfFiles();
            if (!isFilesExists)
            {
                Console.WriteLine("ERROR!!!");
            }

            GetListOfInputData();

            var isProcessed = ProcessInputData();
            if (!isProcessed)
            {
                Console.WriteLine("ERROR!!!");
            }

            var isWrited = WriteDataToFile();
            if (!isWrited)
            {
                Console.WriteLine("ERROR!!!");
            }

            if (isPathExist && isFilesExists && isProcessed && isWrited)
            {
                Console.WriteLine("OK!");
            }
            else
            {
                Console.WriteLine("NOT OK!");
            }
        }

        /// <summary>
        /// Обработка указанного http пути.
        /// </summary>
        private bool ProcessingSpecifiedHttpPath()
        {
            try
            {
                ReadFile();

                var now = DateTime.Now;
                var date = now.ToString("yyyyMMdd");
                var time = now.ToString("hhmmss");

                var tempFolder = CreateFolder(date + time);
                var currentDirectory = Directory.GetCurrentDirectory();
                var path = currentDirectory + @"\" + tempFolder;


                foreach (var item in _ds.ListOfFiles)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMdd_hhmmss");
                    DownloadFiles(item, path, fileName);
                }

                _ds.Input_address = path;
                _ds.ListOfFiles.Clear();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());

                return false;
            }
        }

        /// <summary>
        /// Получить данные из файла.
        /// </summary>
        private void ReadFile()
        {
            using (var sr = new StreamReader(_ds.Input_address, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Trim().ToLower();
                    _ds.ListOfFiles.Add(line);
                }
            }
        }

        /// <summary>
        /// Создать новую папку для загрузки материала с веб-узла.
        /// </summary>
        /// <returns>Возвращает название созданной папки.</returns>
        private string CreateFolder(string dt)
        {
            var tempFolder = "temp_" + dt;

            var dirInfo = new DirectoryInfo(tempFolder);
            dirInfo.Create();

            return tempFolder;
        }

        /// <summary>
        /// Загрузка материала с веб-узла.
        /// </summary>
        /// <param name="url">веб-узел.</param>
        /// <param name="tempFolder">путь к папке.</param>
        /// <param name="fileName">название файла.</param>
        private void DownloadFiles(string url, string tempFolder, string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, $@"{tempFolder}\{fileName}.txt");
                Console.WriteLine($"File successfully {url} downloaded!");
            }
        }
    }
}
