using System.Threading.Tasks;

namespace FileProcessing.BL.Controllers.Interfaces
{
    interface IDataProcessing
    {
        /// <summary>
        /// Обработка указанного пути.
        /// </summary>
        Task ProcessingSpecifiedPath();

        /// <summary>
        /// Получить список файлов для считывания данных.
        /// </summary>
        Task GetListOfFiles();

        /// <summary>
        /// Получить данные со всех выбранных файлов.
        /// </summary>
        Task GetListOfInputData();

        /// <summary>
        /// Обработать считанные данные.
        /// </summary>
        Task ProcessInputData();

        /// <summary>
        /// Записать результат в файл output.txt
        /// </summary>
        Task WriteDataToFile();
    }
}
