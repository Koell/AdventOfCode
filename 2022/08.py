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


def case1(lines):
    grid = []
    for line in lines:
        grid.append([int(x) for x in line])

    result = len(grid) * 2 + len(grid[0]) * 2 - 4

    for c in range(1, len(grid) - 1):
        for r in range(1, len(grid[0]) - 1):
            height = grid[c][r]

            top = []
            bottom = []
            for cols in grid[:c]:
                top.append(cols[r])

            for cols in grid[c + 1:]:
                bottom.append(cols[r])
            if max(grid[c][:r]) < height or max(grid[c][r + 1:]) < height \
                    or max(top) < height or max(bottom) < height:
                result += 1

    return result


def case2(lines):
    result = 0
    grid = []
    for line in lines:
        grid.append([int(x) for x in line])

    for c in range(1, len(grid) - 1):
        for r in range(1, len(grid[0]) - 1):
            height = grid[c][r]
            top_s, bottom_s, left_s, right_s = 0, 0, 0, 0
            score = 0
            top = []
            bottom = []
            left = grid[c][:r]
            left.reverse()
            right = grid[c][r + 1:]
            for cols in grid[:c]:
                top.append(cols[r])
            top.reverse()
            for cols in grid[c + 1:]:
                bottom.append(cols[r])

            for tree in top:
                top_s += 1
                if tree >= height:
                    break
            for tree in bottom:
                bottom_s += 1
                if tree >= height:
                    break
            for tree in left:
                left_s += 1
                if tree >= height:
                    break
            for tree in right:
                right_s += 1
                if tree >= height:
                    break

            score = top_s * bottom_s * left_s * right_s
            result = max(result, score)

    return result


run()
