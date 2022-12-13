import os.path
import helper.help_functions as hf
import copy
import networkx as nx

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


def calculate_paths(position: (int, int), goal: (int, int), steps: int, height_map: [[int]], movement_map: [[int]],
                    result) -> int:
    # deep search is never an option
    y = position[0]
    x = position[1]
    current_height = ord(height_map[y][x]) + 1
    if position == goal:
        result = min(result, steps)
        # print(f"steps: {steps}")
        # for line in movement_map:
        #    print(line)
    else:
        if x - 1 >= 0 and movement_map[y][x - 1] == "." and current_height >= ord(
                height_map[y][x - 1]) and current_height - 2 < ord(height_map[y][x - 1]):
            left_movement = copy.deepcopy(movement_map)
            left_movement[y][x] = "<"
            result = min(result, calculate_paths((y, x - 1), goal, steps + 1, height_map, left_movement, result))
        if x + 1 < len(height_map[y]) and movement_map[y][x + 1] == "." and current_height >= ord(
                height_map[y][x + 1]) and current_height - 2 < ord(height_map[y][x + 1]):
            right_movement = copy.deepcopy(movement_map)
            right_movement[y][x] = ">"
            result = min(result, calculate_paths((y, x + 1), goal, steps + 1, height_map, right_movement, result))
        if y - 1 >= 0 and movement_map[y - 1][x] == "." and current_height >= ord(
                height_map[y - 1][x]) and current_height - 2 < ord(height_map[y - 1][x]):
            up_movement = copy.deepcopy(movement_map)
            up_movement[y][x] = "^"
            result = min(result, calculate_paths((y - 1, x), goal, steps + 1, height_map, up_movement, result))
        if y + 1 < len(height_map) and movement_map[y + 1][x] == "." and current_height >= ord(
                height_map[y + 1][x]) and current_height - 2 < ord(height_map[y + 1][x]):
            down_movement = copy.deepcopy(movement_map)
            down_movement[y][x] = "v"
            result = min(result, calculate_paths((y + 1, x), goal, steps + 1, height_map, down_movement, result))
    return result


def case1(lines):
    result = 0
    start = (0, 0)
    goal = (0, 0)
    heights = {}
    graph = nx.DiGraph()
    for i, line in enumerate(lines):
        for j, char in enumerate(line):
            if char == "S":
                char = "a"
                start = complex(j, i)
            elif char == "E":
                char = "z"
                goal = complex(j, i)
            heights[complex(j, i)] = char
    for node, c in heights.items():
        for d in [1, -1, 1j, -1j]:
            if ord(heights.get(node + d, "{")) <= ord(c) + 1:
                graph.add_edge(node, node + d)

    paths = nx.shortest_path_length(graph, target=goal)
    return paths.get(start, 0)


def case2(lines):
    result = 0
    start = (0, 0)
    goal = (0, 0)
    heights = {}
    graph = nx.DiGraph()
    for i, line in enumerate(lines):
        for j, char in enumerate(line):
            if char == "S":
                char = "a"
                start = complex(j, i)
            elif char == "E":
                char = "z"
                goal = complex(j, i)
            heights[complex(j, i)] = char
    for node, c in heights.items():
        for d in [1, -1, 1j, -1j]:
            if ord(heights.get(node + d, "{")) <= ord(c) + 1:
                graph.add_edge(node, node + d)

    paths = nx.shortest_path_length(graph, target=goal)
    shortest_path = []
    for node, c in heights.items():
        if c == "a":
            shortest_path.append(paths.get(node, 999))
    return min(shortest_path)


run()
