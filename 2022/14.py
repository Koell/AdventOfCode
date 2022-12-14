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
    print(f"case 2: {sol2}")


def case1(lines):
    result = 0

    x_coordinates = []
    y_coordinates = []
    source = [500, 0]
    for line in lines:
        for subline in line.split(" -> "):
            tmp = subline.split(",")
            x_coordinates.append(int(tmp[0]))
            y_coordinates.append(int(tmp[1]))

    cave = [["." for y in range(max(y_coordinates)+1)] for x in range(max(x_coordinates)+1)]

    for line in lines:
        rocks = []
        for subline in line.split(" -> "):
            tmp = subline.split(",")
            rocks.append([int(tmp[0]), int(tmp[1])])
        if len(rocks) > 1:
            for i in range(0, len(rocks) - 1, 1):
                while rocks[i][0] < rocks[i+1][0]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][0] += 1
                while rocks[i][0] > rocks[i+1][0]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][0] -= 1

                while rocks[i][1] < rocks[i+1][1]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][1] += 1
                while rocks[i][1] > rocks[i+1][1]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][1] -= 1
            cave[rocks[-1][0]][rocks[-1][1]] = "#"
        else:
            cave[rocks[0][0]][rocks[0][1]] = "#"

    cave[source[0]][source[1]] = "+"

    count_sand = 0
    sand_flows = True
    while sand_flows:
        count_sand += 1
        new_sand = [source[0], source[1] + 1]
        cave[new_sand[0]][new_sand[1]] = "o"
        while 1:
            if new_sand[1] + 1 >= len(cave[0]):
                sand_flows = False
                count_sand -= 1
                break
            elif cave[new_sand[0]][new_sand[1] + 1] == ".":
                cave[new_sand[0]][new_sand[1]] = "."
                new_sand[1] += 1
                cave[new_sand[0]][new_sand[1]] = "o"
            elif new_sand[0] - 1 < 0:
                sand_flows = False
                count_sand -= 1
                break
            elif cave[new_sand[0] - 1][new_sand[1] + 1] == ".":
                cave[new_sand[0]][new_sand[1]] = "."
                new_sand[0] -= 1
                new_sand[1] += 1
                cave[new_sand[0]][new_sand[1]] = "o"
            elif new_sand[0] + 1 >= len(cave):
                sand_flows = False
                count_sand -= 1
                break
            elif cave[new_sand[0] + 1][new_sand[1] + 1] == ".":
                cave[new_sand[0]][new_sand[1]] = "."
                new_sand[0] += 1
                new_sand[1] += 1
                cave[new_sand[0]][new_sand[1]] = "o"
            else:
                break

    return count_sand


def case2(lines):
    result = 0

    x_coordinates = []
    y_coordinates = []
    source = [500, 0]
    for line in lines:
        for subline in line.split(" -> "):
            tmp = subline.split(",")
            x_coordinates.append(int(tmp[0]))
            y_coordinates.append(int(tmp[1]))

    cave = [["." for y in range(max(y_coordinates)+3)] for x in range(max(x_coordinates)*2+1)]

    for line in lines:
        rocks = []
        for subline in line.split(" -> "):
            tmp = subline.split(",")
            rocks.append([int(tmp[0]), int(tmp[1])])
        if len(rocks) > 1:
            for i in range(0, len(rocks) - 1, 1):
                while rocks[i][0] < rocks[i+1][0]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][0] += 1
                while rocks[i][0] > rocks[i+1][0]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][0] -= 1

                while rocks[i][1] < rocks[i+1][1]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][1] += 1
                while rocks[i][1] > rocks[i+1][1]:
                    cave[rocks[i][0]][rocks[i][1]] = "#"
                    rocks[i][1] -= 1
            cave[rocks[-1][0]][rocks[-1][1]] = "#"
        else:
            cave[rocks[0][0]][rocks[0][1]] = "#"

    for x in range(len(cave)):
        cave[x][max(y_coordinates)+2] = "#"

    cave[source[0]][source[1]] = "+"

    count_sand = 0
    sand_flows = True
    while sand_flows:
        count_sand += 1
        new_sand = [source[0], source[1]]
        cave[new_sand[0]][new_sand[1]] = "o"
        while 1:
            if new_sand[1] + 1 >= len(cave[0]):
                break
            elif cave[new_sand[0]][new_sand[1] + 1] == ".":
                cave[new_sand[0]][new_sand[1]] = "."
                new_sand[1] += 1
                cave[new_sand[0]][new_sand[1]] = "o"
            elif new_sand[0] - 1 < 0:
                break
            elif cave[new_sand[0] - 1][new_sand[1] + 1] == ".":
                cave[new_sand[0]][new_sand[1]] = "."
                new_sand[0] -= 1
                new_sand[1] += 1
                cave[new_sand[0]][new_sand[1]] = "o"
            elif new_sand[0] + 1 >= len(cave):
                break
            elif cave[new_sand[0] + 1][new_sand[1] + 1] == ".":
                cave[new_sand[0]][new_sand[1]] = "."
                new_sand[0] += 1
                new_sand[1] += 1
                cave[new_sand[0]][new_sand[1]] = "o"
            else:
                break
        if new_sand == [source[0], source[1]]:
            sand_flows = False

    return count_sand


run()
