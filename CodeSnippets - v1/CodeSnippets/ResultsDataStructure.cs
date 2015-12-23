using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets
{
    class ResultsDataStructure
    {
        List<string[]> data = null;
        List<KeyValuePair<string, int>> sorted_names = new List<KeyValuePair<string, int>>();
        // keeps track of index in sorted array with specific character
        Dictionary<char, int> beginning_character = new Dictionary<char, int>();

        public int min_index = 0;
        public int max_index = 0;
        public char[] match = null;
        public int match_len = 0;

        Stack<int> past_min_indices = new Stack<int>();
        Stack<int> past_max_indices = new Stack<int>();

        static int CompareKey(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        public void widenIndices()
        {
            if (past_max_indices.Count > 0)
            {
                min_index = past_min_indices.Pop();
                max_index = past_max_indices.Pop();
            }
        }
        
        // ** TO DO: use binary search to make this run in logn time
        public void narrowIndices(char c) {
            bool within_range = getChar(max_index) == c;

            past_min_indices.Push(min_index);
            past_max_indices.Push(max_index);

            // Reduce max_index
            while (min_index < max_index && !within_range)
            {
                max_index--;
                within_range = getChar(max_index) == c;
            }

            // Increase min_index
            within_range = getChar(min_index) == c;
            while (min_index < max_index && !within_range)
            {
                min_index++;
                within_range = getChar(min_index) == c;
            }

            if (min_index == max_index && !within_range)
            {
                max_index--;
            }
        }

        char getChar(int i)
        {
            if (0 > i || i >= sorted_names.Count) { return '\0'; }
            string entry = sorted_names.ElementAt(i).Key;
            return entry.Length <= match_len ? '\0' : entry[match_len];
        }

        public ResultsDataStructure(ref Database db)
        {
            data = db.select("tblSnippets", "*", 2, null, null);

            try
            {
                for (int i = 0; i < data.Count; i++)
                {
                    sorted_names.Add(new KeyValuePair<string,int>(data.ElementAt(i)[0].ToLower(), i));
                }
            }
            catch
            {

            }
            sorted_names.Sort(CompareKey);
            int counter = 0;
            while (counter < sorted_names.Count && getChar(counter) < 'a')
            {
                counter++;
            }
            for (char c = 'a'; c <= 'z'; c++) {
                beginning_character[c] = -1;
                while (counter < sorted_names.Count && getChar(counter) < c)
                {
                    counter++;
                }

                if (counter < sorted_names.Count && getChar(counter) == c) // if found match
                {
                    beginning_character[c] = counter;
                }
            }

            match = new char[20];
            match_len = 0;
            max_index = sorted_names.Count - 1;
        }

        public void addChar(char c) {
            if (match_len < 20)
            {
                if (match_len == 0 && 'a' <= c && c <= 'z')
                {
                    past_min_indices.Push(min_index);
                    past_max_indices.Push(max_index);
                    
                    min_index = beginning_character[c];

                    if (min_index == -1) // not in database
                    {
                        max_index = 0;
                        min_index = max_index + 1;
                    }
                    else
                    {
                        bool done = false;
                        char next = c;
                        next++;

                        for (; next <= 'z'; next++)
                        {
                            if (beginning_character[next] != -1) // character is letter < 'z'
                            {
                                max_index = beginning_character[next] - 1;
                                done = true;
                                break;
                            }
                        }

                        if (!done)
                        {
                            for (int i = min_index + 1; i < sorted_names.Count; i++)
                            {
                                if (getChar(i) != c)
                                {
                                    max_index = i - 1;
                                    break;
                                }
                            }
                        }
                    }
                }
                else {
                    narrowIndices(c);
                }

                match[match_len] = c;
                match_len++;
            }
        }

        public void removeChar() {
            if (match_len > 0)
            {
                widenIndices();
                match_len--;
            }
        }

        public string getNameFromSorted(int i) {
            try {
                string result = data.ElementAt(sorted_names.ElementAt(i).Value)[0];
                return result;
            } catch {

            }
            return "";
        }

        public string getCodeFromSorted(int i)
        {
            try
            {
                string result = data.ElementAt(sorted_names.ElementAt(i).Value)[1];
                return result;
            }
            catch
            {

            }
            return "";
        }
    }
}
