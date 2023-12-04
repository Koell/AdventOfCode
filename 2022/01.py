from helper.help_functions import extract_list


def solve():
    lines = extract_list("inputs/input_01")
    elfs = []
    sum = 0
    for line in lines:
        if not line:
           elfs.append(sum)
           sum = 0
        else:
            sum += int(line)

    if len(lines) and sum != 0:
        elfs.append(sum)

    elfs.sort(reverse=True)
    sum = elfs[0] + elfs[1] + elfs[2]
    print(elfs[0])
    print(sum)

solve()
