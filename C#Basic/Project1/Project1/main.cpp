#include <stdio.h>

int main() {
    int num = 0x12345678;  // 16���� �� (4����Ʈ)
    unsigned char* p = (unsigned char*)&num;

    printf("�޸𸮿� ����� ����Ʈ ����: ");
    for (int i = 0; i < sizeof(num); i++) {
        printf("%02X ", p[i]);  // ����Ʈ ���� ���
    }
    printf("\n");

    return 0;
}
