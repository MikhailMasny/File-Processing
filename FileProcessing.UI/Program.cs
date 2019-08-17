using System;
using FileProcessing.BL;
using FileProcessing.BL.Controllers.Implementations;
using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;

// TODO: Вынести константы.
// TODO: Проработать исключения.

namespace FileProcessing.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataProcessing dataProcessing;

            try
            {
                if (args == null)
                {
                    throw new IndexOutOfRangeException("The index was outside the bounds of the array! Info: -help.");
                }

                if (args.Length < Constants.maxArgs)
                {
                    throw new IndexOutOfRangeException("The index was outside the bounds of the array! Info: -help.");
                }

                if (args.Length == Constants.maxArgs)
                {
                    var dataStructure = new DataStructure
                    {
                        Input_mode = args[0],
                        Input_address = args[1]
                    };

                    switch (args[0])
                    {
                        case Constants.filesystemValue:
                            {
                                dataProcessing = new FileSystemController(dataStructure);
                                dataProcessing.StartProcessing();
                            }
                            break;

                        case Constants.httpValue:
                            {
                                dataProcessing = new HttpController(dataStructure);
                                dataProcessing.StartProcessing();
                            }
                            break;

                        case Constants.helpValue:
                            {
                                HelpInformation();
                            }
                            break;

                        default:
                            {
                                throw new ArgumentException($"The first parameter should be '{Constants.filesystemValue}' or '{Constants.httpValue}'. Info: -help.");
                            }
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Получить справочную информацию.
        /// </summary>
        private static void HelpInformation()
        {
            Console.WriteLine("Possible arguments for 'mode': filesystem и http.");
            Console.WriteLine("Possible arguments for 'address':");
            Console.WriteLine("\tAt filesystem, address - path to the directory.");
            Console.WriteLine("\tAt http, address - the path to the file.");
        }
    }
}
