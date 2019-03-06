using Practice;

public class DoublyLinkedNode<T>
{
    public T Value { get; }

    public DoublyLinkedNode<T> Previous { get; private set; }

    public DoublyLinkedNode<T> Next     { get; private set; }

    public DoublyLinkedNode(T value)
    {
        PracticeExtensions.ThrowIfNull(value, nameof(value));

        Value = value;
    }

    internal DoublyLinkedNode<T> LinkAfter(T value)
    {
        PracticeExtensions.ThrowIfNull(value, nameof(value));

        var newNode = new DoublyLinkedNode<T>(value);

        ReplaceNext(newNode);

        newNode.Previous = this;

        return newNode;
    }

    internal DoublyLinkedNode<T> LinkBefore(T value)
    {
        PracticeExtensions.ThrowIfNull(value, nameof(value));

        var newNode = new DoublyLinkedNode<T>(value);

        ReplacePrevious(newNode);

        newNode.Next = this;

        return newNode;
    }

    internal DoublyLinkedNode<T> Replace(T value)
    {
        PracticeExtensions.ThrowIfNull(value, nameof(value));

        var replacement = new DoublyLinkedNode<T>(value);

        ReplacePrevious(replacement);

        ReplaceNext(replacement);

        return replacement;
    }

    private void ReplacePrevious(DoublyLinkedNode<T> newNode)
    {
        if (this.Previous != null)
        {
            this.Previous.Next = newNode;
            newNode.Previous = this.Previous;
        }

        this.Previous = newNode;
    }

    private void ReplaceNext(DoublyLinkedNode<T> newNode)
    {
        if (this.Next != null)
        {
            this.Next.Previous = newNode;
            newNode.Next = this.Next;
        }

        this.Next = newNode;
    }
}