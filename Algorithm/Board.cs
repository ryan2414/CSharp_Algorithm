using System;
namespace Algorithm
{
    #region List
    public class MyList<T>
    {
        const int DEFAULTSize = 1;
        T[] _data = new T[DEFAULTSize];

        public int Count; // 실제 사용 중인 데이터 개수 
        public int Capacity { get { return _data.Length; } } // 예약된 데이터 개수

        // O(1) 예외 케이스 : 이사 비용은 무시한다 
        public void Add(T item)
        {
            // 1. 공간이 충분히 남아 있는지 확인한다 .
            if (Count >= Capacity)
            {
                // 공간을 다시 늘려서 확보한다
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                {
                    newArray[i] = _data[i];
                }
                _data = newArray;
            }

            // 2. 공간에다가 데이터를 넣어준다
            _data[Count] = item;
            Count++;
        }

        // 인덱서
        // O(1)
        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        // O(N)
        public void RemoveAt(int index)
        {
            // 101 102 104 105 103
            for (int i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i - 1];
            }
            _data[Count - 1] = default(T); //기본값으로 초기화 
            Count--;
        }
    }
    #endregion

    #region LinkedList
    public class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }

    public class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null; // 첫번째
        public MyLinkedListNode<T> Tail = null; // 마지막 
        public int Count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newMyLinkedListNode = new MyLinkedListNode<T>();
            newMyLinkedListNode.Data = data;

            // 만약에 아직 방이 없다, 새로 추가한 방이 첫번째 방이 곧 Head.
            if (Head == null)
            {
                Head = newMyLinkedListNode;
            }

            // 기존의 [마지막 방]과 [새로 추가되는 방]을 연결해 준다 .
            if (Tail != null)
            {
                Tail.Next = newMyLinkedListNode;
                newMyLinkedListNode.Prev = Tail;
            }

            // [새로 추가 되는 방]을 [마지막 방]으로 인정한다 .
            Tail = newMyLinkedListNode;
            Count++;
            return newMyLinkedListNode;
        }

        // O(1)
        public void Remove(MyLinkedListNode<T> MyLinkedListNode)
        {
            // [기존의 첫번째 방 다음 방]을 [첫번째 방]으로 인정한다. 
            if (Head == MyLinkedListNode)
            {
                Head = Head.Next;
            }

            // [기존의 마지막 방의 이전 방]을 [마지막 방]으로 인정한다.
            if (Tail == MyLinkedListNode)
            {
                Tail = Tail.Prev;
            }

            if (MyLinkedListNode.Prev != null)
            {
                MyLinkedListNode.Prev.Next = MyLinkedListNode.Next;
            }

            if (MyLinkedListNode.Next != null)
            {
                MyLinkedListNode.Next.Prev = MyLinkedListNode.Prev;
            }

            Count--;
        }
    }
    #endregion


    public class Board
    {
        //public int[] _data = new int[25]; // 배열
        //public List<int> _data2 = new List<int>(); //동적 배열 (C++ -> Vector)
        //public MyList<int> _data2 = new MyList<int>(); //동적 배열 (C++ -> Vector)
        //public LinkedList<int> _data3 = new LinkedList<int>(); // (양뱡향 ) 연결 리스트 (C++ -> List) 
        //public MyLinkedList<int> _data3 = new MyLinkedList<int>(); // (양뱡향 ) 연결 리스트 (C++ -> List) 

        const char CIRCLE = '\u25cf';

        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }

        internal void Initialize(int size, Player player)
        {
            #region List
            //_data2.Add(101);
            //_data2.Add(102);
            //_data2.Add(103);
            //_data2.Add(104);
            //_data2.Add(105);

            //int tem = _data2[2];

            //_data2.RemoveAt(2);
            #endregion

            #region LinkedList
            //_data3.AddLast(101);
            //_data3.AddLast(102);
            ////LinkedListNode<int> node = _data3.AddLast(103);
            //MyLinkedListNode<int> node = _data3.AddLast(103);
            //_data3.AddLast(104);
            //_data3.AddLast(105);

            //_data3.Remove(node);
            #endregion

            if (size % 2 == 0)
            {
                return;
            }

            _player = player;
                
            Tile = new TileType[size, size];
            Size = size;

            // Mazes for Programmers
            //GenerateByBinaryTree();
            GenerateBySideWinder();

        }

        private void GenerateBySideWinder()
        {
            // 일단 길을 다 막아버리는 작업 
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }


            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            // Binary Tree Algorithm
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randIndex = rand.Next(0, count);
                        Tile[y + 1, x - randIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }

        private void GenerateByBinaryTree()
        {

            // 일단 길을 다 막아버리는 작업 
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }


            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            // Binary Tree Algorithm
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;

                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;

                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    // 플레이어 좌표를 갖고 와서 그 좌표 현재 y, x가 일치하면 플레이어 전용 색상으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);

                    }
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        private ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}

