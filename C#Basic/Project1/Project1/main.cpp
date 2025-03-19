#include <stdio.h>

int main() {
    int num = 0x12345678;  // 16진수 값 (4바이트)
    unsigned char* p = (unsigned char*)&num;

    printf("메모리에 저장된 바이트 순서: ");
    for (int i = 0; i < sizeof(num); i++) {
        printf("%02X ", p[i]);  // 바이트 단위 출력
    }
    printf("\n");

    return 0;
}
