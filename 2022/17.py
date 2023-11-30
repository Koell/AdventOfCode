import helper.help_functions as hf
import os.path
import time


def run():
    file_name = os.path.basename(__file__).replace(".py", "")
    test_input = hf.extract_list(f"inputs/tinput_{file_name}")
    real_input = hf.extract_list(f"inputs/input_{file_name}")
    print("Testrun:")
    solve(test_input)
    print("\nSolution:")
    solve(real_input)


def solve(lines):
    sol1 = case1(lines)
    sol2 = case2(lines)

    print(f"case 1: {sol1}")
    print(f"case 2: {sol2}")


def case1(lines):
    start = time.time()
    result = 0



    for line in lines:
        pass
    end = time.time()
    print(f"Time needed: {end - start}")
    return result


def case2(lines):
    start = time.time()
    result = 0

    for line in lines:
        pass
    end = time.time()
    print(f"Time needed: {end - start}")
    return result


run()
