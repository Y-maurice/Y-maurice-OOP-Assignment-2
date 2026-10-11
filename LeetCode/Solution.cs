public class Solution {
    public int MaxVowels(string s, int k) {
        int left = 0;
        int current = 0;
        int max = 0;

        for (int right = 0; right < s.Length; right++) {
            if (IsVowel(s[right])) current++;

            if (right - left + 1 > k) {
                if (IsVowel(s[left])) current--;
                left++;
            }

            max = Math.Max(max, current);
            if (max == k) return k;   
        }

        return max;
    }

    private static bool IsVowel(char c) {
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
    }
}