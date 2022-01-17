#!/bin/sh

# example input given in the instructions
plaintext="Hello World"
ciphertext="RgdIKNgHn2Wg7jXwAykTlA=="

# Instead of having to copy/paste the commented command below, you can just run this script from the command line ./testYourProgram.ps1
# dotnet run "Hello World" "RgdIKNgHn2Wg7jXwAykTlA=="
# example output:
# 26564295

dotnet run $plaintext $ciphertext
