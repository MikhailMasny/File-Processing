namespace FileProcessing.BL
{
    /// <summary>
    /// Перечисление возможных ошибок.
    /// </summary>
    public enum OperationStatus
    {
        OPERATION_SUCCEEDED = 0x00000,

        PATH = 0x00001,
        LIST_OF_FILES = 0x00002,
        LIST_OF_INPUT_DATA = 0x00003,
        PROCESSING_INPUT_DATA = 0x00004,
        WRITE_DATA = 0x00005,
        DOWNLOAD_FILES = 0x00006,
        OPERATION_FAILED = 0x00007 
    }
}
