using System;
using System.Collections;
using System.Collections.Generic;
using ClassLibrary_LabWork2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CLLTests
    {
        //тестові дані
        [TestCase(1, 2, 3, 4, 5, 6)]
        [TestCase(0)]
        public void ConstructorCount_Get_ShouldReturnCorrectValue(params int[] elements) //тест на працездатність конструктора
        {
            //arrange
            CustomLL<int> list = new CustomLL<int>(elements);

            //act
            var expected = elements.Length;
            var actual = list.Count;

            //assert
            Assert.AreEqual(expected, actual, message: "Count works incorrectly");
            Assert.AreEqual(elements[0], list[0], message: "Get index works incorrectly");
        }

        [Test]
        public void IsEmpty_Return0()
        {
            //arrange
            var expected = true;

            //act
            CustomLL<int> list = new CustomLL<int>();
            var actual = list.IsEmpty;

            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, list.Count, message: "IsEmpty works incorrectly");
                Assert.AreEqual(expected, actual, message: "IsEmpty works incorrectly");
            });
        }

        [Test]
        public void Count_Return0() //тест на працездатність властивості Count
        {
            //arrange
            int expected = 0;

            //act
            CustomLL<int> list = new CustomLL<int>();

            //assert
            Assert.AreEqual(expected, list.Count, message: "Count works incorrectly");
        }

        [Test]
        public void Clear_ReturnCount0() //тест на працездатність Clear
        {
            //arrange
            CustomLL<int> list = new CustomLL<int>(1, 2, 3);
            Item<int> expectedHead = null;
            Item<int> expectedTail = null;

            //act
            list.Clear();
            int count = list.Count;
            var actualHead = list.Head;
            var actualTail = list.Tail;

            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, count, message: "Clear method works incorrectly ");
                Assert.AreEqual(expectedHead, actualHead, message: "Clear method works incorrectly ");
                Assert.AreEqual(expectedTail, actualTail, message: "Clear method works incorrectly ");
            });
        }

        [TestCase(2, new int[] { 1, 2, 3, 4, 5 }, 4, true)]
        [TestCase(10, new int[] { 1, 2, 3, 4, 5 }, 5, false)]
        [TestCase(15, new int[] { 11, 4, 5 }, 3, false)]
        [TestCase(-1, new int[] { -1, 2, -1, 4, 5 }, 4, true)]
        [TestCase(99, new int[] { -1, 2, -1, 4, 100 }, 5, false)]
        [TestCase(1, new int[] { 1 }, 0, true)]
        public void Remove_Element_ReturnedExpectedCount(int elementToRemove, int[] elements, int expectedCount, bool expectedBool) //тест на працездатність Remove
        {
            //arrange
            CustomLL<int> list = new CustomLL<int>(elements);

            //act
            var actualBool = list.Remove(elementToRemove);

            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedCount, list.Count, message: "Remove method works incorrectly");
                Assert.AreEqual(expectedBool, actualBool, message: "Remove method works incorrectly");
            });
        }

        [TestCase(2,88,new int[] {1,2,3},true)]
        [TestCase(2, -1, new int[] { -11, 52, 93,1000 }, true)]
        [TestCase(1, 1, new int[] {5,4,87}, true)]
        [TestCase(3, 1, new int[] { 5, 4, 87 }, true)]
        public void AddAfter_And_AddBefore_ReturnedExpectedBool(int index,int newElement, int[] elements, bool expectedBool) //тест на працездатність AddAfter_And_AddBefore
        {
            //arrange
            CustomLL<int> list1 = new CustomLL<int>(elements);
            CustomLL<int> list2 = new CustomLL<int>(elements);
            //act
            list1.AddAfter(index, newElement);
            list2.AddBefore(index, newElement);
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedBool, list1.Contains(newElement), message: "AddAfter method works incorrectly");
                Assert.AreEqual(expectedBool, list2.Contains(newElement), message: "AddBefore method works incorrectly");
            });
        }

        [TestCase(88, new int[] { 1, 2, 3 })]
        [TestCase(-1, new int[] { -11, 52, 93, 1000 })]
        public void AddFirst_And_AddLast_ReturnedExpectedInt(int newElement, int[] elements) //тест на працездатність AddFirst_And_AddLast
        {
            //arrange
            CustomLL<int> list1 = new CustomLL<int>(elements);
            CustomLL<int> list2 = new CustomLL<int>(elements);
            //act
            list1.AddFirst(newElement);
            list2.AddLast(newElement);
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(newElement, list1[1], message: "AddFirst method works incorrectly");
                Assert.AreEqual(newElement, list2[list2.Count], message: "AddLast method works incorrectly");
            });
        }

        [Test]
        public void RemoveFirst_And_RemoveLast_ReturnedExpectedInt() //тест на працездатність RemoveFirst_And_RemoveLast
        {
            //arrange
            CustomLL<int> list1 = new CustomLL<int>(1, 2, 3);
            CustomLL<int> list2 = new CustomLL<int>(1, 2, 3);
            //act
            list1.RemoveFirst();
            list2.RemoveLast();
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(false, list1.Contains(1), message: "RemoveFirst method works incorrectly");
                Assert.AreEqual(false, list2.Contains(3), message: "RemoveLast method works incorrectly");
            });
        }

        [Test]
        public void GetEnumerator_OfListAndCustomList_ShouldHaveEqualElements() //тест на працездатність GetEnumerator
        {
            //arrange 
            List<int> list = new List<int>() { 1, 2, 3 };
            CustomLL<int> custom = new CustomLL<int>(1,2,3);
            //act
            var en = list.GetEnumerator();
            var en2 = custom.GetEnumerator();
            //assert
            if (en.MoveNext() && en2.MoveNext())
            {
                Assert.AreEqual(en.Current, en2.Current,
                    message: "GetEnumerator works incorrectly");
            }
        }

        [Test]
        public void Clear_ReturnedArgumentNullException()
        {
            //arrange
            CustomLL<int> list = new CustomLL<int>();
            var expEx = typeof(ArgumentNullException);
            //act
            var actEx = Assert.Catch(() => list.Clear());

            Assert.AreEqual(expEx, actEx.GetType(), message: "Expection error");
        }

        [Test]
        public void Add_ReturnedArgumentNullException()
        {
            //arrange
            CustomLL<object> list = new CustomLL<object>();
            var expEx = typeof(ArgumentNullException);
            //act
            var actEx = Assert.Catch(() => list.Add(null));
            //assert
            Assert.AreEqual(expEx, actEx.GetType(), message: "ArgumentNullException in Add hasn`t been returned");
        }

        [Test]
        public void Remove_ReturnedArgumentNullException()
        {
            //arrange
            CustomLL<object> list = new CustomLL<object>();
            var expEx = typeof(ArgumentNullException);
            //act
            var actEx = Assert.Catch(() => list.Remove(null));
            //assert
            Assert.AreEqual(expEx, actEx.GetType(), message: "ArgumentNullException in Remove hasn`t been returned");
        }

        [Test]
        public void Contains_ReturnedArgumentNullException()
        {
            //arrange
            CustomLL<object> list = new CustomLL<object>();
            var expEx = typeof(ArgumentNullException);
            //act
            var actEx = Assert.Catch(() => list.Contains(null));
            //assert
            Assert.AreEqual(expEx, actEx.GetType(), message: "ArgumentNullException in Contains hasn`t been returned");
        }

        [Test]
        public void Indexer_ReturnedIndexOutOfRangeException()
        {
            //arrange
            CustomLL<int> list = new CustomLL<int>(1,2,3);
            var expEx = typeof(IndexOutOfRangeException);
            //act
            var actEx = Assert.Catch(() => list[4].ToString());
            //assert
            Assert.AreEqual(expEx, actEx.GetType(), message: "IndexOutOfRangeException in Indexer hasn`t been returned");
        }
    }
}
