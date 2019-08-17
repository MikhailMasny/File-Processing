namespace FileProcessing.BL.Controllers.Interfaces
{
    public interface IDataProcessing
    {
        /// <summary>
        /// Начать обработку файлов.
        /// </summary>
        OperationStatus StartProcessing();
    }
}
