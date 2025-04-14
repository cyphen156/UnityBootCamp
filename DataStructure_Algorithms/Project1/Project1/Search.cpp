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
	int left = 0, right = linearList.size(); //right ==> 실제 인덱스 위치를 포함시키던지 사이즈 위치를 미포함시키던지
	while (1)
	{
		int mid = left + (right - left) / 2;
		// 만약 right가 매우 큰 수일 경우 오버플로우가 날 수 있음
		cnt++;
		if (linearList[mid] == key)
		{
			cout << "응 있어" << cnt << endl;
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

		// 마지막 순회까지 다 탐색했어
		if (left > right)
		{
			cout << "응 없서";
			break;
		}
	}
	return false;
}

int DuclicateBSearch(vector<int> linearList, int key)
{
	int cnt = 0;
	int left = 0, right = linearList.size(); //right ==> 실제 인덱스 위치를 포함시키던지 사이즈 위치를 미포함시키던지
	bool isFind = false;
	
	while (left < right)
	{
		int mid = left + (right - left) / 2;
		// 만약 right가 매우 큰 수일 경우 오버플로우가 날 수 있음
		cnt++;
		// 찾앗는데 중복이 있을 수 있음
		if (linearList[mid] == key)
		{
			isFind = true;
			right = mid;
		}	

		// 아직 못찾았음
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
	int left = 0, right = linearList.size(); //right ==> 실제 인덱스 위치를 포함시키던지 사이즈 위치를 미포함시키던지
	int tempIDX = -1;
	bool isFind = false;

	while (left < right)
	{
		int mid = left + (right - left) / 2;
		// 만약 right가 매우 큰 수일 경우 오버플로우가 날 수 있음
		cnt++;


		/**
		* key와 같다 => 검색 범위를 왼쪽으로 옮긴다.
		* key보다 작다 => 검색 범위를 왼쪽으로 옮긴다.
		* key보다 크다 => 검색 범위를 오른쪽으로 옮긴다.
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

// 나보다 큰 애들을 찾는데 그중 가장 작은 애를 찾는다.
int UpperBoundSearch(vector<int> linearList, int key)
{
	int cnt = 0;
	int left = 0, right = linearList.size(); //right ==> 실제 인덱스 위치를 포함시키던지 사이즈 위치를 미포함시키던지
	int tempIDX = -1;
	bool isFind = false;

	while (left < right)
	{
		int mid = left + (right - left) / 2;
		// 만약 right가 매우 큰 수일 경우 오버플로우가 날 수 있음
		cnt++;
		/**
		* key와 같다 => 검색 범위를 오른쪽으로 옮긴다.
		* key보다 작다 => 검색 범위를 오른쪽으로 옮긴다.
		* key보다 크다 => 검색 범위를 왼쪽으로 옮긴다.
		*/

		// 찾아온 놈이 키보다 작거나 같으면
		if (linearList[mid] <= key)
		{
			left = mid + 1;
		}
		// 크면 찾는다.
		else
		{
			right = mid;
			tempIDX = right;
		}
	}
	return tempIDX;
}