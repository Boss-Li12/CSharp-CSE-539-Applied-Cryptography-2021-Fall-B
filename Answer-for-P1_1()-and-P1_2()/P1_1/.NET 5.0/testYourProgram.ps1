# This is a powershell script for Windows users

# example input given in the instructions
$twelveHexadecimalDigits = "B1 FF FF CC 98 80 09 EA 04 48 7E C9"

# Instead of having to copy/paste the commented command below, you can just run this script from the command line ./testYourProgram.ps1
# dotnet run "B1 FF FF CC 98 80 09 EA 04 48 7E C9"
# example output:
# 42 4D 4C 00 00 00 00 00 00 00 1A 00 00 00 0C 00 00 00 04 00 04 00 01 00 18 00 02 03
# FF FE FC FC 03 03 FC FC FC FC FC FF FC 00 02 01 FD FF FD 00 00 00 FF 00 02 FE
# FC FD FD 02 00 FF FE FF FE FF FD 00 01 03 FC FD FC 00 02 01

dotnet run "$twelveHexadecimalDigits"
