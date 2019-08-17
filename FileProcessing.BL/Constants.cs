namespace FileProcessing.BL
{
    /// <summary>
    /// Основные константы проекта.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Выбор параметра - файловая система.
        /// </summary>
        public const string filesystemValue = "filesystem";

        /// <summary>
        /// Выбор параметра - http.
        /// </summary>
        public const string httpValue = "http";

        /// <summary>
        /// Выбор параметра - помощь по параметрам.
        /// </summary>
        public const string helpValue = "-help";

        /// <summary>
        /// Название выходного файла.
        /// </summary>
        public const string output = "output.txt";

        /// <summary>
        /// Максимальное количество аргументов.
        /// </summary>
        public const int maxArgs = 2;

        /// <summary>
        /// Успешное сохранение в файл.
        /// </summary>
        public const string DATA_SAVED_TO_FILE = "Data successfully saved to the file output.txt!";

        /// <summary>
        /// Невозможно выполнить операцию со строкой.
        /// </summary>
        public const string EXCEPTION_OPERATION_ON_STRING = "Unable to perform operation on string: ";

        /// <summary>
        /// Критическая ошибка работы процесса.
        /// </summary>
        public const string EXCEPTION_PROCESS_FAILED = "The process failed: {0}";
    }
}
