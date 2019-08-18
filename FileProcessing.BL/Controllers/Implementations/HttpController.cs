using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public OperationStatus StartProcessing()
        {
            var isDownloaded = ProcessingSpecifiedHttpPath();
            if (!isDownloaded)
            {
                return OperationStatus.DOWNLOAD_FILES;
            }

            var isPathExist = CheckForSpecifiedPath();
            if (!isPathExist)
            {
                return OperationStatus.PATH;
            }

            var isFilesExists = GetListOfFiles();
            if (!isFilesExists)
            {
                return OperationStatus.LIST_OF_FILES;
            }

            var items = _ds.ListOfFiles.ToArray();

            Parallel.For(0, items.Length, new ParallelOptions
            {
                MaxDegreeOfParallelism = Constants.count_parallel
            }, (i, state) =>
            {
                GetListOfInputData(items[i]);
            });

            var isProcessed = ProcessInputData();
            if (!isProcessed)
            {
                return OperationStatus.PROCESSING_INPUT_DATA;
            }

            var isWrited = WriteDataToFile();
            if (!isWrited)
            {
                return OperationStatus.WRITE_DATA;
            }

            if (isPathExist && isFilesExists && isProcessed && isWrited)
            {
                return OperationStatus.OPERATION_SUCCEEDED;
            }
            else
            {
                return OperationStatus.OPERATION_FAILED;
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
