import os.path
import helper.help_functions as hf
import math

def run():
    file_name = os.path.basename(__file__).replace(".py", "")
    test_input = hf.extract_list(f"inputs/tinput_{file_name}")
    real_input = hf.extract_list(f"inputs/input_{file_name}")
    print("Testrun:")
    solve(test_input)
    print("\nSolution:")
    solve(real_input)


def solve(lines):
    sol1 = case1(parse_monkey(lines))
    sol2 = case2(parse_monkey(lines))

    print(f"case 1: {sol1}")
    print(f"case 2: {sol2}")


class Monkey(object):
    def __init__(self, name, items: list[int], operation: list, divisible_by: int, true: int, false: int):
        self.name = name
        self.items = items
        self.operation = operation
        self.divisible_by = divisible_by
        self.true = true
        self.false = false
        self.inspect_counter = 0


def parse_monkey(lines: list[str]) -> list[Monkey]:
    monkeys = []
    monkey_params = []
    for line in lines:
        line = line.strip()
        if line.startswith("Monkey"):
            monkey_params.append(line)
        elif line.startswith("Starting"):
            line = line.replace("Starting items: ", "")
            monkey_params.append(list(map(int, line.split(", "))))
        elif line.startswith("Operation:"):
            line = line.replace("Operation: new = ", "")
            monkey_params.append(line.split(" "))
        elif line.startswith("Test:"):
            monkey_params.append(int(line.replace("Test: divisible by ", "")))
        elif line.startswith("If true:"):
            monkey_params.append(int(line.replace("If true: throw to monkey ", "")))
        elif line.startswith("If false:"):
            monkey_params.append(int(line.replace("If false: throw to monkey ", "")))

        if len(monkey_params) == 6:
            monkeys.append(
                Monkey(monkey_params[0], monkey_params[1], monkey_params[2], monkey_params[3], monkey_params[4],
                       monkey_params[5]))
            monkey_params = []
    return monkeys


def calculate_worry(old: int, operations) -> int:
    result = 0
    params = []
    for i in [0, 2]:
        if operations[i] == "old":
            params.append(old)
        else:
            params.append(int(operations[i]))

    match operations[1]:
        case "+":
            result = params[0] + params[1]
        case "-":
            result = params[0] - params[1]
        case "*":
            result = params[0] * params[1]
        case "/":
            result = params[0] / params[1]

    return result


def case1(monkeys: list[Monkey]):
    result = 0
    rounds = 20

    for i in range(rounds):
        for monkey in monkeys:
            for item in monkey.items:
                new_value = calculate_worry(item, monkey.operation)
                new_value = math.floor(new_value/3)
                if new_value % monkey.divisible_by:
                    monkeys[monkey.false].items.append(new_value)
                else:
                    monkeys[monkey.true].items.append(new_value)
                monkey.inspect_counter += 1
            monkey.items = []

    values = []
    for monkey in monkeys:
        values.append(monkey.inspect_counter)

    values.sort(reverse=True)
    result = values[0] * values[1]

    return result


def case2(monkeys):
    result = 0
    rounds = 10000

    mod = 1
    for monkey in monkeys:
        mod *= monkey.divisible_by

    for i in range(rounds):
        for monkey in monkeys:
            for item in monkey.items:
                new_value = calculate_worry(item, monkey.operation)
                new_value = new_value % mod

                if new_value % monkey.divisible_by:
                    monkeys[monkey.false].items.append(new_value)
                else:
                    monkeys[monkey.true].items.append(new_value)
                monkey.inspect_counter += 1
            monkey.items = []

    values = []
    for monkey in monkeys:
        values.append(monkey.inspect_counter)

    values.sort(reverse=True)
    result = values[0] * values[1]

    return result


run()
