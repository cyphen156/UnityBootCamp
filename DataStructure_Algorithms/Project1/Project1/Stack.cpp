#include <iostream>
#include <string>
using namespace std;

class Node
{

public:
	int data;
	Node* prev = nullptr;
	Node(int inData) : data(inData), prev(nullptr) {}

	~Node() {};
};

class Stack
{
public:
	int size;
	Node* Top;

	Stack() : 
		Top(nullptr)
		, size(0)
	{
	}
	~Stack()
	{
		for (int i = 0; i < size; ++i)
		{
			Pop();
		}
	}
	void Push(int data) 
	{
		Node* node = new Node(data);

		node->prev = Top;
		Top = node;
		++size;
	};

	void Pop()
	{
		if (size != 0)
		{
			Node* temp = Top;
			Top = temp->prev;
			delete(temp);
			size--;
		}
	};

	int Peek()
	{
		if (size != 0)
		{
			return Top->data;
		}
		return -1;
	};
};

class LinearStack
{

public:
	int top;
	int size;
	int* data;

	LinearStack() :
		top(0),
		size(10)
	{
		data = new int[size];
	}

	~LinearStack()
	{
		for (int i = 0; i < top; ++i)
		{
			Pop();
		}
	}

	void Push(int inData)
	{
		// 할당 된게 다 찻어
		if (top == size)
		{
			size *= 2;
			int* newData = new int[size];
			// reallocate
			for (int i = 0; i < size / 2; ++i)
			{
				newData[i] = data[i];
			}
			data = newData;
		}
		top++;
		data[top] = inData;
	}
	
	int Pop()
	{
		return data[top--];
	}


	int Size()
	{
		return top;
	}

	bool Empty()
	{
		if (top == 0)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}
	
	int Top()
	{
		if (Empty())
		{
			return -1;
		}
		return data[top];
	}
};

//int main()
//{
//	ios::sync_with_stdio(false);
//	cin.tie(nullptr);
//	LinearStack ls;
//	int n;
//	cin >> n;
//
//	string str;
//
//	for (int i = 0; i < n; ++i)
//	{
//		cin >> str;
//		if (str.compare("push") == 0)
//		{
//			int data;
//			cin >> data;
//			ls.Push(data);
//		}
//		else if (str.compare("pop") == 0)
//		{
//			cout << ls.Pop() << '\n';
//		}
//		else if (str.compare("size") == 0)
//		{
//			cout << ls.Size() << '\n';
//		}
//		else if (str.compare("empty") == 0)
//		{
//			cout << ls.Empty() << '\n';
//		}
//		else if (str.compare("top") == 0)
//		{
//			cout << ls.Top() << '\n';
//		}
//	}
//	return 0;
//}