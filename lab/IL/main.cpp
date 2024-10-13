#include <iostream>

// Declare the C# add function
extern "C" {
    int add(int a, int b);  // Declare the function as it is in C#
}

int main() {
    // Call the C# add function
    int result = add(2, 2);
    std::cout << "C++: The result of add(2, 2) is " << result << std::endl;
    return 0;
}
