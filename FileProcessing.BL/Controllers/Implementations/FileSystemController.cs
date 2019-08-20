using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// Контроллер для обработки всех файлов содержащихся по указанной директории.
    /// </summary>
    public class FileSystemController : OperationsWithFiles, IDataProcessing
    {
        private readonly DataStructure _ds;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public FileSystemController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">экзепляр класса.</param>
        public FileSystemController(DataStructure dataStructure) : base(dataStructure)
        {
            _ds = dataStructure;
        }

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

            return OperationStatus.OPERATION_SUCCEEDED;
        }        
    }
}
