import os.path

import helper.help_functions as hf
from collections import Counter


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


def neighbors(head, tail):
    result = True
    if abs(head['x'] - tail['x']) > 1 or abs(head['y'] - tail['y']) > 1:
        result = False
    return result


def case1(lines):
    result = 0
    steps = {'0,0': 1}
    tail = {'x': 0, 'y': 0}
    head = {'x': 0, 'y': 0}

    for line in lines:
        dir = line.split(" ")
        for i in range(0, int(dir[1])):
            match dir[0]:
                case "R":
                    head['x'] = head['x'] + 1
                case "L":
                    head['x'] = head['x'] - 1
                case "U":
                    head['y'] = head['y'] + 1
                case "D":
                    head['y'] = head['y'] - 1
            if not neighbors(head, tail):
                if head['x'] != tail['x']:
                    x_diff = 1 if (head['x'] - tail['x']) > 0 else -1
                    tail['x'] = tail['x'] + x_diff
                if head['y'] != tail['y']:
                    y_diff = 1 if (head['y'] - tail['y']) > 0 else -1
                    tail['y'] = tail['y'] + y_diff

                steps[f"{tail['x']},{tail['y']}"] = 1
    result = len(steps)
    grid = []
    for r in reversed(range(0, 7)):
        sub_grid = []
        for c in range(0, 7):
            if steps.get(f"{c},{r}", 0):
                sub_grid.append("#")
            else:
                sub_grid.append(".")
        print(sub_grid)

    return result


def case2(lines):
    result = 0
    steps = {'0,0': 1}

    knots = []
    for i in range(0, 10):
        knots.append({'x': 0, 'y': 0})

    for line in lines:
        dir = line.split(" ")
        for i in range(0, int(dir[1])):
            for j in range(0, 10):
                if j == 0:
                    match dir[0]:
                        case "R":
                            knots[j]['x'] = knots[j]['x'] + 1
                        case "L":
                            knots[j]['x'] = knots[j]['x'] - 1
                        case "U":
                            knots[j]['y'] = knots[j]['y'] + 1
                        case "D":
                            knots[j]['y'] = knots[j]['y'] - 1
                else:
                    if not neighbors(knots[j-1], knots[j]):
                        if knots[j-1]['x'] != knots[j]['x']:
                            x_diff = 1 if (knots[j-1]['x'] - knots[j]['x']) > 0 else -1
                            knots[j]['x'] = knots[j]['x'] + x_diff
                        if knots[j-1]['y'] != knots[j]['y']:
                            y_diff = 1 if (knots[j-1]['y'] - knots[j]['y']) > 0 else -1
                            knots[j]['y'] = knots[j]['y'] + y_diff

                        if j == 9:
                            steps[f"{knots[j]['x']},{knots[j]['y']}"] = 1
    result = len(steps)

    return result


run()
