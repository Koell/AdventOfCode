import helper.help_functions as hf
import os.path
import time
import networkx as nx
import re
import copy as cp


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


def calculate_optimal_rate(graph, options, source, timer) -> int:
    result = 0
    if len(options) and timer > 0:
        for pos, option in enumerate(options):
            if pos > 9:
                break
            time_to_open = nx.shortest_path_length(graph, source, option[0])
            if (timer - time_to_open - 1) > 0:
                option[1]['tmp_rate'] = option[1]['rate'] * (timer - time_to_open - 1)
                o_copy = cp.deepcopy(options)
                o_copy.pop(pos)
                if o_copy:
                    rec = calculate_optimal_rate(graph, o_copy, option[0], (timer - time_to_open - 1))
                else:
                    rec = 0
                result = max(result, option[1]['tmp_rate'] + rec)

    return result


def calculate_optimal_rate_dual(graph, options, source, source_2, timer, timer_2) -> int:
    result = 0
    if len(options) and timer > 0:
        for pos, option in enumerate(options):
            if pos > 6:
                break
            time_to_open = nx.shortest_path_length(graph, source, option[0])
            if (timer - time_to_open - 1) > 0:
                option[1]['tmp_rate'] = option[1]['rate'] * (timer - time_to_open - 1)
                o_copy = cp.deepcopy(options)
                o_copy.pop(pos)
                for pos_i, option_i in enumerate(o_copy):
                    if pos_i > 6:
                        break
                    if option == option_i:
                        if o_copy:
                            rec = calculate_optimal_rate(graph, o_copy, option[0], (timer - time_to_open - 1))
                        else:
                            rec = 0
                        result = max(result, option[1]['tmp_rate'] + rec)
                    else:
                        time_to_open_2 = nx.shortest_path_length(graph, source_2, option[0])
                        if (timer_2 - time_to_open_2 - 1) > 0:
                            option_i[1]['tmp_rate'] = option_i[1]['rate'] * (timer_2 - time_to_open_2 - 1)
                            i_copy = cp.deepcopy(o_copy)
                            i_copy.pop(pos_i)
                            if i_copy:
                                rec = calculate_optimal_rate_dual(graph, i_copy, option[0], option_i[0], (timer - time_to_open - 1), (timer_2 - time_to_open_2 - 1))
                            else:
                                rec = 0
                            result = max(result, option[1]['tmp_rate'] + option_i[1]['tmp_rate'] + rec)

    return result


def case1(lines):
    start = time.time()
    result = 0
    graph = nx.Graph()

    for line in lines:
        split = re.search(r"Valve (\D\D)\D*?rate=(\d+)\D*? valves? (\D*)", line)
        graph.add_node(split.group(1), rate=int(split.group(2)), tmp_rate=0, open=False)
        for string in split.group(3).split(", "):
            graph.add_edge(split.group(1), string)

    source = "AA"

    timer = 30

    options = [[x, y] for x, y in graph.nodes(data=True) if not y['open'] and y['rate'] > 0]

    for option in options:
        time_to_open = nx.shortest_path_length(graph, source, option[0])
        option[1]['tmp_rate'] = option[1]['rate'] * (timer - time_to_open - 1)

    options.sort(key=lambda x: x[1]['tmp_rate'], reverse=True)

    result = calculate_optimal_rate(graph, options, source, timer)

    end = time.time()
    print(f"Time needed: {end - start}")
    return result


def case2(lines):
    start = time.time()
    result = 0
    graph = nx.Graph()

    for line in lines:
        split = re.search(r"Valve (\D\D)\D*?rate=(\d+)\D*? valves? (\D*)", line)
        graph.add_node(split.group(1), rate=int(split.group(2)), tmp_rate=0, open=False)
        for string in split.group(3).split(", "):
            graph.add_edge(split.group(1), string)

    source = "AA"

    timer = 26

    options = [[x, y] for x, y in graph.nodes(data=True) if not y['open'] and y['rate'] > 0]

    for option in options:
        time_to_open = nx.shortest_path_length(graph, source, option[0])
        option[1]['tmp_rate'] = option[1]['rate'] * (timer - time_to_open - 1)

    options.sort(key=lambda x: x[1]['tmp_rate'], reverse=True)

    result = calculate_optimal_rate_dual(graph, options, source, source, timer, timer)

    end = time.time()
    print(f"Time needed: {end - start}")
    return result


run()
