import os.path

import helper.help_functions as hf


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
    print(f"case 2:")
    for line in sol2:
        print(line)


def case1(lines):
    result = 0
    cycle = 1
    significant_cycle = 20
    step_size = 40
    instructions = []
    register = 1
    for line in lines:
        if line == "noop":
            instructions.append([0, 1])
        else:
            tmp = line.split(" ")
            instructions.append(([int(tmp[1]), 2]))
    test = []
    pos = 0
    while pos < len(instructions):
        if cycle == significant_cycle:
            # print(f"register: {register}")
            # print(f"signal: {register * significant_cycle}")
            result += register * significant_cycle
            significant_cycle += step_size
        instructions[pos][1] = instructions[pos][1] - 1
        if instructions[pos][1] == 0:
            register += instructions[pos][0]
            pos += 1
        cycle += 1

    return result


def case2(lines):
    result = 0
    cycle = 1
    significant_cycle = 20
    step_size = 40
    instructions = []
    register = 1
    for line in lines:
        if line == "noop":
            instructions.append([0, 1])
        else:
            tmp = line.split(" ")
            instructions.append(([int(tmp[1]), 2]))

    pos = 0
    picture = []
    pixel = ""
    while pos < len(instructions):
        if len(pixel) in [register - 1, register, register + 1]:
            pixel += "#"
        else:
            pixel += "."
        instructions[pos][1] = instructions[pos][1] - 1
        if instructions[pos][1] == 0:
            register += instructions[pos][0]
            pos += 1
        cycle += 1
        if len(pixel) == 40:
            picture.append(pixel)
            pixel = ""

    return picture


run()
