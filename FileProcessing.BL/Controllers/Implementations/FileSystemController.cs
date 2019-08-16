using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System;

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// Контроллер для обработки всех файлов содержащихся по указанной директории.
    /// </summary>
    public class FileSystemController : OperationsWithFiles, IDataProcessing
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public FileSystemController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public FileSystemController(DataStructure dataStructure) : base(dataStructure) { }

        /// <inheritdoc/>
        public void StartProcessing()
        {
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
    }
}
