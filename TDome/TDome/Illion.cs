using System;
using System.Collections.Generic;
using System.Globalization;

namespace TDome.Illion
{
    /// <summary>
    /// Class to store documents in a List, used to check topics
    /// like string interpolation, validation, reflection of private properties
    /// </summary>
    public class DocumentStore
    {
        private readonly List<string> documents = new List<string>();
        private readonly int capacity;

        public DocumentStore(int capacity)
        {
            this.capacity = capacity;
        }

        public int Capacity { get { return this.capacity; } }

        public IEnumerable<string> Documents { get { return documents; } }

        private void AddDocument(string document)
        {
            if (documents.Count + 1 > this.capacity) // bug fixed
                throw new InvalidOperationException();

            documents.Add(document);
        }

        public override string ToString()
        {
            return $"Document store: ({documents.Count})/({Capacity})"; //C# string interpolation
        }
    }

    /// <summary>
    /// Class to change the format of a set of dates, for an restringered set
    /// of formats. Check manipulation of date formats (validation and set format)
    /// </summary>
    public class DateTransform
    {
        public static List<string> ChangeDateFormat(List<string> dates)
        {
            List<string> result = new List<string>();
            DateTime expectedDate;

            string[] formats = { "dd/MM/yyyy", "MM-dd-yyyy", "yyyyMMdd" };

            foreach (string date in dates)
            {
                if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"),
                                        DateTimeStyles.None, out expectedDate))
                {
                    result.Add(expectedDate.ToString("yyyyMMdd"));
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Class to find the total and weight of two sets of elements on arrays.
    /// Check the use of generics, dynamic types, and validation setting 
    /// custom exceptions.
    /// </summary>
    public class WeightedAverage
    {
        public static double Mean<T>(IList<T> numbers, IList<T> weights)
        {
            if (numbers == null || weights == null)
                throw new ArgumentException();

            if (numbers.Count != weights.Count)
                throw new ArgumentException();

            dynamic total = 0, totalWeights = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                dynamic dx = numbers[i], dy = weights[i];

                total += dx * dy;
                totalWeights += dy;
            }

            return total / totalWeights;
        }
    }
}
