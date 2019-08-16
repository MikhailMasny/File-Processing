using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;

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

            _operationsWithFiles = new OperationsWithFiles(_dataStructure);
        }

        /// <inheritdoc/>
        public void StartProcessing()
        {
            var isCompleted = ProcessingSpecifiedPath();

            if (!isCompleted)
            {
                Console.WriteLine("ERROR!");
            }

            var isSuccessfullyCompleted = _operationsWithFiles.Start();

            if (isSuccessfullyCompleted)
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
        private bool ProcessingSpecifiedPath()
        {
            return false;
        }   
    }
}
