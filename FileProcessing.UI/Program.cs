using System;
using System.IO;

namespace FileProcessing.UI
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        case "filesystem":
                            {
                                // TODO: FileSystem(args);
                            }
                            break;

                        case "http":
                            {
                                // TODO: Http(args);
                            }
                            break;

                        case "-help":
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
