using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;

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
        public OperationStatus StartProcessing()
        {
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

            GetListOfInputData();

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
    }
}
