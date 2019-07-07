using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace TDome.Illion.Tests
{
    [TestClass]
    public class DocumentStoreTests
    {
        /// <summary>
        /// Validate throwing of exception when the number of elements overpass
        /// the limit defined by the constructor. Use reflection to call the 
        /// AddDocument method.
        /// </summary>
        [TestMethod]
        public void AddDocument_OverflowCapacity_Throws()
        {
            DocumentStore documentStore = new DocumentStore(2);

            MethodInfo dynMethod = documentStore.GetType().GetMethod("AddDocument",
            BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod.Invoke(documentStore, new object[] { "item1" });

            dynMethod.Invoke(documentStore, new object[] { "item2" });

            Assert.ThrowsException<TargetInvocationException>(() => dynMethod.Invoke(documentStore, new object[] { "item3" }));
        }
    }

    [TestClass]
    public class DateTransformTests
    {
        /// <summary>
        /// Validate the base case with 4 date strings with different formats, one
        /// of them not valid.
        /// </summary>
        [TestMethod]
        public void ChangeDateFormat_BaseCase_Success()
        {
            var input = new List<string> { "2010/03/30", "15/12/2016", "11-15-2012", "20130720" };

            CollectionAssert.AreEqual(new List<string> { "20161215", "20121115", "20130720" }, DateTransform.ChangeDateFormat(input));
        }
    }

    [TestClass]
    public class WeightedAverageTests
    {
        /// <summary>
        /// Validate the case when the arrays arrive null to the Mean method
        /// </summary>
        [TestMethod]
        public void Mean_NullArrays_Throws()
        {
            int[] values = null;
            int[] weights = null;

            Assert.ThrowsException<ArgumentException>(() => WeightedAverage.Mean(values, weights));
        }

        /// <summary>
        /// Validate the case when the arrays do not have the same size
        /// </summary>
        [TestMethod]
        public void Mean_ArraysDifferentSize_Throws()
        {
            int[] values = new int[] { 1, 2 };
            int[] weights = new int[] { 3 };

            Assert.ThrowsException<ArgumentException>(() => WeightedAverage.Mean(values, weights));
        }

        /// <summary>
        /// Validate the case base case
        /// </summary>
        [TestMethod]
        public void Mean_BaseCaseIntArray_Success()
        {
            int[] IntValues = new int[] { 3, 6 };
            int[] IntWeights = new int[] { 4, 2 };

            Assert.AreEqual(4, WeightedAverage.Mean(IntValues, IntWeights));
        }

        /// <summary>
        /// Validate the case base changing the type of the arrays
        /// </summary>
        [TestMethod]
        public void Mean_BaseCaseSingleArray_Success()
        {
            Single[] SingleValues = new Single[] { 3, 6 };
            Single[] SingleWeights = new Single[] { 4, 2 };

            Assert.AreEqual(4, WeightedAverage.Mean(SingleValues, SingleWeights));
        }

    }
}