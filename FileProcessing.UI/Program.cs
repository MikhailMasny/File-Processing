using System;
using FileProcessing.BL;
using FileProcessing.BL.Controllers.Implementations;
using FileProcessing.BL.Controllers.Interfaces;
using FileProcessing.BL.Models;

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
                    throw new IndexOutOfRangeException(Constants.EXCEPTION_INDEX_OUT_OF_RANGE);
                }

                if (args.Length < Constants.maxArgs)
                {
                    throw new IndexOutOfRangeException(Constants.EXCEPTION_INDEX_OUT_OF_RANGE);
                }

                if (args.Length == Constants.maxArgs)
                {
                    var dataStructure = new DataStructure
                    {
                        Input_mode = args[0],
                        Input_address = args[1]
                    };

                    var result = OperationStatus.OPERATION_FAILED;

                    switch (args[0])
                    {
                        case Constants.filesystemValue:
                            {
                                dataProcessing = new FileSystemController(dataStructure);
                                result = dataProcessing.StartProcessing();
                            }
                            break;

                        case Constants.httpValue:
                            {
                                dataProcessing = new HttpController(dataStructure);
                                result = dataProcessing.StartProcessing();
                            }
                            break;

                        case Constants.helpValue:
                            {
                                HelpInformation();
                            }
                            break;

                        default:
                            {
                                throw new ArgumentException(Constants.EXCEPTION_ARGUMENT);
                            }
                    }

                    switch (result)
                    {
                        case OperationStatus.OPERATION_FAILED: { } break;
                        case OperationStatus.OPERATION_SUCCEEDED: { } break;
                        case OperationStatus.PATH: { Console.WriteLine(Constants.ERROR_PATH); } break;
                        case OperationStatus.LIST_OF_FILES: { Console.WriteLine(Constants.ERROR_LIST_OF_FILES); } break;
                        case OperationStatus.LIST_OF_INPUT_DATA: { Console.WriteLine(Constants.ERROR_LIST_OF_INPUT_DATA); } break;
                        case OperationStatus.PROCESSING_INPUT_DATA: { Console.WriteLine(Constants.ERROR_PROCESSING_INPUT_DATA); } break;
                        case OperationStatus.WRITE_DATA: { Console.WriteLine(Constants.ERROR_WRITE_DATA); } break;
                        case OperationStatus.DOWNLOAD_FILES: { Console.WriteLine(Constants.ERROR_DOWNLOAD_FILES); } break;
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
