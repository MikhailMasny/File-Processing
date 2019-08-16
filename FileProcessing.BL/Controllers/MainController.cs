using FileProcessing.BL.Models;
using System.Collections.Generic;

namespace FileProcessing.BL.Controllers
{
    /// <summary>
    /// Основной контроллер приложения.
    /// </summary>
    public class MainController
    {
        private readonly DataStructure _dataStructure;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public MainController() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="dataStructure">основные данные.</param>
        /// <param name="args">аргументы.</param>
        public MainController(DataStructure dataStructure, string[] args)
        {
            _dataStructure = dataStructure;
            _dataStructure.Input_mode = args[0];
            _dataStructure.Input_address = args[1];
            //_dataStructure.ListInputData = SeparationArgs();
        }

        /// <summary>
        /// Разделить аргументы.
        /// </summary>
        /// <returns>Список элементов.</returns>
        private List<string> SeparationArgs()
        {
            var result = new List<string>();

            switch(_dataStructure.Input_mode)
            {
                case Constants.filesystemValue: { } break;
                case Constants.httpValue: { } break;
                default: { } break;
            }

            return result;
        }
    }
}
