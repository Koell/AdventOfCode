import os.path

import helper.help_functions as hf
import re


def run():
    file_name = os.path.basename(__file__).replace(".py", "")
    test_input = hf.extract_list(f"inputs/tinput_{file_name}")
    real_input = hf.extract_list(f"inputs/input_{file_name}")
    print("Testrun:")
    solve(test_input)
    print("\n\nSolution:")
    solve(real_input)


def solve(lines):
    setup1 = case1(lines)
    setup2 = case2(lines)

    print(f"case 1:")
    for elem in setup1:
        print(elem)

    print(f"\ncase 2:")
    for elem in setup2:
        print(elem)


def split(list_a, chunk_size, skip):
    result = []
    for i in range(0, len(list_a), chunk_size+skip):
        result.append(list_a[i:i + chunk_size][1])
    return result


def case1(lines):
    setup = []
    start = True

    for line in lines:
        if not line:
            start = False
            continue

        if start:
            tmp = split(line, 3, 1)
            i = 0
            for parts in tmp:
                if len(setup) < i + 1:
                    setup.append([])
                if parts != " ":

                    if parts.isnumeric():
                        setup[i].reverse()
                    else:
                        setup[i].append(parts)
                i += 1
        else:
            instruction = re.findall(r'\d+', line)
            for i in range(int(instruction[0])):
                setup[int(instruction[2]) - 1].append(setup[int(instruction[1]) - 1].pop())
    return setup


def case2(lines):
    setup = []
    start = True

    for line in lines:
        if not line:
            start = False
            continue

        if start:
            tmp = split(line, 3, 1)
            i = 0
            for parts in tmp:
                if len(setup) < i + 1:
                    setup.append([])
                if parts != " ":

                    if parts.isnumeric():
                        setup[i].reverse()
                    else:
                        setup[i].append(parts)
                i += 1
        else:
            instruction = re.findall(r'\d+', line)
            setup[int(instruction[2]) - 1].extend(setup[int(instruction[1]) - 1][-int(instruction[0]):])
            setup[int(instruction[1]) - 1] = setup[int(instruction[1]) - 1][:-(int(instruction[0]))]
    return setup


run()
