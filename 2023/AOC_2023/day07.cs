using System.Net.Security;
using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day07: Day {

        public struct Hand {
            public Hand(string cards, long points) {
                Cards = cards;
                Points = points;
            }

            public string Cards { get; set; }

            public long Points { get; set; }

            public override string ToString() => $"({Cards}, {Points})";
            public int CalcPoints(int rank) => (int)(Points * rank);

            public int EvaluateCards() {
                int result = 0;
                var chars = Cards.ToCharArray();
                Array.Sort(chars);
                string sortedCards = new String(chars);
                Regex rx_five = new Regex(@"(.)\1{4}");
                Regex rx_four = new Regex(@"(.)\1{3}");
                Regex rx_full_1 = new Regex(@"(.)\1{2}(.)\2{1}");
                Regex rx_full_2 = new Regex(@"(.)\1{1}(.)\2{2}");
                Regex rx_three = new Regex(@"(.)\1{2}");
                Regex rx_two_pair = new Regex(@"(.)\1{1}.*?(.)\2{1}");
                Regex rx_pair = new Regex(@"(.)\1{1}");

                if (rx_five.IsMatch(sortedCards)) {
                    result = 6;
                } else if (rx_four.IsMatch(sortedCards)) {
                    result = 5;
                } else if (rx_full_1.IsMatch(sortedCards) || rx_full_2.IsMatch(sortedCards)) {
                    result = 4;
                } else if (rx_three.IsMatch(sortedCards)) {
                    result = 3;
                } else if (rx_two_pair.IsMatch(sortedCards)) {
                    result = 2;
                } else if (rx_pair.IsMatch(sortedCards)) {
                    result = 1;
                } else {
                    result = 0;
                }

                return result;
            }
            
            public int EvaluateCards2() {
                int result = 0;
                var chars = Cards.ToCharArray();
                Array.Sort(chars);
                string sortedCards = new String(chars);
                Regex rx_joker = new Regex(@"(1*)");
                Regex rx_five = new Regex(@"(.)\1{4}");
                Regex rx_four = new Regex(@"(.)\1{3}");
                Regex rx_full_1 = new Regex(@"(.)\1{2}(.)\2{1}");
                Regex rx_full_2 = new Regex(@"(.)\1{1}(.)\2{2}");
                Regex rx_three = new Regex(@"(.)\1{2}");
                Regex rx_two_pair = new Regex(@"(.)\1{1}.*?(.)\2{1}");
                Regex rx_pair = new Regex(@"(.)\1{1}");
                int joker = rx_joker.Match(sortedCards).Value.Length;
                sortedCards = sortedCards.Replace("1", "");

                if (rx_five.IsMatch(sortedCards)) {
                    result = 6;
                } else if (rx_four.IsMatch(sortedCards)) {
                    result = 5;
                    result += joker;
                } else if (rx_full_1.IsMatch(sortedCards) || rx_full_2.IsMatch(sortedCards)) {
                    result = 4;
                } else if (rx_three.IsMatch(sortedCards)) {
                    result = 3;
                    
                    if (joker > 0) {
                        result += joker +1;
                    }
                } else if (rx_two_pair.IsMatch(sortedCards)) {
                    result = 2;
                    if (joker == 1) {
                        result += joker +1;
                    }
                    
                } else if (rx_pair.IsMatch(sortedCards)) {
                    result = 1;
                    if (joker >= 3) {
                        result += 5;
                    } else if (joker > 0) {
                        result += (int)Math.Pow(2, joker);
                    }
                } else {
                    if (joker == 1) {
                        result = joker;
                    } else if (joker == 2) {
                        result = joker +1;
                    } else if (joker >= 3) {
                        result = Math.Min(6, joker +2);
                    } else {
                        result = 0;
                    }
                }

                return result;
            }
            
        }

        private string replacePictureCard(string input) {
            string result = input;
            result = result.Replace("A", "E");
            result = result.Replace("K", "D");
            result = result.Replace("Q", "C");
            result = result.Replace("J", "B");
            result = result.Replace("T", "A");

            return result;
        }
        private string replacePictureCard2(string input) {
            string result = input;
            result = result.Replace("A", "E");
            result = result.Replace("K", "D");
            result = result.Replace("Q", "C");
            result = result.Replace("J", "1");
            result = result.Replace("T", "A");

            return result;
        }


        protected override Solution Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;
            
            // Your case 1 logic here
            Dictionary<int, List<Hand>> hands = new Dictionary<int, List<Hand>>();
            
            List<Hand> sorted_hands = new List<Hand>();
            foreach (var line in lines) {
                var split = line.Split(" ");
                var hand = new Hand(replacePictureCard(split[0]), long.Parse(split[1]));
                int index = hand.EvaluateCards();
                if (!hands.ContainsKey(index)) {
                    hands.Add(index, new List<Hand>());
                }
                hands[index].Add(hand);
            }
            for (int i = 0; i <= 6; i++) {
                if (hands.ContainsKey(i)) {
                    hands[i].Sort((hand1, hand2) => String.Compare(hand1.Cards, hand2.Cards, StringComparison.Ordinal));
                    sorted_hands.AddRange(hands[i]);
                }
            }

            foreach ((var hand, int i) in sorted_hands.Select((hand, i) => (hand, i))) {
                result += hand.CalcPoints(i + 1);
            }
            

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 2 logic here
            
            Dictionary<int, List<Hand>> hands = new Dictionary<int, List<Hand>>();
            
            List<Hand> sorted_hands = new List<Hand>();
            foreach (var line in lines) {
                var split = line.Split(" ");
                var hand = new Hand(replacePictureCard2(split[0]), long.Parse(split[1]));
                int index = hand.EvaluateCards2();
                if (!hands.ContainsKey(index)) {
                    hands.Add(index, new List<Hand>());
                }
                hands[index].Add(hand);
            }
            for (int i = 0; i <= 6; i++) {
                if (hands.ContainsKey(i)) {
                    hands[i].Sort((hand1, hand2) => String.Compare(hand1.Cards, hand2.Cards, StringComparison.Ordinal));
                    sorted_hands.AddRange(hands[i]);
                }
            }

            foreach ((var hand, int i) in sorted_hands.Select((hand, i) => (hand, i))) {
                result += hand.CalcPoints(i + 1);
            }

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }
    }
}
