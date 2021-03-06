﻿using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets {
    public class TxtKeyValuePair {
        public static string text_file_location = AppDomain.CurrentDomain.BaseDirectory + "DatabaseTextFiles\\";
        
        Dictionary<string, string> hundred_results {get; set;}
        public List<string> file_names { get; set; }

        private bool all_keys_in_memory = false;

        public TxtKeyValuePair() {
            if (!Directory.Exists(text_file_location)) {
                Directory.CreateDirectory(text_file_location);
            }
            List<string> file_paths = Directory.GetFiles(text_file_location).ToList();
            file_names = new List<string>();
            foreach (string str in file_paths) {
                file_names.Add(H.getFileName(str));
            }
            file_names.Sort();
            
            hundred_results = new Dictionary<string, string>();
            if (file_paths.Count <= 100) {
                all_keys_in_memory = true;
                for (int i = 0; i < file_paths.Count; i++) {
                    hundred_results[H.getFileName(file_paths[i])] = H.fileToString(file_paths[i]);
                }
            }
        }

        public bool add(string key, string value)
        {
            if (all_keys_in_memory) {
                hundred_results[key] = value;
            }
            H.stringToFile(value, text_file_location + key + ".txt");
            file_names.Add(key);

            return true;
        }

        public string getValue(string key)
        {
            try {
                if (all_keys_in_memory) {
                    return hundred_results[key];
                }

                return H.fileToString(text_file_location + key + ".txt");
            } catch {
                return "";
            }
        }

        public bool remove(string key) {
            if (all_keys_in_memory) {
                hundred_results[key] = "";
            }
            try {
                File.Delete(text_file_location + key + ".txt");
                file_names.Remove(key);
                return true;
            } catch { }

            return false;
        }

        /************************* Tests *******************************
            TxtKeyValuePair kvp = new TxtKeyValuePair();
            kvp.add("1", "hello");
            kvp.add("2", "world");
            kvp.add("3", "world");
            kvp.remove("3");
            MessageBox.Show(kvp.getValue("1"));
        */
    }
}
