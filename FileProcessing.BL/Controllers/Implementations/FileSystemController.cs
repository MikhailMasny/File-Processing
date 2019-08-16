using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;

namespace FileProcessing.BL.Controllers.Implementations
{
    /// <summary>
    /// 
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
            _dataStructure = new DataStructure();
            _dataStructure.Input_mode = mode;
            _dataStructure.Input_address = path;

            _operationsWithFiles = new OperationsWithFiles();
        }

        /// <inheritdoc/>
        public void StartProcessing()
        {

        }        
    }
}
