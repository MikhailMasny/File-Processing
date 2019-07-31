﻿using System.Collections.Generic;

namespace FileProcessing.BL.Models
{
    /// <summary>
    /// Модель данных.
    /// </summary>
    public class DataStructure
    {
        /// <summary>
        /// Режим получения данных.
        /// </summary>
        public string Input_mode { get; set; }

        /// <summary>
        /// Адрес получения данных.
        /// </summary>
        public string Input_address { get; set; }

        /// <summary>
        /// Список файлов для чтения.
        /// </summary>
        public ICollection<string> ListOfFiles;

        /// <summary>
        /// Список считанных данных из файлов.
        /// </summary>
        public ICollection<string> ListOfInputData;
    }
}
