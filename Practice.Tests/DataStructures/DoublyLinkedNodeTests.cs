using FluentAssertions;
using Xunit;

namespace Practice.Tests.DataStructures
{
    public class DoublyLinkedNodeTests
    {
        [Fact]
        public void WhenNewNodeIsCreated_PreviousAndNextAreNull()
        {
            var newNode = new DoublyLinkedNode<int>(1);

            newNode.Previous.Should().BeNull();
            newNode.Next.Should().BeNull();
        }

        [Fact]
        public void WhenNewNodeInsertedAfter_ThenFirstNodeHasNextReference()
        {
            var firstNode = new DoublyLinkedNode<int>(1);

            var secondValue = 2;

            firstNode.LinkAfter(secondValue);

            firstNode.Next.Value.Should().Be(secondValue);
        }

        [Fact]
        public void WhenNewNodeInsertedBefore_ThenFirstNodeHasNextReference()
        {
            var firstNode = new DoublyLinkedNode<int>(1);

            var secondValue = 2;

            firstNode.LinkBefore(secondValue);

            firstNode.Previous.Value.Should().Be(secondValue);
        }

        [Fact]
        public void WhenTwoNodesAreInsertedAfter_ThenValuesAreLinkedLIFO()
        {
            var firstNode = new DoublyLinkedNode<int>(1);

            var secondValue = 2;
            var thirdValue = 3;

            firstNode.LinkAfter(thirdValue);
            firstNode.LinkAfter(secondValue);

            firstNode.Next.Value.Should().Be(secondValue);
            firstNode.Next.Next.Value.Should().Be(thirdValue);
        }

        [Fact]
        public void WhenTwoNodesAreInsertedBefore_ThenValuesAreLinkedLIFO()
        {
            var firstNode = new DoublyLinkedNode<int>(1);

            var secondValue = 2;
            var thirdValue = 3;

            firstNode.LinkBefore(thirdValue);
            firstNode.LinkBefore(secondValue);

            firstNode.Previous.Value.Should().Be(secondValue);
            firstNode.Previous.Previous.Value.Should().Be(thirdValue);
        }

        [Fact]
        public void WhenNodeIsInsertedAfter_TheNewNodePreviousIsFirstNode()
        {
            var firstNode  = new DoublyLinkedNode<int>(1);
            var secondNode = firstNode.LinkAfter(2);

            secondNode.Previous.Should().Be(firstNode);
        }

        [Fact]
        public void WhenNodeIsInsertedBefore_TheNewNodeNextIsFirstNode()
        {
            var firstNode = new DoublyLinkedNode<int>(1);
            var secondNode = firstNode.LinkBefore(2);

            secondNode.Next.Should().Be(firstNode);
        }

        [Fact]
        public void WhenNodeIsReplaced_ThenItsPreviousAndNextReferToTheReplacement()
        {
            var firstNode = new DoublyLinkedNode<int>(2);

            var previous = firstNode.LinkBefore(1);
            var next = firstNode.LinkAfter(3);

            var replacement = firstNode.Replace(5);

            previous.Next.Value.Should().Be(5);
            next.Previous.Value.Should().Be(5);

            replacement.Previous.Should().Be(previous);
            replacement.Next.Should().Be(next);
        }
    }
}
