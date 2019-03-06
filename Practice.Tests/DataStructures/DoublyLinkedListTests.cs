using FluentAssertions;
using Practice.DataStructures;
using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Practice.Tests.DataStructures
{
    public class DoublyLinkedListTests
    {
        [Fact]
        public void WhenNodesIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => new DoublyLinkedList<int>(null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*nodes*");
        }

        [Fact]
        public void WhenOnlyOneNodeIsAddedInCtor_ThenFirstAndLastAreEqual()
        {
            var ctorInput = new[] { new TestClass("nameName", 1) };

            var list = new DoublyLinkedList<TestClass>(ctorInput);

            list.First.Should().Be(list.Last);
        }

        [Fact]
        public void WhenOnlyOneNodeIsAppended_ThenFirstAndLastAreEqual()
        {
            var input = new TestClass("nameName", 1);

            var list = new DoublyLinkedList<TestClass>();

            list.Append(input);

            list.First.Should().Be(list.Last);
        }

        [Fact]
        public void WhenOnlyOneNodeIsInserted_ThenFirstAndLastAreEqual()
        {
            var input = new TestClass("nameName", 1);

            var list = new DoublyLinkedList<TestClass>();

            list.Insert(input);

            list.First.Should().Be(list.Last);
        }

        [Fact]
        public void WhenMultipleLinkedNodesAreConstructedAndAppended_ThenEachReturnsInForeach()
        {
            var initialValues  = new[] { 1, 2, 3, 4, 5 };
            var appendedValues = new[] { 1, 2, 3, 4, 5 };

            var list = new DoublyLinkedList<int>(initialValues);

            foreach (var appendValue in appendedValues)
            {
                list.Append(appendValue);
            }

            var expectedContents = new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };

            list.Select(x => x.Value).Should().ContainInOrder(expectedContents);
        }

        [Fact]
        public void WhenMultipleLinkedNodesAreConstructedAndInserted_ThenEachReturnsInForeach()
        {
            var initialValues  = new[] { 1, 2, 3, 4, 5 };
            var insertedValues = new[] { 1, 2, 3, 4, 5 };

            var list = new DoublyLinkedList<int>(initialValues);

            foreach(var appendValue in insertedValues)
            {
                list.Insert(appendValue);
            }

            var expectedContents = new[] { 5, 4, 3, 2, 1, 1, 2, 3, 4, 5 };

            list.Select(x => x.Value).Should().ContainInOrder(expectedContents);
        }

        [Fact]
        public void WhenReferenceTypeIsAdded_ThenReferencesAreReturned()
        {
            var ctorInput = new TestClass("nameName", 1212);

            var list = new DoublyLinkedList<TestClass>(new[] { ctorInput });

            list.First.Value.Should().Be(ctorInput);
        }

        [Fact]
        public void WhenEnumerableIsAddedToCtor_ThenItIsEnumeratedOnlyInCtor()
        {
            var inputCount = 10;
            var enumerationCount = 0;

            var ctorInput = Enumerable.Range(0, inputCount).Select(count =>
            {
                enumerationCount++;
                return new TestClass("nameName", count);
            });

            var list = new DoublyLinkedList<TestClass>(ctorInput);

            enumerationCount.Should().Be(inputCount);

            foreach(var record in list)
            {
                var reference = record;
            }

            enumerationCount.Should().Be(inputCount);
        }

        [Fact]
        public void WhenNonGenericEnumeratorIsUsed_ThenItWorks()
        {
            var initialValues = new[] { 1, 2, 3, 4, 5 };

            IEnumerable list = new DoublyLinkedList<int>(initialValues);

            foreach(var record in list)
            {
                record.Should().BeOfType<DoublyLinkedNode<int>>();
            }
        }

        [Fact]
        public void WhenEmptyListIsIterated_ThenNoIterationsAreReturned()
        {
            var list = new DoublyLinkedList<int>();

            var iterations = 0;

            foreach (var node in list)
            {
                iterations++;
            }

            iterations.Should().Be(0);
        }

        [Fact]
        public void WhenListWithOneNodeIsReplaced_ThenFirstAndLastAreReplaced()
        {
            var firstValue  = new TestClass("Name1", 1);
            var secondValue = new TestClass("Name2", 2);

            var list = new DoublyLinkedList<TestClass>(new[] { firstValue });

            list.Replace(list.First, secondValue);

            list.First.Value.Should().Be(secondValue);
            list.Last.Value.Should().Be(secondValue);
        }

        [Fact]
        public void WhenReplacementNodeIsNotInList_ThenFirstAndLastAreNotReplaced()
        {
            var firstValue  = new TestClass("Name1", 1);
            var secondValue = new TestClass("Name2", 2);

            var list = new DoublyLinkedList<TestClass>(new[] { firstValue });

            list.Replace(new DoublyLinkedNode<TestClass>(firstValue), secondValue);

            list.First.Value.Should().Be(firstValue);
            list.Last.Value.Should().Be(firstValue);
        }

        [Fact]
        public void WhenListWithMultipleNodesHasFirstReplaced_IterationStartsWithNewNode()
        {
            var inputCount = 10;
            var ctorInput  = Enumerable.Range(0, inputCount).Select(count =>
                new TestClass("nameName", count));

            var list     = new DoublyLinkedList<TestClass>(ctorInput);
            var newValue = new TestClass("WHOOOOOO!", 1999);

            list.Replace(list.First, newValue);

            TestClass lastValue = null;

            foreach (var node in list)
            {
                if (lastValue is null)
                {
                    lastValue = node.Value;
                }
            }

            lastValue.Should().Be(newValue);
            list.Count().Should().Be(inputCount);
        }

        [Fact]
        public void WhenListWithMultipleNodesHasLastReplaced_IterationEndsWithNewNode()
        {
            var inputCount = 10;
            var ctorInput  = Enumerable.Range(0, inputCount).Select(count =>
                new TestClass("nameName", count));

            var list     = new DoublyLinkedList<TestClass>(ctorInput);
            var newValue = new TestClass("WHOOOOOO!", 1999);

            list.Replace(list.Last, newValue);

            TestClass lastValue = null;

            foreach (var node in list)
            {
                lastValue = node.Value;
            }

            lastValue.Should().Be(newValue);
            list.Count().Should().Be(inputCount);
        }

        [Fact]
        public void WhenListIsConstructed_CountIsEqualToCtorArgumentLength()
        {
            var inputCount = 100;
            var input = Enumerable.Range(0, inputCount);

            var list = new DoublyLinkedList<int>(input);

            list.Count.Should().Be(inputCount);
        }

        [Fact]
        public void WhenListIsAppendedTo_ThenCountIncrements()
        {
            var list = new DoublyLinkedList<int>();

            list.Count().Should().Be(0);

            list.Append(50);

            list.Count().Should().Be(1);
        }

        [Fact]
        public void WhenListIsInsertedTo_ThenCountIncrements()
        {
            var list = new DoublyLinkedList<int>();

            list.Count().Should().Be(0);

            list.Insert(50);

            list.Count().Should().Be(1);
        }


        [Fact]
        public void WhenListItemIsReplaced_ThenCountShouldNotIncrement()
        {
            var inputCount = 100;
            var input = Enumerable.Range(0, inputCount);

            var list = new DoublyLinkedList<int>(input);

            list.Replace(list.First, 1000000);

            list.Count.Should().Be(inputCount);
        }

        public class TestClass
        {
            public string Name { get; }
            public int    Id   { get; }

            public TestClass(string name, int id)
            {
                Name = name;
                Id   = id;
            }
        }
    }
}
