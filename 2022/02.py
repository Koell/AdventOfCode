from helper.help_functions import extract_list


def solve():
    lines = extract_list("inputs/input_02")
    points = 0
    for line in lines:
        guide = line.split(" ")
        match line:
            case "A Y":
                points += 6
            case "B Z":
                points += 6
            case "C X":
                points += 6
            case "A X":
                points += 3
            case "B Y":
                points += 3
            case "C Z":
                points += 3

        match guide[1]:
            case "X":
                points += 1
            case "Y":
                points += 2
            case "Z":
                points += 3

    points2 = 0
    for line in lines:
        guide = line.split(" ")

        match guide[1]:
            case "X":
                points2 += 0
                match guide[0]:
                    case "A":
                        points2 += 3
                    case "B":
                        points2 += 1
                    case "C":
                        points2 += 2
            case "Y":
                points2 += 3
                match guide[0]:
                    case "A":
                        points2 += 1
                    case "B":
                        points2 += 2
                    case "C":
                        points2 += 3
            case "Z":
                points2 += 6
                match guide[0]:
                    case "A":
                        points2 += 2
                    case "B":
                        points2 += 3
                    case "C":
                        points2 += 1

    print(f"case 1: {points}")
    print(f"case 2: {points2}")


solve()
