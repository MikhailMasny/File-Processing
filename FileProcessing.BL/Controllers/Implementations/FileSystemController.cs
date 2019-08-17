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
        public bool StartProcessing()
        {
            var isPathExist = CheckForSpecifiedPath();
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

            if (isPathExist && isFilesExists && isProcessed && isWrited)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        
    }
}
