namespace StackAndQueue;

class Program
{
    // 선형 자료구조 -> 자료가 일렬로

    // 스택 :  LIFO(후입선출 Last In First Out)
    // 큐 : FIFO(선입선출 First In First Out)
     
    static void Main(string[] args)
    {
        Stack<int> stack = new Stack<int>();

        stack.Push(101);
        stack.Push(102);
        stack.Push(103);
        stack.Push(104);
        stack.Push(105);

        int data = stack.Pop();
        int dat2 = stack.Peek();

        Queue<int> queue = new Queue<int>();

        queue.Enqueue(101);
        queue.Enqueue(102);
        queue.Enqueue(103);
        queue.Enqueue(104);
        queue.Enqueue(105);

        int _data = queue.Dequeue();
        int _data2 = queue.Peek();
    }
}

