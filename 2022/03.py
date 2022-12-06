from helper.help_functions import extract_list


def solve():
    lines = extract_list("inputs/input_03")
    points = 0
    points2 = 0

    for line in lines:
        if line:
            first, second = line[:len(line)//2], line[len(line)//2:]
            for char in first:
                if char in second:
                    if ord(char) >= 97:
                        points += (ord(char) - 96)
                        break
                    else:
                        points += (ord(char) - 38)
                        break

    n = 0
    while n <= len(lines) - 2:
        first, second, third = lines[n], lines[n+1], lines[n+2]
        n += 3
        if first:
            for char in first:
                if char in second and char in third:
                    if ord(char) >= 97:
                        points2 += (ord(char) - 96)
                        break
                    else:
                        points2 += (ord(char) - 38)
                        break

    print(f"case 1: {points}")
    print(f"case 2: {points2}")


solve()
