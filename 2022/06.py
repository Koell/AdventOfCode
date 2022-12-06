import os.path

import helper.help_functions as hf
from collections import Counter


def run():
    file_name = os.path.basename(__file__).replace(".py", "")
    test_input = hf.extract_list(f"inputs/tinput_{file_name}")
    real_input = hf.extract_list(f"inputs/input_{file_name}")
    print("Testrun:")
    solve(test_input)
    print("\n\nSolution:")
    solve(real_input)


def solve(lines):
    sol1 = case1(lines)
    sol2 = case2(lines)

    print(f"case 1: {sol1}")

    print(f"\ncase 2: {sol2}")


def case1(lines):
    result = 0
    line = lines[0]
    chunk_size = 4
    for i in range(0, len(line) - chunk_size):
        buffer = line[i:i+chunk_size]
        freq = Counter(buffer)
        if len(buffer) == len(freq):
            result = i + chunk_size
            break

    return result


def case2(lines):
    result = 0
    line = lines[0]
    chunk_size = 14
    for i in range(0, len(line) - chunk_size):
        buffer = line[i:i + chunk_size]
        freq = Counter(buffer)
        if len(buffer) == len(freq):
            result = i + chunk_size
            break

    return result


run()
