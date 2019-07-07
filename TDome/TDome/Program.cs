using System;
using System.Collections.Generic;
using System.Reflection;
using TDome.Illion;

namespace TDome
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Illion
            IllionDocumentStore();

            IllionDateTransform();

            IllionWeightedAverage();
            #endregion

            System.Threading.Thread.Sleep(3000);
        }

        static void IllionDocumentStore()
        {
            DocumentStore documentStore = new DocumentStore(2);
            //documentStore.AddDocument("item");

            MethodInfo dynMethod = documentStore.GetType().GetMethod("AddDocument",
            BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod.Invoke(documentStore, new object[] { "item1" });

            dynMethod.Invoke(documentStore, new object[] { "item2" });

            try
            {
                dynMethod.Invoke(documentStore, new object[] { "item3" });
            }
            catch (TargetInvocationException e)
            {
                Console.WriteLine(e.InnerException.GetType().ToString());
            }

            Console.WriteLine(documentStore);
        }

        static void IllionDateTransform()
        {
            var input = new List<string> { "2010/03/30", "15/12/2016", "11-15-2012", "20130720" };
            DateTransform.ChangeDateFormat(input).ForEach(Console.WriteLine);
        }

        static void IllionWeightedAverage()
        {
            #region WeightedAverage
            int[] values = null;
            int[] weights = null;

            try
            {
                Console.WriteLine(WeightedAverage.Mean(values, weights));
            } catch (ArgumentException ae)
            {
                Console.WriteLine("Throw ArgumentException");
            }

            int[] IntValues = new int[] { 3, 6 };
            int[] IntWeights = new int[] { 4, 2 };

            Console.WriteLine(WeightedAverage.Mean(IntValues, IntWeights));

            Single[] SingleValues = new Single[] { 3, 6 };
            Single[] SingleWeights = new Single[] { 4, 2 };

            Console.WriteLine(WeightedAverage.Mean(SingleValues, SingleWeights));
            #endregion
        }
    }
}
