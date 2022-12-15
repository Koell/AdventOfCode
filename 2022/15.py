import os.path
import time

import helper.help_functions as hf
import re


def run():
    file_name = os.path.basename(__file__).replace(".py", "")
    test_input = hf.extract_list(f"inputs/tinput_{file_name}")
    real_input = hf.extract_list(f"inputs/input_{file_name}")
    print("Testrun:")
    solve(test_input, 10, 20)
    print("\nSolution:")
    solve(real_input, 2000000, 4000000)


def solve(lines, y_pos=2000000, range=4000000):
    sol1 = case1(lines, y_pos)
    sol2 = case2(lines, range)

    print(f"case 1: {sol1}")
    print(f"case 2: {sol2}")


def case1(lines, y_pos=2000000):
    result = 0
    sensors = {}
    beacons = {}
    max_x = 0
    min_x = 0
    for line in lines:
        split = re.search(r"\D*? x=(-?\d+), y=(-?\d+)\D*? x=(-?\d+), y=(-?\d+)", line)
        distance = abs(int(split.group(1)) - int(split.group(3))) + abs(int(split.group(2)) - int(split.group(4)))
        sensors[complex(int(split.group(1)), int(split.group(2)))] = distance
        beacons[complex(int(split.group(3)), int(split.group(4)))] = True
        max_x = max(max_x, int(split.group(1)) + distance)
        min_x = min(min_x, int(split.group(1)) - distance)

    for x in range(min_x, max_x + 1):
        scanned_space = False
        if beacons.get(complex(x, y_pos), False):
            continue
        for key, distance in sensors.items():
            if distance >= (abs(key.real - int(x)) + abs(key.imag - int(y_pos))):
                scanned_space = True
                break

        if scanned_space:
            result += 1

    return result


def calculate_range(x, y, distance, ref_y) -> [int]:
    result = []
    dis = distance - abs(y - ref_y)
    if dis >= 0:
        result = [x - dis, x + dis]
    return result


def case2(lines, max_pos=4000000):
    result = 0
    sensors = {}
    beacons = {}
    max_x = 0
    min_x = 0
    start = time.time()
    for line in lines:
        split = re.search(r"\D*? x=(-?\d+), y=(-?\d+)\D*? x=(-?\d+), y=(-?\d+)", line)
        distance = abs(int(split.group(1)) - int(split.group(3))) + abs(int(split.group(2)) - int(split.group(4)))
        sensors[complex(int(split.group(1)), int(split.group(2)))] = distance
        beacons[complex(int(split.group(3)), int(split.group(4)))] = True

    for y in range(max_pos + 1):
        ranges = []
        for key, distance in sensors.items():
            calc_range = calculate_range(key.real, key.imag, distance, y)
            if len(calc_range) == 2:
                ranges.append(calc_range)
        ranges.sort(key=lambda x: x[0])
        combined_ranges = [ranges[0]]
        i = 0
        for rang in ranges:
            if combined_ranges[i][0] <= rang[0] <= combined_ranges[i][1] or \
                    combined_ranges[i][0] <= rang[1] <= combined_ranges[i][1]:
                combined_ranges[i] = [min(combined_ranges[i][0], rang[0]), max(combined_ranges[i][1], rang[1])]
            else:
                combined_ranges.append(rang)
        if len(combined_ranges) > 1:
            result = (combined_ranges[1][0] - 1) * 4000000 + y
            break

    end = time.time()
    print(f"Time needed: {end-start}")
    return result


run()
