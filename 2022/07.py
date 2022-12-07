import os.path

import helper.help_functions as hf
from collections import Counter


class File(object):
    def __init__(self, name, parent, dir=True, size=-1):
        self.name = name
        self.dir = dir
        self.size = size
        self.children = []
        self.parent = parent

    def calculate_size(self):
        if self.dir:
            self.size = 0
            for child in self.children:
                self.size += child.calculate_size()
        return self.size

    def size_of_dir_lower(self, lower_then=0):
        sum = 0
        for child in self.children:
            if child.dir:
                if child.size <= lower_then:
                    sum += child.size
                sum += child.size_of_dir_lower(lower_then)
        return sum

    def size_of_file_to_delete(self, req_size, current_size):
        if self.dir and req_size < self.size < current_size:
            current_size = self.size
        for child in self.children:
            if child.dir:
                current_size = child.size_of_file_to_delete(req_size, current_size)
        return current_size


def run():
    file_name = os.path.basename(__file__).replace(".py", "")
    test_input = hf.extract_list(f"inputs/tinput_{file_name}")
    real_input = hf.extract_list(f"inputs/input_{file_name}")
    print("Testrun:")
    solve(test_input)
    print("\nSolution:")
    solve(real_input)


def solve(lines):

    sol1 = case1(read_input(lines))
    sol2 = case2(read_input(lines))

    print(f"case 1: {sol1}")
    print(f"case 2: {sol2}")


def read_input(lines):
    root_dir = File("/", None)
    current_parent = root_dir
    for line in lines:
        if line.startswith("$ cd"):
            red_line = line.replace("$ cd ", "")
            if red_line == "..":
                current_parent = current_parent.parent
                continue
            elif red_line == "/":
                current_parent = root_dir
                continue
            else:
                new_file = File(red_line, current_parent)
                current_parent.children.append(new_file)
                current_parent = new_file
                continue
        elif line.startswith("dir") or line.startswith("$"):
            continue
        else:
            file = line.split(" ")
            new_file = File(file[1], current_parent, False, int(file[0]))
            current_parent.children.append(new_file)
    return root_dir


def case1(root_dir):
    result = 0

    root_dir.calculate_size()
    result = root_dir.size_of_dir_lower(100000)

    return result


def case2(root_dir):
    result = 0
    root_dir.calculate_size()

    filesystem_size = 70000000
    needed_space = 30000000
    current_space = filesystem_size - root_dir.size
    req_space = needed_space - current_space

    result = root_dir.size_of_file_to_delete(req_space, root_dir.size)

    return result


run()
