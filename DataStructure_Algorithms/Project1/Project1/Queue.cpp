#include <iostream>

class Queue 
{
public:
	int front;
	int rear;

	int size;
	int* queue;

	Queue() :
		front(-1),
		rear(-1),
		size(10)
	{
		queue = new int[size];
	}
	~Queue()
	{
		delete[] queue;
	};
	void Push(int inData)
	{
		if (Empty())
		{
			front = rear = 0;
			queue[rear] = inData;
			return;
		}
		
		// 할당된 인덱스를 다 썻을때
		// reallocate
		if (rear + 1 >= size)
		{
			int newSize = 2 * size;

			int* newQueue = new int[newSize];
			int count = rear - front + 1;
			for (int i = 0; i < count; ++i)
			{
				newQueue[i] = queue[front + i];
			}
			delete[] queue;
			queue = newQueue;
			
			size = newSize;
			rear = count - 1;
			front = 0;
		}
		
		queue[++rear] = inData;
	}

	int Pop()
	{
		if (Empty())
		{
			return -1;
		}
		int temp = queue[front++];

		// 땡겨오기
		// 언제? 반 이상 썻거나 혼자 남았을 때
		if (front > (size / 2) || front > rear)
		{
			int count = rear - front + 1;
			for (int i = 0; i < count; ++i)
			{
				queue[i] = queue[front + i];
			}
			rear = count - 1;
			front = 0;
		}

		// 완전 초기화
		else if (front > rear)
		{
			front = rear = -1;
		}
		return temp;
	}

	int Size()
	{
		return rear - front + 1;
	};

	bool Empty()
	{
		// 프론트가 리어 인덱스를 넘어감
		if (front > rear || front == -1)
		{
			return 1;
		}
		return 0;
	}

	int Front()
	{
		if (Empty())
		{
			return -1;
		}
		return queue[front];
	};

	int Back()
	{
		if (Empty())
		{
			return -1;
		}
		return queue[rear];
	};
};