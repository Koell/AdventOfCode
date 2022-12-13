import os.path
import helper.help_functions as hf
import ast
import functools


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


def is_sorted(left, right) -> int:
    result = 0

    if isinstance(left, int) and isinstance(right, int):
        result = -1 if left < right else 1 if left > right else 0
        return result
    elif isinstance(left, int):
        left = [left]
    elif isinstance(right, int):
        right = [right]

    for i in range(len(left)):
        if i >= len(right):
            result = 1
            break

        result = is_sorted(left[i], right[i])
        if result == 0:
            continue
        else:
            break
    if result == 0 and len(left) < len(right):
        result = -1

    return result


def case1(lines):
    result = 0

    packets = []

    for line in lines:
        if line:
            packets.append(ast.literal_eval(line))

    i = 0
    pairs = []
    pair = 1
    while i < len(packets):
        left = packets[i]
        right = packets[i+1]
        if is_sorted(left, right) == -1:
            pairs.append(pair)

        pair += 1
        i += 2
    result = sum(pairs)
    return result


def case2(lines):
    result = 0

    packets = []

    for line in lines:
        if line:
            packets.append(ast.literal_eval(line))
    packets.append([[2]])
    packets.append([[6]])

    sorted_p = sorted(packets, key=functools.cmp_to_key(is_sorted))
    index_s = 0
    index_f = 0

    for i, packet in enumerate(sorted_p, 1):
        if packet == [[2]]:
            index_s = i
        elif packet == [[6]]:
            index_f = i
        else:
            continue

        if index_s and index_f:
            break

    result = index_s * index_f
    return result


run()
