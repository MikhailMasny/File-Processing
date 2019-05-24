using System;
using System.Collections.Generic;

namespace FileProcessing.BL.Models
{
    /// <summary>
    /// Структура данных.
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
        /// Список входных данных.
        /// </summary>
        public List<string> ListInputData = new List<string>();

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public DataStructure() { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="input_mode">режим.</param>
        /// <param name="input_address">адрес.</param>
        public DataStructure(string input_mode, string input_address)
        {
            if (string.IsNullOrWhiteSpace(input_mode) || string.IsNullOrWhiteSpace(input_address))
            {
                throw new ArgumentNullException("Accepted arguments cannot be empty or null.");
            }

            Input_mode = input_mode;
            Input_address = input_address;
        }
    }
}
