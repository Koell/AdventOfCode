from helper.help_functions import extract_list


def solve():
    lines = extract_list("inputs/input_04")
    points = 0
    points2 = 0
    for line in lines:
        try:
            pairs = line.split(",")
            code_a = pairs[0].split("-")
            code_b = pairs[1].split("-")
            if contained(int(code_a[0]), int(code_a[1]), int(code_b[0]), int(code_b[1])):
                points += 1

            if overlap(int(code_a[0]), int(code_a[1]), int(code_b[0]), int(code_b[1])):
                points2 += 1
        except:
            pass

    print(f"case 1: {points}")
    print(f"case 2: {points2}")


def contained(a_first: int, a_second: int, b_first: int, b_second: int) -> bool:
    result = False
    if (a_first <= b_first and a_second >= b_second) or (b_first <= a_first and b_second >= a_second):
        result = True

    return result


def overlap(a_first: int, a_second: int, b_first: int, b_second: int) -> bool:
    result = False
    if (a_first <= b_first <= a_second) or (b_first <= a_first <= b_second) or (b_first <= a_second <= b_second)\
            or (a_first <= b_second <= a_second):
        result = True

    return result


solve()
