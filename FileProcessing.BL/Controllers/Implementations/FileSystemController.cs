using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// Контроллер для обработки всех файлов содержащихся по указанной директории.
    /// </summary>
    public class FileSystemController : IDataProcessing
    {
        private readonly DataStructure _dataStructure;
        private readonly OperationsWithFiles _operationsWithFiles;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public FileSystemController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public FileSystemController(string mode, string path)
        {
            _dataStructure = new DataStructure
            {
                Input_mode = mode,
                Input_address = path
            };

            _operationsWithFiles = new OperationsWithFiles(_dataStructure);
        }

        /// <inheritdoc/>
        public void StartProcessing()
        {
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
    }
}
