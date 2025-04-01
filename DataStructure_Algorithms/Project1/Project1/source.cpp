#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main()
{
	int T;
	int age = 52;
	vector<char> strAge;

	while (1)
	{
		if (age == 0)
		{
			break;
		}
		int temp = age % 10;
		age /= 10;
		strAge.push_back(temp - 0 + 'a');
	}
	reverse(strAge.begin(), strAge.end());

	for (int i = 0; i < strAge.size(); ++i)
	{
		cout << strAge[i];
	}
	return 0;
}