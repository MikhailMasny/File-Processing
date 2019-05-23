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
                    throw new IndexOutOfRangeException("The index was outside the bounds of the array! Info: -help all.");
                }
                if (args.Length < 2)
                {
                    throw new IndexOutOfRangeException("The index was outside the bounds of the array! Info: -help all.");
                }
                if (args.Length == 2)
                {
                    // TODO: Реализовать функционал.
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
    }
}
