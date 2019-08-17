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

        /// <summary>
        /// Ошибка выхода за пределы массива.
        /// </summary>
        public const string EXCEPTION_INDEX_OUT_OF_RANGE = "The index was outside the bounds of the array! Info: -help.";

        /// <summary>
        /// Ошибка аргумента.
        /// </summary>
        public const string EXCEPTION_ARGUMENT = "The first parameter should be 'filesystem' or 'http'. Info: -help.";

        /// <summary>
        /// Ошибка правильности пути.
        /// </summary>
        public const string ERROR_PATH = "Error correct path.";

        /// <summary>
        /// Ошибка получения списка файлов.
        /// </summary>
        public const string ERROR_LIST_OF_FILES = "Error getting file list.";

        /// <summary>
        /// Ошибка получения списка данных из файлов.
        /// </summary>
        public const string ERROR_LIST_OF_INPUT_DATA = "Error getting a list of data from files.";

        /// <summary>
        /// Ошибка обработки данных.
        /// </summary>
        public const string ERROR_PROCESSING_INPUT_DATA = "Error processing data.";

        /// <summary>
        /// Ошибка записи файла.
        /// </summary>
        public const string ERROR_WRITE_DATA = "Error writing file.";

        /// <summary>
        /// Ошибка скачивания файлов по HTTP.
        /// </summary>
        public const string ERROR_DOWNLOAD_FILES = "Error downloading files over HTTP.";
    }
}
