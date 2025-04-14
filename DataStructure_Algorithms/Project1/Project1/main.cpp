#include <iostream>
#include "Queue.cpp"
using namespace std;

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	Queue q;
	int n;
	cin >> n;
	
	string str;
	
	for (int i = 0; i < n; ++i)
	{
		cin >> str;
		if (str.compare("push") == 0)
		{
			int data;
			cin >> data;
			q.Push(data);
		}
		else if (str.compare("pop") == 0)
		{
			cout << q.Pop() << '\n';
		}
		else if (str.compare("size") == 0)
		{
			cout << q.Size() << '\n';
		}
		else if (str.compare("empty") == 0)
		{
			cout << q.Empty() << '\n';
		}
		else if (str.compare("front") == 0)
		{
			cout << q.Front() << '\n';
		}
		else if (str.compare("back") == 0)
		{
			cout << q.Back() << '\n';
		}
	}
}