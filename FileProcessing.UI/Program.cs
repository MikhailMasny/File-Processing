﻿using System;
using System.IO;
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
            var dataStructure = new DataStructure(); 

            try
            {
                if (args == null)
                {
                    throw new IndexOutOfRangeException("The index was outside the bounds of the array! Info: -help.");
                }
                if (args.Length < 2)
                {
                    throw new IndexOutOfRangeException("The index was outside the bounds of the array! Info: -help.");
                }
                if (args.Length == 2)
                {
                    switch (args[0])
                    {
                        case Constants.filesystemValue:
                            {
                                dataStructure.Input_mode = Constants.filesystemValue;
                                dataStructure.Input_address = @"C:\Users\Mike\Desktop\Tests";

                                IDataProcessing dataProcessing = new FileSystemController(dataStructure);
                                dataProcessing.GetListOfFiles();
                                dataProcessing.GetListOfInputData();
                                dataProcessing.ProcessInputData();
                                dataProcessing.WriteDataToFile();
                            }
                            break;

                        case Constants.httpValue:
                            {
                                // TODO: Http(args);
                            }
                            break;

                        case Constants.helpValue:
                            {
                                HelpInformation();
                            }
                            break;

                        default:
                            {
                                throw new ArgumentException("The index was outside the bounds of the array! Info: -help.");
                            }
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
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
                //Console.ReadLine();
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
