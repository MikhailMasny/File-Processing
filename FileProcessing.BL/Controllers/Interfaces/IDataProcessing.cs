using System.Threading.Tasks;

namespace FileProcessing.BL.Controllers.Interfaces
{
    public interface IDataProcessing
    {
        /// <summary>
        /// Обработка указанного пути.
        /// </summary>
        //Task ProcessingSpecifiedPath();

        /// <summary>
        /// Начать обработку файлов.
        /// </summary>
        void StartProcessing();
    }
}
