using System.Threading.Tasks;

namespace FileProcessing.BL.Controllers.Interfaces
{
    public interface IDataProcessing
    {
        /// <summary>
        /// Обработка указанного пути.
        /// </summary>
        Task ProcessingSpecifiedPath();

        /// <summary>
        /// Получить список файлов для считывания данных.
        /// </summary>
        /// <returns>Результат операции.</returns>
        bool GetListOfFiles();

        /// <summary>
        /// Получить данные со всех выбранных файлов.
        /// </summary>
        void GetListOfInputData();

        /// <summary>
        /// Обработать считанные данные.
        /// </summary>
        bool ProcessInputData();

        /// <summary>
        /// Записать результат в файл output.txt
        /// </summary>
        /// <returns>Результат операции.</returns>
        bool WriteDataToFile();

        /// <summary>
        /// Проверить наличие указанного пути.
        /// </summary>
        /// <returns>Результат операции.</returns>
        bool CheckForSpecifiedPath(string path);
    }
}
