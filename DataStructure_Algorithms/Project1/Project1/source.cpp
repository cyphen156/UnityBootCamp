/**
 * ���� ����Լ��� ������_17478
 *
 *
 * ���ѻ���
 *****************************************
 *
 *****************************************
 *
 *
 *
 * ����
 *
 *
 * Ǯ�̽ð� 0��
 */


#include <iostream>
#include <string>

using namespace std;

void RecursiveCall(int length, int originLength)
{
    for (int i = length; i < originLength; ++i)
    {
        cout << "____";
    }
    cout << "\"����Լ��� ������?\"\n";

    if (length == 0)
    {
        for (int i = length; i < originLength; ++i)
        {
            cout << "____";
        }
        cout << "\"����Լ��� �ڱ� �ڽ��� ȣ���ϴ� �Լ����\"\n";
    }
    else
    {
        for (int i = length; i < originLength; ++i)
        {
            cout << "____";
        }
        cout << "\"�� ����. �������� �� �� ����⿡ �̼��� ��� ������ ����� ������ �־���.\n";
        for (int i = length; i < originLength; ++i)
        {
            cout << "____";
        }

        cout << "���� ������� ��� �� ���ο��� ������ ������ �߰�, ��� �����Ӱ� ����� �־���.\n";
        for (int i = length; i < originLength; ++i)
        {
            cout << "____";
        }

        cout << "���� ���� ��κ� �ǾҴٰ� �ϳ�. �׷��� ��� ��, �� ���ο��� �� ���� ã�ƿͼ� ������.\"\n";
        RecursiveCall(length - 1, originLength);
    }
    for (int i = length; i < originLength; ++i)
    {
        cout << "____";
    }
    cout << "��� �亯�Ͽ���.\n";
}

int main(void)
{
    int N;

    cin >> N;

    cout << "��� �� ��ǻ�Ͱ��а� �л��� ������ �������� ã�ư� ������." << endl;
    RecursiveCall(N, N);

    return 0;
}

