#include <iostream>
#include <algorithm>
#include <vector>

using namespace std;

bool LinearSearch(vector<int> linearList, int key);
bool BSearch(vector<int> linearList, int key);
int DuclicateBSearch(vector<int> linearList, int key);
int LowerBoundSearch(vector<int> linearList, int key);
int UpperBoundSearch(vector<int> linearList, int key);

//int main()
//{
//	vector<int> lList, lList2, upperList;
//	for (int i = 0; i < 10000; ++i)
//	{
//		lList.push_back(i);
//	}
//
//
//	lList2 = {1, 2, 5, 5, 13, 13, 13, 13, 15, 17, 20};
//	upperList = {1, 3, 5, 7, 17, 17, 19, 20};
//	//LinearSearch();
//	BSearch(lList, 55);
//	cout << DuclicateBSearch(lList2, 13) << endl;
//
//	cout << "Upperbound Test : " << UpperBoundSearch(upperList, 17) << endl;
//
//	return 0;
//}


bool LinearSearch(vector<int> linearList, int key)
{
	for (int i = 0; i < linearList.size(); ++i)
	{
		if (linearList[i] == key)
		{
			return true;
		}
	}
	return false;
};

bool BSearch(vector<int> linearList, int key)
{
	int cnt = 0;
	int left = 0, right = linearList.size(); //right ==> ���� �ε��� ��ġ�� ���Խ�Ű���� ������ ��ġ�� �����Խ�Ű����
	while (1)
	{
		int mid = left + (right - left) / 2;
		// ���� right�� �ſ� ū ���� ��� �����÷ο찡 �� �� ����
		cnt++;
		if (linearList[mid] == key)
		{
			cout << "�� �־�" << cnt << endl;
			return true;
		}

		else
		{
			if (linearList[mid] < key)
			{
				left = mid + 1;
			}
			else
			{
				right = mid;
			}
		}

		// ������ ��ȸ���� �� Ž���߾�
		if (left > right)
		{
			cout << "�� ����";
			break;
		}
	}
	return false;
}

int DuclicateBSearch(vector<int> linearList, int key)
{
	int cnt = 0;
	int left = 0, right = linearList.size(); //right ==> ���� �ε��� ��ġ�� ���Խ�Ű���� ������ ��ġ�� �����Խ�Ű����
	bool isFind = false;
	
	while (left < right)
	{
		int mid = left + (right - left) / 2;
		// ���� right�� �ſ� ū ���� ��� �����÷ο찡 �� �� ����
		cnt++;
		// ã�Ѵµ� �ߺ��� ���� �� ����
		if (linearList[mid] == key)
		{
			isFind = true;
			right = mid;
		}	

		// ���� ��ã����
		else if (linearList[mid] < key)
		{
			left = mid + 1;
		}
		else
		{
			right = mid;
		}
	}

	if (isFind)
	{
		return 1;
	}
	return -1;
}

int LowerBoundSearch(vector<int> linearList, int key)
{
	int cnt = 0;
	int left = 0, right = linearList.size(); //right ==> ���� �ε��� ��ġ�� ���Խ�Ű���� ������ ��ġ�� �����Խ�Ű����
	int tempIDX = -1;
	bool isFind = false;

	while (left < right)
	{
		int mid = left + (right - left) / 2;
		// ���� right�� �ſ� ū ���� ��� �����÷ο찡 �� �� ����
		cnt++;


		/**
		* key�� ���� => �˻� ������ �������� �ű��.
		* key���� �۴� => �˻� ������ �������� �ű��.
		* key���� ũ�� => �˻� ������ ���������� �ű��.
		*/
		if (linearList[mid] > key)
		{
			right = mid;
			tempIDX = right;
		}
		else
		{
			left = mid + 1;
		}
	}
	return tempIDX;
}

// ������ ū �ֵ��� ã�µ� ���� ���� ���� �ָ� ã�´�.
int UpperBoundSearch(vector<int> linearList, int key)
{
	int cnt = 0;
	int left = 0, right = linearList.size(); //right ==> ���� �ε��� ��ġ�� ���Խ�Ű���� ������ ��ġ�� �����Խ�Ű����
	int tempIDX = -1;
	bool isFind = false;

	while (left < right)
	{
		int mid = left + (right - left) / 2;
		// ���� right�� �ſ� ū ���� ��� �����÷ο찡 �� �� ����
		cnt++;
		/**
		* key�� ���� => �˻� ������ ���������� �ű��.
		* key���� �۴� => �˻� ������ ���������� �ű��.
		* key���� ũ�� => �˻� ������ �������� �ű��.
		*/

		// ã�ƿ� ���� Ű���� �۰ų� ������
		if (linearList[mid] <= key)
		{
			left = mid + 1;
		}
		// ũ�� ã�´�.
		else
		{
			right = mid;
			tempIDX = right;
		}
	}
	return tempIDX;
}